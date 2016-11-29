using System;

namespace HtmlConsole
{
    public class SystemConsoleWriter : IConsoleWriter
    {
        public void Write(char c)
        {
            Console.Write(c);
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