using System.Linq;
using HtmlConsole.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class DeclarationSetTests
    {
        [TestMethod]
        public void Construct_CollectionWithDuplicates_RemovesDuplicatesPreservingNewer()
        {
            StyleValue a, b, c;
            var input = new[]
            {
                new Declaration("a", a = new AutoStyleValue()),
                new Declaration("a", b = new AutoStyleValue()),
                new Declaration("b", c = new AutoStyleValue())
            };

            var actual = new DeclarationSet(input);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(b, actual["a"].Value);
            Assert.AreEqual(c, actual["b"].Value);
        }

        [TestMethod]
        public void Construct_CollectionWithDuplicates_RemovesDuplicatesPreservingOlderButMoreImportant()
        {
            StyleValue a, b, c;
            var input = new[]
            {
                new Declaration("a", a = new AutoStyleValue(), true),
                new Declaration("a", b = new AutoStyleValue()),
                new Declaration("b", c = new AutoStyleValue())
            };

            var actual = new DeclarationSet(input);

            Assert.AreEqual(2, actual.Count());
            Assert.AreEqual(a, actual["a"].Value);
            Assert.AreEqual(c, actual["b"].Value);
        }
        
        [TestMethod]
        public void MergeFrom_Duplicates_RemovesDuplicatesPreservingNewer()
        {
            StyleValue a, b, c, d;
            var input1 = new[]
            {
                new Declaration("a", a = new AutoStyleValue()),
                new Declaration("b", b = new AutoStyleValue())
            };
            
            var input2 = new[]
            {
                new Declaration("b", c = new AutoStyleValue()),
                new Declaration("c", d = new AutoStyleValue())
            };

            var actual = new DeclarationSet(input1);
            actual.MergeFrom(new DeclarationSet(input2));

            Assert.AreEqual(3, actual.Count());
            Assert.AreEqual(a, actual["a"].Value);
            Assert.AreEqual(c, actual["b"].Value);
            Assert.AreEqual(d, actual["c"].Value);
        }
        
        [TestMethod]
        public void MergeFrom_Duplicates_RemovesDuplicatesOlderButMoreImportant()
        {
            StyleValue a, b, c, d;
            var input1 = new[]
            {
                new Declaration("a", a = new AutoStyleValue()),
                new Declaration("b", b = new AutoStyleValue(), true)
            };

            var input2 = new[]
            {
                new Declaration("b", c = new AutoStyleValue()),
                new Declaration("c", d = new AutoStyleValue())
            };

            var actual = new DeclarationSet(input1);
            actual.MergeFrom(new DeclarationSet(input2));

            Assert.AreEqual(3, actual.Count());
            Assert.AreEqual(a, actual["a"].Value);
            Assert.AreEqual(b, actual["b"].Value);
            Assert.AreEqual(d, actual["c"].Value);
        }
    }
}
