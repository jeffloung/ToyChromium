﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using STLib;

namespace ToyChromium
{
    public partial class MainFrm : Form
    {
        string configFile = "config.ini";
        string fullscreen;
        string mouseright;
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
            string path = Environment.CurrentDirectory+"\\"+configFile;
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

            browser = new ChromiumWebBrowser(url)
            {
                Dock = DockStyle.Fill
            };
            if (mouseright == "1")
            {
                browser.MenuHandler = new CustomMenuHandler();
            }

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

        private void Browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            bool isLoading = e.Browser.IsLoading;
            if (isLoading)
            {
                browser.ExecuteScriptAsync(jsFunction);
            }
            Console.WriteLine(isLoading);
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
