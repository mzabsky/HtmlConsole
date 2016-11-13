using System;

namespace HtmlConsole.Extensions
{
    public static class ConsolePresentationExtensions
    {
        public static void WriteLine(this IConsoleWriter writer, string str)
        {
            writer.Write(str + Environment.NewLine);
        }
    }
}