using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlConsole.Dom;
using HtmlConsole.Rendering;

namespace HtmlConsole.Tests.Testing.Extensions
{
    public static class RendererExtensions
    {
        public static string GetRendererString(this IRenderer renderer, Func<IRenderer, string> func)
        {
            StringBuilder sb = new StringBuilder();
            GetRendererString(renderer, sb, 0, func);
            return sb.ToString();
        }

        private static void GetRendererString(IRenderer renderer, StringBuilder sb, int level, Func<IRenderer, string> func)
        {
            var funcString = func(renderer);
            string rendererString = string.IsNullOrEmpty(funcString) ? "" : $" - {funcString}";

            sb.AppendLine($"{new string(' ', level * 4)}{renderer.GetType().Name}{rendererString}");
            foreach (var child in renderer.Children)
            {
                GetRendererString(child, sb, level + 1, func);
            }
        }
    }
}
