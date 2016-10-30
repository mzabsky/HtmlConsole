using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HtmlConsole.Css;
using HtmlConsole.Extensions;
using HtmlConsole.Tests.Css.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class StyleParserTests
    {
        // The parser is immutable
        private readonly TestingStyleParser _parser = new TestingStyleParser();

        private void TestGetSyntaxTree(string code, string expectedTreeString)
        {
            var syntaxTree = _parser.TestingGetSyntaxTree(code);
            var actualTreeString = _parser.PrintSyntaxTree(syntaxTree,
                (match, level) =>
                    $"{new string('\t', level)}{(match.Matches.Any() ? match.Name : match.Name + "=" + match.Text)}{Environment.NewLine}");

            var expectedTreeLines = expectedTreeString.SplitIntoLines().Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();
            var actualTreeLines = actualTreeString.SplitIntoLines().Where(p => !string.IsNullOrWhiteSpace(p)).ToArray();

            int lineNumber = 0;
            foreach (var linesPairs in expectedTreeLines.Zip(actualTreeLines))
            {
                if(linesPairs.Item1.Trim() != linesPairs.Item2.Trim())
                {
                    Assert.Fail($"Tree line {lineNumber} was expected to be \"{linesPairs.Item1.Trim()}\", was \"{linesPairs.Item2.Trim()}\". Full tree:{Environment.NewLine}{Environment.NewLine}{actualTreeString}");
                }
                lineNumber++;
            }

            if (expectedTreeLines.Length != actualTreeLines.Length)
            {
                Assert.Fail($"Tree line number was expected to be \"{expectedTreeLines.Length}\", was \"{actualTreeLines.Length}\". Full tree:{Environment.NewLine}{Environment.NewLine}{actualTreeString}");
            }
        }

        [TestMethod]
        public void GetSyntaxTree_SimpleCss_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                "div#hash{background:red;}     #someid{padding:1px 1px;}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name
                                        ident=div
                                    hash
                                        ident=hash
                        declarations
                            declaration
                                ident=background
                                expression
                                    term
                                        ident=red
                        S=
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    hash
                                        ident=someid
                        declarations
                            declaration
                                ident=padding
                                expression
                                    term
                                        number=1px
                                    operator
                                        S=
                                    term
                                        number=1px");
        }

        [TestMethod]
        public void GetSyntaxTree_ComplicatedSimpleSelector_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                "div#id.class1.class2:hover[color=red] {}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name
                                        ident=div
                                    hash
                                        ident=id
                                    class
                                        ident=class1
                                    class
                                        ident=class2
                                    pseudo
                                        ident=hover
                                    attrib
                                        ident=color
                                        ident=red
                        S=");
        }

        [TestMethod]
        public void GetSyntaxTree_ComplicatedSimpleSelectorWeirdOrder_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                "div:hover.class1#id[color=red].class2 {}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name
                                        ident=div
                                    pseudo
                                        ident=hover
                                    class
                                        ident=class1
                                    hash
                                        ident=id
                                    attrib
                                        ident=color
                                        ident=red
                                    class
                                        ident=class2
                        S=");
        }

        [TestMethod]
        public void GetSyntaxTree_SelectorWithCombinators_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                "a,b1+b2>b3,c,d1+d2+d3,e1>e2>e3 {}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name
                                        ident=a
                            selector
                                simple_selector
                                    element_name
                                        ident=b1
                                combinator=+
                                simple_selector
                                    element_name
                                        ident=b2
                                combinator=>
                                simple_selector
                                    element_name
                                        ident=b3
                            selector
                                simple_selector
                                    element_name
                                        ident=c
                            selector
                                simple_selector
                                    element_name
                                        ident=d1
                                combinator=+
                                simple_selector
                                    element_name
                                        ident=d2
                                combinator=+
                                simple_selector
                                    element_name
                                        ident=d3
                            selector
                                simple_selector
                                    element_name
                                        ident=e1
                                combinator=>
                                simple_selector
                                    element_name
                                        ident=e2
                                combinator=>
                                simple_selector
                                    element_name
                                        ident=e3
                        S=");
        }

        [TestMethod]
        public void GetSyntaxTree_BasicDeclarations_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                "* {background:red;padding:1px 1px;margin-left:-1em;width:50%;color:#ffffff;z-index:5}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name=*
                            S=
                        declarations
                            declaration
                                ident=background
                                expression
                                    term
                                        ident=red
                            declaration
                                ident=padding
                                expression
                                    term
                                        number=1px
                                    operator
                                        S=
                                    term
                                        number=1px
                            declaration
                                ident=margin-left
                                expression
                                    term
                                        unary_operator=-
                                        number=1em
                            declaration
                                ident=width
                                expression
                                    term
                                        number=50%
                            declaration
                                ident=color
                                expression
                                    term
                                        hexcolor=#ffffff
                            declaration
                                ident=z-index
                                expression
                                    term
                                        number=5");
        }

        [TestMethod]
        public void GetSyntaxTree_CodeWithUpperCase_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                "SpaN {backGround:reD;COLOR:#ffFF09}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name
                                        ident=SpaN
                            S=
                        declarations
                            declaration
                                ident=backGround
                                expression
                                    term
                                        ident=reD
                            declaration
                                ident=COLOR
                                expression
                                    term
                                        hexcolor=#ffFF09");
        }

        [TestMethod]
        public void GetSyntaxTree_ImportantDeclaration_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                "* {backGround:red!important;}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name=*
                            S=
                        declarations
                            declaration
                                ident=backGround
                                expression
                                    term
                                        ident=red
                                prio=!important");
        }
    }
}
