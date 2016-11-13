namespace HtmlConsole.Css
{
    public class Rule
    {
        public string PropertyName { get; set; } 

        public StyleValue Value { get; set; }

        public bool IsImportant { get; set; }
    }
}
