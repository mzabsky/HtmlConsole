using HtmlConsole.Css;

namespace HtmlConsole
{
    public interface IConsoleWriter
    {
        void Write(char c);
        void SetForegroundColor(Color color);
        void SetBackgroundColor(Color color);
        int GetConsoleWidth();
        int GetConsoleHeight();
    }
}
