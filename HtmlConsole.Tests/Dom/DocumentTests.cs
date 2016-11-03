using System.Collections.Generic;
using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Dom
{
    public class DocumentTests
    {
        private void TestParseNode(INode expectedRoot, string html)
        {
            var calculated = Document.ParseHtml(html);

            Assert.IsTrue(expectedRoot.Equals(calculated.RootNode));
        }

        [TestMethod]
        public void ParseNode_SingleNode_ParsesCorrectly()
        {
            var input = "<el/>";
            var expected = new ElementNode
            {
                Element = "el"
            };

            TestParseNode(expected, input);
        }

        [TestMethod]
        public void ParseNode_NodeWithAllTheThings_ParsesCorrectly()
        {
            var input = @"<el id=""testid"" class=""class1 class2"" another=""1234"">Hello world!</el>";
            var expected = new ElementNode
            {
                Element = "el",
                Id = "testid",
                Classes = new[] { "class1", "class2" },
                Attributes = new Dictionary<string, string>
                {
                    {"id", "testid"},
                    {"class", "class1 class2"},
                    {"another", "1234"}
                },
                Children = new INode[]
                {
                    new TextNode("Hello world!"),
                }
            };

            TestParseNode(expected, input);
        }

        [TestMethod]
        public void ParseNode_NodeWithCapitalization_LowerCaseInTagIdAndClasses()
        {
            var input = @"<EL iD=""TeStid"" cLASs=""cLass1 claSS2"" another=""Test1234"">Hello world!</El>";
            var expected = new ElementNode
            {
                Element = "el",
                Id = "testid",
                Classes = new[] { "class1", "class2" },
                Attributes = new Dictionary<string, string>
                {
                    {"id", "TeStid"},
                    {"class", "cLass1 claSS2"},
                    {"another", "Test1234"}
                },
                Children = new INode[]
                {
                    new TextNode("Hello world!"),
                }
            };

            TestParseNode(expected, input);
        }

        [TestMethod]
        public void Find_SimpleChain_ReturnsCorrectNodes()
        {
            var html = @"<strong>
                    <span>
                        <a></a>
                    </span>
                    <div>
                        <div>
                            <span>
                                <strong></strong>
                            </span>
                            <span>
                                <a></a>
                            </span>
                        </div>
                    </div>
                </strong>";
            var document = Document.ParseHtml(html);

            // Equivalent to "div a"
            var selector = new AndSelector
            {
                Children = new List<Selector>
                {
                    new ElementSelector {ElementName = "a"},
                    new IsDescendantOfSelector
                    {
                        SubSelector = new ElementSelector
                        {
                            ElementName = "div"
                        }
                    }
                }
            };

            var foundNodes = document.Find(selector).ToList();

            Assert.AreEqual(1, foundNodes.Count);
            Assert.AreEqual("a", foundNodes.Single().Element);
            CollectionAssert.AreEquivalent(new[] { "span", "div", "div", "strong" }, foundNodes.Single().GetAncestors().OfType<ElementNode>().Select(p => p.Element).ToList());
        }
    }
}
