using System;

namespace HtmlConsole.Extensions
{
    public static class ConsolePresentationExtensions
    {
        public static void Write(this IConsoleWriter writer, string str)
        {
            foreach (var c in str)
            {
                writer.Write(c);
            }
        }

        public static void WriteLine(this IConsoleWriter writer, string str)
        {
            writer.Write(str + Environment.NewLine);
        }
    }
}