using System.Diagnostics;

namespace CleverTest.Source.Commands
{
    class CmdHelper
    {
        public static string Run(string cmd, bool needRead)
        {
            string ret = null;
            using (Process p = new Process())
            {
                p.StartInfo.FileName = "cmd.exe";
                p.StartInfo.Arguments = "/c " + cmd;
                p.StartInfo.UseShellExecute = false;        //是否使用操作系统shell启动
                p.StartInfo.RedirectStandardInput = true;   //接受来自调用程序的输入信息
                p.StartInfo.RedirectStandardOutput = true;  //由调用程序获取输出信息
                p.StartInfo.RedirectStandardError = true;   //重定向标准错误输出
                p.StartInfo.CreateNoWindow = true;          //不显示程序窗口
                p.Start();//启动程序

                if(needRead)
                {
                    ret = p.StandardOutput.ReadLine();
                }

                p.WaitForExit();
                p.Close();
            }

            return ret;
        }
    }
}
