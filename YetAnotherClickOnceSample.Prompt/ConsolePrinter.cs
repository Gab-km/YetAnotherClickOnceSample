using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YetAnotherClickOnceSample
{
    public class ConsolePrinter : IPrinter
    {
        public bool Printing { get; set; }

        public ConsolePrinter(bool printing)
        {
            this.Printing = printing;
        }

        public void Print(string value)
        {
            if (this.Printing)
            {
                Console.WriteLine(value);
            }
        }

        public void Print(string format, object arg0)
        {
            if (this.Printing)
            {
                Console.WriteLine(format, arg0);
            }
        }

        public void PrintError(string format, object arg0)
        {
            if (this.Printing)
            {
                Console.Error.WriteLine(format, arg0);
            }
        }

        public void PrintDebug(string value)
        {
            if (this.Printing)
            {
                Console.WriteLine(value);
            }
        }

        public void PrintDebug(string format, object arg0, object arg1, object arg2, object arg3)
        {
            if (this.Printing)
            {
                Console.WriteLine(format, arg0, arg1, arg2, arg3);
            }
        }
    }
}
