using System.Collections;
using System.Collections.Generic;

namespace HtmlConsole.Css
{
    public class DeclarationSet : IEnumerable<Declaration>
    {
        private readonly Dictionary<string, Declaration> _declarations;

        public DeclarationSet()
        {
            _declarations = new Dictionary<string, Declaration>();
        }

        public DeclarationSet(IEnumerable<Declaration> declarations)
        {
            _declarations = new Dictionary<string, Declaration>();
            foreach (var declaration in declarations)
            {
                _declarations[declaration.PropertyName] = Declaration.GetMoreImportantDeclaration(this[declaration.PropertyName], declaration);
            }
        }

        public Declaration this[string propertyName]
        {
            get
            {
                Declaration declaration;
                if (!_declarations.TryGetValue(propertyName, out declaration))
                {
                    return null;
                }

                return declaration;
            }
        }

        public void MergeFrom(DeclarationSet other)
        {
            foreach (var declaration in other)
            {
                _declarations[declaration.PropertyName] = Declaration.GetMoreImportantDeclaration(this[declaration.PropertyName], declaration);
            }
        }

        public IEnumerator<Declaration> GetEnumerator()
        {
            return _declarations.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
