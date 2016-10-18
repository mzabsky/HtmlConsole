using System;

namespace HtmlConsole.Css
{
    public class Specificity : IComparable<Specificity>
    {
        public int ElementSpecificity { get; set; }
        public int ClassSpecificity { get; set; }
        public int IdSpecificity { get; set; }
        
        public int CompareTo(Specificity other)
        {
            var idComparison = IdSpecificity.CompareTo(other.IdSpecificity);
            if (idComparison != 0)
            {
                return idComparison;
            }

            var classComparison = ClassSpecificity.CompareTo(other.ClassSpecificity);
            if (classComparison != 0)
            {
                return classComparison;
            }

            return ElementSpecificity.CompareTo(other.ElementSpecificity);
        }
    }
}