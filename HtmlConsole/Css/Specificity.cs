using System;

namespace HtmlConsole.Css
{
    public class Specificity : IComparable<Specificity>
    {
        public int ElementSpecificity { get; set; } // Specificity "d"
        public int ClassSpecificity { get; set; } // Specificity "c"
        public int IdSpecificity { get; set; } // Specificity "b"

        public static Specificity operator+(Specificity a, Specificity b)
        {
            return new Specificity
            {
                ElementSpecificity = a.ElementSpecificity + b.ElementSpecificity,
                ClassSpecificity = a.ClassSpecificity + b.ClassSpecificity,
                IdSpecificity = a.IdSpecificity + b.IdSpecificity,
            };
        }

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