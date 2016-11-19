using System.Collections.Generic;
using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Dom
{
    [TestClass]
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
            (
                new List<Selector>
                {
                    new ElementSelector("a"),
                    new IsDescendantOfSelector
                    (
                        new ElementSelector("div")
                    )
                }
            );

            var foundNodes = document.Find(selector).ToList();

            Assert.AreEqual(1, foundNodes.Count);
            Assert.AreEqual("a", foundNodes.Single().Element);
            CollectionAssert.AreEquivalent(new[] { "span", "div", "div", "strong" }, foundNodes.Single().GetAncestors().OfType<ElementNode>().Select(p => p.Element).ToList());
        }

        [TestMethod]
        public void FindString_SimpleChain_ReturnsCorrectNodes()
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

            var foundNodes = document.Find("div a").ToList();

            Assert.AreEqual(1, foundNodes.Count);
            Assert.AreEqual("a", foundNodes.Single().Element);
            CollectionAssert.AreEquivalent(new[] { "span", "div", "div", "strong" }, foundNodes.Single().GetAncestors().OfType<ElementNode>().Select(p => p.Element).ToList());
        }

        [TestMethod]
        public void ComputeStyles_TwoStylesheetsAllRuleSetsMatching_PreservesNewer()
        {
            StyleValue value1, value2, value3, value4, value5, value6;
            var stylesheet1 = new Stylesheet
            {
                RuleSets = new List<RuleSet>
                {
                    new RuleSet
                    {
                        Selector = new StarSelector(),
                        Declarations = new []
                        {
                            new Declaration("a", value1 = new AutoStyleValue()),
                            new Declaration("b", value2 = new AutoStyleValue()),
                        }
                    }
                }
            };

            var stylesheet2 = new Stylesheet
            {
                RuleSets = new List<RuleSet>
                {
                    new RuleSet
                    {
                        Selector = new StarSelector(),
                        Declarations = new []
                        {
                            new Declaration("b", value3 = new AutoStyleValue()),
                            new Declaration("c", value4 = new AutoStyleValue())
                        }
                    },
                    new RuleSet
                    {
                        Selector = new StarSelector(),
                        Declarations = new []
                        {
                            new Declaration("b", value5 = new AutoStyleValue()),
                            new Declaration("d", value6 = new AutoStyleValue())
                        }
                    }
                }
            };

            var document = Document.ParseHtml("<div></div>");
            document.Stylesheets.Add(stylesheet1);
            document.Stylesheets.Add(stylesheet2);
            document.ComputeStyles();

            var divNode = document.Find("div").Single();
            Assert.AreEqual(4, divNode.Styles.Count());
            Assert.AreEqual(value1, divNode.Styles["a"].Value);
            Assert.AreEqual(value5, divNode.Styles["b"].Value);
            Assert.AreEqual(value4, divNode.Styles["c"].Value);
            Assert.AreEqual(value6, divNode.Styles["d"].Value);
        }

        [TestMethod]
        public void ComputeStyles_TwoStylesheetsSomeRuleSetsMatching_IncludesMatchingOnly()
        {
            StyleValue value1, value2, value3, value4, value5, value6;
            var stylesheet1 = new Stylesheet
            {
                RuleSets = new List<RuleSet>
                {
                    new RuleSet
                    {
                        Selector = new StarSelector(),
                        Declarations = new []
                        {
                            new Declaration("a", value1 = new AutoStyleValue()),
                            new Declaration("b", value2 = new AutoStyleValue())
                        }
                    }
                }
            };

            var stylesheet2 = new Stylesheet
            {
                RuleSets = new List<RuleSet>
                {
                    new RuleSet
                    {
                        Selector = new StarSelector(),
                        Declarations = new []
                        {
                            new Declaration("b", value3 = new AutoStyleValue()),
                            new Declaration("c", value4 = new AutoStyleValue())
                        }
                    },
                    new RuleSet
                    {
                        Selector = new ElementSelector("span"),
                        Declarations = new []
                        {
                            new Declaration("b", value5 = new AutoStyleValue()),
                            new Declaration("d", value6 = new AutoStyleValue())
                        }
                    }
                }
            };

            var document = Document.ParseHtml("<div></div>");
            document.Stylesheets.Add(stylesheet1);
            document.Stylesheets.Add(stylesheet2);
            document.ComputeStyles();

            var divNode = document.Find("div").Single();
            Assert.AreEqual(3, divNode.Styles.Count());
            Assert.AreEqual(value1, divNode.Styles["a"].Value);
            Assert.AreEqual(value3, divNode.Styles["b"].Value);
            Assert.AreEqual(value4, divNode.Styles["c"].Value);
        }
    }
}
