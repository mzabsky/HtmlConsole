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
            var a = new Specificity(1, 2, 3);
            var b = new Specificity(11, 12, 13);

            var actual = a + b;

            Assert.AreEqual(12, actual.IdSpecificity);
            Assert.AreEqual(14, actual.ClassSpecificity);
            Assert.AreEqual(16, actual.ElementSpecificity);
        }

        [TestMethod]
        public void CompareTo_Equal_Returns0()
        {
            var a = new Specificity(5, 6, 7);
            var b = new Specificity(5, 6, 7);

            var actual = a.CompareTo(b);

            Assert.AreEqual(0, actual);
        }

        [TestMethod]
        public void CompareTo_IdSpecGreaterRestLesser_ReturnsGreater()
        {
            var a = new Specificity(8, 1, 0);
            var b = new Specificity(6, 5, 4);

            var actual = a.CompareTo(b);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void CompareTo_IdSpecLesserRestGreater_ReturnsLesser()
        {
            var a = new Specificity(8, 5, 4);
            var b = new Specificity(10, 1, 0);

            var actual = a.CompareTo(b);

            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void CompareTo_IdSpecSameClassSpecGreaterRestLesser_ReturnsGreater()
        {
            var a = new Specificity(1, 9, 0);
            var b = new Specificity(1, 8, 4);

            var actual = a.CompareTo(b);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void CompareTo_IdSpecSameClassSpecLesserRestGreater_ReturnsLesser()
        {
            var a = new Specificity(1, 8, 4);
            var b = new Specificity(1, 9, 0);

            var actual = a.CompareTo(b);

            Assert.AreEqual(-1, actual);
        }

        [TestMethod]
        public void CompareTo_ElementSpecGreaterRestSame_ReturnsGreater()
        {
            var a = new Specificity(1, 9, 4);
            var b = new Specificity(1, 8, 0);

            var actual = a.CompareTo(b);

            Assert.AreEqual(1, actual);
        }

        [TestMethod]
        public void CompareTo_ElementSpecLesserRestSame_ReturnsLesser()
        {
            var a = new Specificity(1, 2, 0);
            var b = new Specificity(1, 2, 4);

            var actual = a.CompareTo(b);

            Assert.AreEqual(-1, actual);
        }
    }
}
