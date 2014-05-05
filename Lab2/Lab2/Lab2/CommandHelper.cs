using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Lab2
{
    public static class CommandHelper
    {

        private static Process _process;
        private static string _output;

        public static Process GetProcess()
        {
            if (_process == null)
            {
                _process = new Process();
                ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                _process.StartInfo = startInfo;
                _process.Exited += ProcessOnExited;
            }
            return _process;
        }

        public static string GetOutput()
        {
            return _output ?? string.Empty;
        }

        public static void ExecuteCommand(string command)
        {
            var process = GetProcess();
            process.StartInfo.Arguments = "/C " + command;
            process.Start();
            process.WaitForExit();
            //_output = process.StandardOutput.ReadToEnd();

        }

        private static void ProcessOnExited(object sender, EventArgs eventArgs)
        {
            _output = GetProcess().StandardOutput.ReadToEnd();
        }
    }
}
