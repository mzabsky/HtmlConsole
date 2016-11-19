namespace HtmlConsole.Css
{
    public abstract class CombinatorSelectorBase : Selector
    {
        public Selector SubSelector { get; }

        protected CombinatorSelectorBase(Selector selector)
        {
            SubSelector = selector;
        }
    }
}