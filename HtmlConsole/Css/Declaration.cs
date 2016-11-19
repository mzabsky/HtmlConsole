namespace HtmlConsole.Css
{
    public class Declaration
    {
        public string PropertyName { get; set; } 

        public StyleValue Value { get; set; }

        public bool IsImportant { get; set; }

        public static Declaration GetMoreImportantDeclaration(Declaration older, Declaration newer)
        {
            if (older == null) return newer;

            if (older.IsImportant && !newer.IsImportant) return older;

            return newer;
        }
    }
}
