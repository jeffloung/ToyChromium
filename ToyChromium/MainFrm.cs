using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using STLib;

namespace ToyChromium
{
    public delegate void SetStatus(bool visiable, string text);

    public partial class MainFrm : Form
    {
        string path;
        string configFile = "config.ini";
        string fullscreen;
        string mouseright;
        string topmost;
        Dictionary<string, string> commands;
        Size mainSize;
        string url="baidu.com";
        ChromiumWebBrowser browser;
        string jsFunction = "";

        UdpServer udpServer;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            path = Environment.CurrentDirectory+"\\"+configFile;
            InitConfig(path);

            InitSrv();
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
            url = IniHelper.ReadValue("app", "url", configPath);
            fullscreen = IniHelper.ReadValue("app", "fullscreen", configPath, "0");
            mouseright = IniHelper.ReadValue("app", "disablemouseright", configPath, "0");
            commands = new Dictionary<string, string>();
            commands = IniHelper.ReadKeyValues("command", configPath);
            if (fullscreen == "1")
            {
                this.Location = new Point(0, 0);
                mainSize.Height = Screen.PrimaryScreen.Bounds.Height;
                mainSize.Width = Screen.PrimaryScreen.Bounds.Width;
                this.Size = mainSize;
            }
            chPl.Size = this.Size;

            topmost = IniHelper.ReadValue("app", "topmost", configPath, "0");
            if (topmost.Equals("1"))
            {
                this.TopMost = true;
            }

            browser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.Fill
            };
            if (mouseright == "1")
            {
                browser.MenuHandler = new CustomMenuHandler();
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
            BeginInvoke(new SetStatus(setStatus), true, "开始载入");
        }

        private void setStatus(bool visiable, string text)
        {
            lblStatus.Visible = visiable;
            lblStatus.Text = text;
        }

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            int httpCode = e.HttpStatusCode;
            bool isLoading = e.Browser.IsLoading;
            Console.WriteLine("end:" + httpCode + isLoading);
            if (httpCode == 200)
            {
                BeginInvoke(new SetStatus(setStatus), false, "成功");
                browser.ExecuteScriptAsync(jsFunction);
            }
            else
            {
                BeginInvoke(new SetStatus(setStatus), true, "载入失败，状态码：" + e.HttpStatusCode
                    + "，请检查网络，开始自动刷新");
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
