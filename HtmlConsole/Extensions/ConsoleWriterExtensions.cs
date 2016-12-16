using System;
using HtmlConsole.Css;
using HtmlConsole.Rendering;

namespace HtmlConsole.Extensions
{
    public static class ConsoleWriterExtensions
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

        public static void WriteBlock(this IConsoleWriter writer, VisualLayer layer)
        {
            // TODO: Consider current position on the line
            Color currentColor = null;
            for (int y = 0; y < layer.Size.Height; y++)
            {
                for (int x = 0; x < layer.Size.Width; x++)
                {
                    var position = new Position(x, y);
                    if (currentColor != layer.GetColor(position))
                    {
                        currentColor = layer.GetColor(position);
                        writer.SetForegroundColor(currentColor);
                    }

                    writer.Write(layer.GetCharacter(position));
                }

                writer.WriteLine("");
            }
        }
    }
}