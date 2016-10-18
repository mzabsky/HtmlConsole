using System;

namespace HtmlConsole
{
    public static class ConsolePresentationExtensions
    {
        public static void WriteLine(this IConsoleWriter writer, string str)
        {
            writer.Write(str + Environment.NewLine);
        }
    }
}