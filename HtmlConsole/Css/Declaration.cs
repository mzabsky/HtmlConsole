namespace HtmlConsole.Css
{
    public class Declaration
    {
        public string PropertyName { get; } 

        public StyleValue Value { get; }

        public bool IsImportant { get; }

        public Declaration(string propertyName, StyleValue value, bool isImportant = false)
        {
            PropertyName = propertyName;
            Value = value;
            IsImportant = isImportant;
        }

        public static Declaration GetMoreImportantDeclaration(Declaration older, Declaration newer)
        {
            if (older == null) return newer;

            if (older.IsImportant && !newer.IsImportant) return older;

            return newer;
        }
    }
}
