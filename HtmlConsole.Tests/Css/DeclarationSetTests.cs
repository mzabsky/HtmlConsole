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
                new Declaration {PropertyName = "a", Value = a = new AutoStyleValue(), IsImportant = false},
                new Declaration {PropertyName = "a", Value = b = new AutoStyleValue(), IsImportant = false},
                new Declaration {PropertyName = "b", Value = c = new AutoStyleValue(), IsImportant = false},
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
                new Declaration {PropertyName = "a", Value = a = new AutoStyleValue(), IsImportant = true},
                new Declaration {PropertyName = "a", Value = b = new AutoStyleValue(), IsImportant = false},
                new Declaration {PropertyName = "b", Value = c = new AutoStyleValue(), IsImportant = false},
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
                new Declaration {PropertyName = "a", Value = a = new AutoStyleValue(), IsImportant = false},
                new Declaration {PropertyName = "b", Value = b = new AutoStyleValue(), IsImportant = false},
            };

            var input2 = new[]
            {
                new Declaration {PropertyName = "b", Value = c = new AutoStyleValue(), IsImportant = false},
                new Declaration {PropertyName = "c", Value = d = new AutoStyleValue(), IsImportant = false},
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
                new Declaration {PropertyName = "a", Value = a = new AutoStyleValue(), IsImportant = false},
                new Declaration {PropertyName = "b", Value = b = new AutoStyleValue(), IsImportant = true},
            };

            var input2 = new[]
            {
                new Declaration {PropertyName = "b", Value = c = new AutoStyleValue(), IsImportant = false},
                new Declaration {PropertyName = "c", Value = d = new AutoStyleValue(), IsImportant = false},
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
