using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class StyleValueTests
    {
        // The parser is immutable
        private readonly TestingStyleParser _parser = new TestingStyleParser();

        private StyleValue[] CreateStyleValues(string propertyName, string code)
        {
            var property = StyleProperty.GetAllProperties()[propertyName];
            return StyleValue.Create(property, _parser.TestingGetSyntaxTree(code, StyleParserMode.StyleValue)).ToArray();
        }

        [TestMethod]
        public void Create_SingleLengthValue_ReturnsCorrectSequence()
        {
            var actual = CreateStyleValues("margin-top", "1px");
            Assert.AreEqual(typeof(LengthStyleValue), actual.Single().GetType());
            Assert.AreEqual(1, ((LengthStyleValue)actual.Single()).Length);
            Assert.AreEqual(LengthUnit.Px, ((LengthStyleValue)actual.Single()).Unit);
        }

        [TestMethod]
        public void Create_SingleEnumValue_ReturnsCorrectSequence()
        {
            var actual = CreateStyleValues("border-top", "thick");
            Assert.AreEqual(typeof(EnumStyleValue<BorderThickness>), actual.Single().GetType());
            Assert.AreEqual(BorderThickness.Thick, ((EnumStyleValue<BorderThickness>)actual.Single()).EnumValue);
        }

        [TestMethod]
        public void Create_SingleInheritValue_ReturnsCorrectSequence()
        {
            var actual = CreateStyleValues("border-top", "inherit");
            Assert.AreEqual(typeof(InheritStyleValue), actual.Single().GetType());
        }

        [TestMethod]
        public void Create_SingleInitialValue_ReturnsCorrectSequence()
        {
            var actual = CreateStyleValues("border-top", "initial");
            Assert.AreEqual(typeof(InitialStyleValue), actual.Single().GetType());
        }

        [TestMethod]
        public void Create_MultipleLengthValues_ReturnsCorrectSequence()
        {
            var actual = CreateStyleValues("margin-top", "1px 2px 3px 4px 5px").Cast<LengthStyleValue>().ToArray();
            Assert.AreEqual(1, actual[0].Length);
            Assert.AreEqual(LengthUnit.Px, actual[0].Unit);
            Assert.AreEqual(2, actual[1].Length);
            Assert.AreEqual(LengthUnit.Px, actual[1].Unit);
            Assert.AreEqual(3, actual[2].Length);
            Assert.AreEqual(LengthUnit.Px, actual[2].Unit);
            Assert.AreEqual(4, actual[3].Length);
            Assert.AreEqual(LengthUnit.Px, actual[3].Unit);
            Assert.AreEqual(5, actual[4].Length);
            Assert.AreEqual(LengthUnit.Px, actual[4].Unit);
        }
    }
}
