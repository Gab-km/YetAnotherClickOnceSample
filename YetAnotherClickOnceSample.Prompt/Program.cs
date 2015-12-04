using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YetAnotherClickOnceSample;

namespace YetAnotherClickOnceSample.Prompt
{
    class Program
    {
        private static ConsolePrinter printer;

        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException +=
                new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            printer = new ConsolePrinter(true);
            var installer = new MyInstaller(printer);
            installer.InstallApplication(@"\\JSSB2447\share\ClickOnceSample\ClickOnceSample.application");
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                var ex = (Exception)e.ExceptionObject;
                printer.PrintError("Unhandled error: {0}", ex.Message);
            }
            finally
            {
                Environment.Exit(1);
            }
        }
    }
}
