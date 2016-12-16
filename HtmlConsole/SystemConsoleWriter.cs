using System;
using System.IO;
using HtmlConsole.Css;

namespace HtmlConsole
{
    public class SystemConsoleWriter : IConsoleWriter
    {
        public void Write(char c)
        {
            Console.Write(c);
        }

        public void SetForegroundColor(Color color)
        {
            Console.ForegroundColor = ConsoleColor.Black; // TODO: Translate color
        }

        public void SetBackgroundColor(Color color)
        {
            Console.BackgroundColor = ConsoleColor.White; // TODO: Translate color
        }

        public int GetConsoleWidth()
        {
            try
            {
                return Console.WindowWidth; // TODO: BufferWidth?
            }
            catch (IOException)
            {
                return 80;
            }
        }

        public int GetConsoleHeight()
        {
            try
            {
                return Console.WindowHeight; // TODO: BufferHeight?
            }
            catch (Exception)
            {
                return 80;
            }
        }
    }
}