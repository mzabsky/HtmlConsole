using System;

namespace HtmlConsole
{
    public class SystemConsoleWriter : IConsoleWriter
    {
        public void Write(string str)
        {
            Console.Write(str);
        }

        public void SetForegroundColor(ConsoleColor color)
        {
            Console.ForegroundColor = color;
        }

        public void SetBackgroundColor(ConsoleColor color)
        {
            Console.BackgroundColor = color;
        }

        public int GetConsoleWidth()
        {
            return Console.WindowWidth; // TODO: BufferWidth?
        }

        public int GetConsoleHeight()
        {
            return Console.WindowHeight; // TODO: BufferHeight?
        }
    }
}