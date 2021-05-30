using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.SchemeHandler;
using CefSharp.WinForms;
using STLib;
using ToyChromium.Helper;
using ToyChromium.Helpers;

namespace ToyChromium
{
    public delegate void SetStatusDelegate(bool visiable, string text);
    public delegate void SetPictureDelegate(bool visiable, string text);
    public delegate void ReloadHandle();

    public partial class MainFrm : Form
    {
        /// <summary>
        /// 当前目录，包含有最后的\
        /// </summary>
        string currentExePath;
        string path;
        string configFile = "config.ini";
        string fullscreen;
        string mouseright;
        string topmost;
        string failautoreflush;
        Dictionary<string, string> commands;
        Size mainSize;
        string url="baidu.com";
        string localPath;
        bool localUrl = false;
        ChromiumWebBrowser browser;
        string jsFunction = "";

        UdpServer udpServer;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            currentExePath = Environment.CurrentDirectory + "\\";
            path = currentExePath + configFile;
            InitConfig(path);

            InitSrv();
        }

        private void Reload()
        {
            browser.Reload();
        }

        private void InitSrv()
        {
            udpServer = new UdpServer();
            udpServer.NewRequestReceived += UdpServer_NewRequestReceived;
            udpServer.Start(6810);
        }

        private void UdpServer_NewRequestReceived(System.Net.IPEndPoint ipe, string msg)
        {
            Console.WriteLine(msg);
            if (commands.ContainsKey(msg))
            {
                Console.WriteLine("run:"+commands[msg]);
                browser.ExecuteScriptAsync(commands[msg]);
            }
        }

        private void InitConfig(string configPath)
        {
            picBox.SendToBack();
            SetPicture(true);
            fullscreen = IniHelper.ReadValue("app", "fullscreen", configPath, "0");
            mouseright = IniHelper.ReadValue("app", "disablemouseright", configPath, "0");
            failautoreflush =IniHelper.ReadValue("app", "failautoreflush", configPath, "0");
            commands = new Dictionary<string, string>();
            commands = IniHelper.ReadKeyValues("command", configPath);
            if (fullscreen == "1")
            {
                this.Location = new Point(0, 0);
                mainSize.Height = Screen.PrimaryScreen.Bounds.Height;
                mainSize.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Size = mainSize;
                picBox.Size = mainSize;
            }
            else
            {
                try
                {
                    string customPosition = IniHelper.ReadValue("app", "position", configPath, "0,0");
                    int posX= int.Parse(customPosition.Split(',')[0]);
                    int posY= int.Parse(customPosition.Split(',')[1]);
                    this.Location = new Point(posX, posY);

                    string customSize = IniHelper.ReadValue("app", "size", configPath, "800,400");
                    mainSize.Width = int.Parse(customSize.Split(',')[0]);
                    mainSize.Height = int.Parse(customSize.Split(',')[1]);
                    this.Size = mainSize;
                    picBox.Size = mainSize;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
            chPl.Size = this.Size;

            topmost = IniHelper.ReadValue("app", "topmost", configPath, "0");
            if (topmost.Equals("1"))
            {
                this.TopMost = true;
            }

            //设置缓存
            CefSettings cefSettings = new CefSettings
            {
                Locale="zh-CN",
                CachePath = currentExePath + "Cache",
                LogSeverity = LogSeverity.Disable
            };

            url = IniHelper.ReadValue("app", "url", configPath);
            localPath = url;
            localUrl = url.IndexOf("/") > 0 ? false : true;
            if (localUrl)
            {
                string schemeName = "tc";
                string domainName = "app";
                cefSettings.RegisterScheme(new CefCustomScheme
                {
                    SchemeName = schemeName,
                    DomainName = domainName,
                    SchemeHandlerFactory = new FolderSchemeHandlerFactory(
                    rootFolder: url,
                    hostName: domainName,
                    defaultPage: "index.html" // will default to index.html
                )
                });
                url = schemeName + "://" + domainName;
            }

            Cef.Initialize(cefSettings);

            browser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.Fill,
                KeyboardHandler = new CEFKeyBoardHander()
            };
            if (mouseright == "1")//如果是1，则禁止响应右键菜单
            {
                browser.MenuHandler = new CEFMenuHandler();
            }
            browser.FrameLoadStart += Browser_FrameLoadStart;
            browser.FrameLoadEnd += Browser_FrameLoadEnd;
            chPl.Controls.Add(browser);
            browser.Load(url);

            string scriptPath = IniHelper.ReadValue("app", "script", configPath, "");
            if (scriptPath != "")
            {
                try
                {
                    jsFunction = File.ReadAllText(Environment.CurrentDirectory + "\\Resources\\" + scriptPath);
                }catch (Exception e)
                {
                    MessageBox.Show(e.Message, "错误信息");
                    Environment.Exit(0);
                }
            }
        }

        private void Browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            BeginInvoke(new SetStatusDelegate(SetStatus), true, "开始载入");
        }

        private void SetStatus(bool visiable, string text)
        {
            lblStatus.Visible = visiable;
            lblStatus.Text = text;
            picBox.Visible = visiable;
        }

        void SetPicture(bool visiable, string path = "Resources/start.jpg")
        {
            picBox.Image = new Bitmap(path);
            picBox.Visible = visiable;
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            int httpCode = e.HttpStatusCode;
            bool isLoading = e.Browser.IsLoading;
            Console.WriteLine("end:" + httpCode + isLoading);
            if (httpCode == 200 || url.IndexOf(":\\") > 0)
            {
                BeginInvoke(new SetStatusDelegate(SetStatus), false, "成功");
                BeginInvoke(new SetPictureDelegate(SetPicture), false, "Resources/error.jpg");
                browser.ExecuteScriptAsync(jsFunction);
            }
            else if (httpCode >= 300 && httpCode < 400) { }
            else if (httpCode < 0) { }
            else if (failautoreflush == "1")
            {
                BeginInvoke(new SetStatusDelegate(SetStatus), true, "加载外部网页失败，5秒后尝试重新连接...(状态码:" + e.HttpStatusCode
                    + "，网络错误)");
                BeginInvoke(new SetPictureDelegate(SetPicture), true, "Resources/error.jpg");
                browser.Reload(true);
                Thread.Sleep(5000);
                Console.WriteLine("刷新");
            }
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (udpServer != null)
            {
                udpServer.Stop();
            }
        }
    }
}
