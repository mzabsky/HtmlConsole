using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using HtmlAgilityPack;
using HtmlConsole.Css;
using HtmlConsole.Dom;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Dom
{
    [TestClass]
    public class ElementNodeTests
    {
        private HtmlDocument StringToHtmlDoc(string str)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(str);
            return doc;
        }

        
    }
}
