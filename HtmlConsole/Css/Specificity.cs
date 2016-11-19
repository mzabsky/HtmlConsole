using System;

namespace HtmlConsole.Css
{
    public class Specificity : IComparable<Specificity>
    {
        public int IdSpecificity { get; } // Specificity "b"
        public int ClassSpecificity { get; } // Specificity "c"
        public int ElementSpecificity { get; } // Specificity "d"

        public Specificity()
        {
        }

        public Specificity(int idSpecificity, int classSpecificity, int elementSpecificity)
        {
            IdSpecificity = idSpecificity;
            ClassSpecificity = classSpecificity;
            ElementSpecificity = elementSpecificity;
        }

        public static Specificity operator+(Specificity a, Specificity b)
        {
            return new Specificity
            (
                a.IdSpecificity + b.IdSpecificity,
                a.ClassSpecificity + b.ClassSpecificity,
                a.ElementSpecificity + b.ElementSpecificity
            );
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