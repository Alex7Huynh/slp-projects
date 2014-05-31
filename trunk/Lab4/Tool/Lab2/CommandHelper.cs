using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MTT
{
    public static class CommandHelper
    {

        private static Process _process;
        private static string _output;
        private static ProcessStartInfo startInfo;
        public static Process GetProcess(bool bPrint)
        {
            if (_process == null)
            {
                _process = new Process();
                if (startInfo == null)
                {
                    startInfo = new System.Diagnostics.ProcessStartInfo();
                    startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                    startInfo.RedirectStandardOutput = bPrint;
                    startInfo.UseShellExecute = false;
                    startInfo.FileName = "cmd.exe";
                }
                _process.StartInfo = startInfo;
                //_process.Exited += ProcessOnExited;
                _process.OutputDataReceived += _process_OutputDataReceived;

            }
            else
            {
                startInfo.RedirectStandardOutput = bPrint;
                _process.StartInfo = startInfo;
            }

            return _process;
        }

        static void _process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            //_output = e.Data;
        }

        public static string GetOutput()
        {
            return _output ?? string.Empty;
        }

        public static void ExecuteCommand(string command, bool bPrint)
        {
            var process = GetProcess(bPrint);
            process.StartInfo.Arguments = "/C " + command;
            process.Start();
            //process.BeginOutputReadLine();
            process.WaitForExit();

            if (bPrint)
                _output = process.StandardOutput.ReadToEnd();

        }

        //private static void ProcessOnExited(object sender, EventArgs eventArgs)
        //{
        //    _output = GetProcess().StandardOutput.ReadToEnd();
        //}
    }
}
