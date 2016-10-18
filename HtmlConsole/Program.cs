using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HtmlConsole
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    static class RichConsole
    {
        public static IConsoleWriter Writer { get; set; } = new SystemConsoleWriter();


    }
}
