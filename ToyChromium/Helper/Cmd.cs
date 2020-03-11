using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ToyChromium.Helpers
{
    public class Cmd
    {
        private Process proc = null;
        /// <summary>
        /// 超时时间，单位秒
        /// </summary>
        public int timeOut = 600;
        /// <summary>
        /// 构造方法
        /// </summary>
        public Cmd()
        {
            proc = new Process();
        }
        /// <summary>
        /// 执行CMD语句
        /// </summary>
        /// <param name="cmd">要执行的CMD命令</param>
        public string RunCmd(string cmd)
        {
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine(cmd);
            proc.StandardInput.WriteLine("exit");
            proc.WaitForExit(timeOut * 1000);
            string outStr;
            if (!proc.HasExited)
            {
                proc.Kill();
                outStr = "time out";
            }
            else
            {
                outStr = proc.StandardOutput.ReadToEnd();
            }
            proc.Close();
            return outStr;
        }
        public void RunCmd(string cmd, string path)
        {
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.StandardInput.WriteLine("cd "+path);
            proc.StandardInput.WriteLine(cmd);
        }

        public string RunBat(string batPath, string arguments)
        {
            ProcessStartInfo psi = new ProcessStartInfo(batPath, arguments);
            proc.StartInfo.CreateNoWindow = true;
            psi.UseShellExecute = false;
            psi.RedirectStandardOutput = true;
            psi.RedirectStandardInput = true;
            psi.RedirectStandardError = true;
            proc.StartInfo = psi;
            proc.Start();
            string outStr = string.Empty;
            outStr = proc.StandardOutput.ReadToEnd();
            proc.WaitForExit();
            
            proc.Close();
            return outStr;
        }
        /// <summary>
        /// 打开软件并执行命令
        /// </summary>
        /// <param name="programName">软件路径加名称（.exe文件）</param>
        /// <param name="cmd">要执行的命令</param>
        public void RunProgram(string programName, string cmd)
        {
            Process proc = new Process();
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.FileName = programName;
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.Start();
            proc.WaitForExit(timeOut * 1000);
            if (cmd.Length != 0)
            {
                proc.StandardInput.WriteLine(cmd);
            }
            proc.Close();
        }
        /// <summary>
        /// 打开软件
        /// </summary>
        /// <param name="programName">软件路径加名称（.exe文件）</param>
        public void RunProgram(string programName)
        {
            this.RunProgram(programName, "");
        }
    }
}
