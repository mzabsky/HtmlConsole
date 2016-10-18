using System;

namespace HtmlConsole
{
    public interface IConsoleWriter
    {
        void Write(string str);
        void SetForegroundColor(ConsoleColor color);
        void SetBackgroundColor(ConsoleColor color);
        int GetConsoleWidth();
        int GetConsoleHeight();
    }
}
