namespace HtmlConsole.Css
{
    public abstract class CombinatorSelectorBase : Selector
    {
        public Selector SubSelector { get; set; }
    }
}