using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlConsole.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class StyleParserTests
    {
        [TestMethod]
        public void ParseNode_SingleNode_ParsesCorrectly()
        {
            var parser = new StyleParser();
            parser.Parse("div#hash{ background:red;} #someid{ padding:1 px 2 px 3 px 4 px;}");
        }
    }
}
