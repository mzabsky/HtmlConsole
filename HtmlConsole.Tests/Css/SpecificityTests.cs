using HtmlConsole.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class SpecificityTests
    {
        [TestMethod]
        public void OperatorPlus_TwoSpecificities_SumsComponentsCorrectly()
        {
            var a = new Specificity { ClassSpecificity = 1, ElementSpecificity = 2, IdSpecificity = 3 };
            var b = new Specificity { ClassSpecificity = 11, ElementSpecificity = 12, IdSpecificity = 13 };

            var actual = a + b;

            Assert.AreEqual(12, actual.ClassSpecificity);
            Assert.AreEqual(14, actual.ElementSpecificity);
            Assert.AreEqual(16, actual.IdSpecificity);
        }

        [TestMethod]
        public void CompareTo_Equal_Returns0()
        {
            var a = new Specificity { ClassSpecificity = 5, ElementSpecificity = 6, IdSpecificity = 7 };
            var b = new Specificity { ClassSpecificity = 5, ElementSpecificity = 6, IdSpecificity = 7 };

            var actual = a.CompareTo(b);

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void CompareTo_IdSpecGreaterRestLesser_ReturnsGreater()
        {
            var a = new Specificity { IdSpecificity = 8, ClassSpecificity = 1, ElementSpecificity = 0 };
            var b = new Specificity { IdSpecificity = 6, ClassSpecificity = 5, ElementSpecificity = 4 };

            var actual = a.CompareTo(b);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void CompareTo_IdSpecLesserRestGreater_ReturnsLesser()
        {
            var a = new Specificity { IdSpecificity = 8, ClassSpecificity = 5, ElementSpecificity = 4 };
            var b = new Specificity { IdSpecificity = 10, ClassSpecificity = 1, ElementSpecificity = 0 };

            var actual = a.CompareTo(b);

            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void CompareTo_IdSpecSameClassSpecGreaterRestLesser_ReturnsGreater()
        {
            var a = new Specificity { IdSpecificity = 1, ClassSpecificity = 9, ElementSpecificity = 0 };
            var b = new Specificity { IdSpecificity = 1, ClassSpecificity = 8, ElementSpecificity = 4 };

            var actual = a.CompareTo(b);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void CompareTo_IdSpecSameClassSpecLesserRestGreater_ReturnsLesser()
        {
            var a = new Specificity { IdSpecificity = 1, ClassSpecificity = 8, ElementSpecificity = 4 };
            var b = new Specificity { IdSpecificity = 1, ClassSpecificity = 9, ElementSpecificity = 0 };

            var actual = a.CompareTo(b);

            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void CompareTo_ElementSpecGreaterRestSame_ReturnsGreater()
        {
            var a = new Specificity { IdSpecificity = 1, ClassSpecificity = 9, ElementSpecificity = 4 };
            var b = new Specificity { IdSpecificity = 1, ClassSpecificity = 8, ElementSpecificity = 0 };

            var actual = a.CompareTo(b);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void CompareTo_ElementSpecLesserRestSame_ReturnsLesser()
        {
            var a = new Specificity { IdSpecificity = 1, ClassSpecificity = 2, ElementSpecificity = 0 };
            var b = new Specificity { IdSpecificity = 1, ClassSpecificity = 2, ElementSpecificity = 4 };

            var actual = a.CompareTo(b);

            Assert.AreEqual(-1, actual);
        }
    }
}
