using System.Collections.Generic;
using System.Xml;
using HtmlAgilityPack;
using HtmlConsole.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Dom
{
    [TestClass]
    public class TagNodeTests
    {
        private HtmlDocument StringToDoc(string str)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            return doc;
        }

        private void TestParseNode(INode expected, string str)
        {
            var calculated = TagNode.ParseNode(StringToDoc(str).DocumentNode.FirstChild);

            Assert.IsTrue(expected.Equals(calculated));
        }
           
        [TestMethod]
        public void ParseNode_SingleNode_ParsesCorrectly()
        {
            var input = "<el/>";
            var expected = new TagNode
            {
                Tag = "el"
            };

            TestParseNode(expected, input);
        }
           
        [TestMethod]
        public void ParseNode_NodeWithAllTheThings_ParsesCorrectly()
        {
            var input = @"<el id=""testid"" class=""class1 class2"" another=""1234"">Hello world!</el>";
            var expected = new TagNode
            {
                Tag = "el",
                Id = "testid",
                Classes = new [] {"class1", "class2"},
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
            var expected = new TagNode
            {
                Tag = "el",
                Id = "testid",
                Classes = new [] {"class1", "class2"},
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
    }
}
