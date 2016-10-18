namespace HtmlConsole
{
    public interface IConsolePresentation
    {
        string ReadLine();
        void Write(string str);
        void WriteLine(string line);
    }
}