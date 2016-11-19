namespace HtmlConsole.Css
{
    public class SelectorMatch
    {
        public bool IsSuccess { get; }
        public Specificity Specificity { get; }

        public SelectorMatch(bool isSuccess, Specificity specificity)
        {
            IsSuccess = isSuccess;
            Specificity = specificity;
        }
    }
}
