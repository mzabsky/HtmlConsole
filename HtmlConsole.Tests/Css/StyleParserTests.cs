using System;
using System.Linq;
using HtmlConsole.Css;
using HtmlConsole.Dom;
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

        private void TestGetSyntaxTree(StyleParserMode mode, string code, string expectedTreeString)
        {
            var syntaxTree = _parser.TestingGetSyntaxTree(code, mode);
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
        public void GetSyntaxTree_StylesheetModeSimpleCss_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                StyleParserMode.Stylesheet,
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
        public void GetSyntaxTree_StylesheetModeComplicatedSimpleSelector_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                StyleParserMode.Stylesheet,
                "div#id.class1.class2:hover[color=red]{}",
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
                                        ident=red");
        }

        [TestMethod]
        public void GetSyntaxTree_StylesheetModeComplicatedSimpleSelectorWeirdOrder_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                StyleParserMode.Stylesheet,
                "div:hover.class1#id[color=red].class2{}",
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
                                        ident=class2");
        }

        [TestMethod]
        public void GetSyntaxTree_StylesheetModeSelectorWithCombinators_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                StyleParserMode.Stylesheet,
                "a,b1+b2>b3,c,d1+d2+d3,e1>e2>e3,f1 f2 f3{}",
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
                            selector
                                simple_selector
                                    element_name
                                        ident=f1
                                S=
                                simple_selector
                                    element_name
                                        ident=f2
                                S=
                                simple_selector
                                    element_name
                                        ident=f3");
        }

        [TestMethod]
        public void GetSyntaxTree_StylesheetModeBasicDeclarations_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                StyleParserMode.Stylesheet,
                "*{background:red;padding:1px 1px;margin-left:-1em;width:50%;color:#ffffff;z-index:5}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name=*
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
        public void GetSyntaxTree_StylesheetModeCodeWithUpperCase_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                StyleParserMode.Stylesheet,
                "SpaN{backGround:reD;COLOR:#ffFF09}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name
                                        ident=SpaN
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
        public void GetSyntaxTree_StylesheetModeImportantDeclaration_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                StyleParserMode.Stylesheet,
                "*{backGround:red!important;}",
                @"
                stylesheet
                    ruleset
                        selectors
                            selector
                                simple_selector
                                    element_name=*
                        declarations
                            declaration
                                ident=backGround
                                expression
                                    term
                                        ident=red
                                prio=!important");
        }

        [TestMethod]
        public void GetSyntaxTree_SelectorModeImportantDeclaration_ParsesCorrectly()
        {
            TestGetSyntaxTree(
                StyleParserMode.Selector,
                "div a.class,b",
                @"
                    selectors
                        selector
                            simple_selector
                                element_name
                                    ident=div
                            S=
                            simple_selector
                                element_name
                                    ident=a
                                class
                                    ident=class
                        selector
                            simple_selector
                                element_name
                                    ident=b");
        }

        [TestMethod]
        public void ParseDeclarations_EmptyString_ReturnsZeroDeclarations()
        {
            var declarations = _parser.ParseDeclarations("");
            Assert.AreEqual(0, declarations.Count);
        }

        [TestMethod]
        public void ParseDeclarations_OneSimpleProperty_ReturnsItsValue()
        {
            var declarations = _parser.ParseDeclarations("display: inline");
            Assert.AreEqual(1, declarations.Count);
            Assert.AreEqual(Display.Inline, ((EnumStyleValue<Display>)declarations["display"].Value).EnumValue);
        }

        [TestMethod]
        public void ParseDeclarations_OneSimplePropertyWithSemicolon_ReturnsItsValue()
        {
            var declarations = _parser.ParseDeclarations("display: inline;");
            Assert.AreEqual(1, declarations.Count);
            Assert.AreEqual(Display.Inline, ((EnumStyleValue<Display>)declarations["display"].Value).EnumValue);
        }

        [TestMethod]
        public void ParseDeclarations_OneSequenceProperty_ReturnsItsValue()
        {
            var declarations = _parser.ParseDeclarations("margin: 1pt 2pt;");
            Assert.AreEqual(4, declarations.Count);
            Assert.AreEqual(1, ((LengthStyleValue)declarations["margin-top"].Value).Length);
            Assert.AreEqual(LengthUnit.Pt, ((LengthStyleValue)declarations["margin-top"].Value).Unit);
            Assert.AreEqual(2, ((LengthStyleValue)declarations["margin-right"].Value).Length);
            Assert.AreEqual(LengthUnit.Pt, ((LengthStyleValue)declarations["margin-right"].Value).Unit);
            Assert.AreEqual(1, ((LengthStyleValue)declarations["margin-bottom"].Value).Length);
            Assert.AreEqual(LengthUnit.Pt, ((LengthStyleValue)declarations["margin-bottom"].Value).Unit);
            Assert.AreEqual(2, ((LengthStyleValue)declarations["margin-left"].Value).Length);
            Assert.AreEqual(LengthUnit.Pt, ((LengthStyleValue)declarations["margin-left"].Value).Unit);
        }

        [TestMethod]
        public void ParseDeclarations_MoreSpecificProperty_PartiallyOverwritesPreviousProperty()
        {
            var declarations = _parser.ParseDeclarations("margin: 1pt 2pt;margin-top: 3pt;");
            Assert.AreEqual(4, declarations.Count);
            Assert.AreEqual(3, ((LengthStyleValue)declarations["margin-top"].Value).Length);
            Assert.AreEqual(LengthUnit.Pt, ((LengthStyleValue)declarations["margin-top"].Value).Unit);
            Assert.AreEqual(2, ((LengthStyleValue)declarations["margin-right"].Value).Length);
            Assert.AreEqual(LengthUnit.Pt, ((LengthStyleValue)declarations["margin-right"].Value).Unit);
            Assert.AreEqual(1, ((LengthStyleValue)declarations["margin-bottom"].Value).Length);
            Assert.AreEqual(LengthUnit.Pt, ((LengthStyleValue)declarations["margin-bottom"].Value).Unit);
            Assert.AreEqual(2, ((LengthStyleValue)declarations["margin-left"].Value).Length);
            Assert.AreEqual(LengthUnit.Pt, ((LengthStyleValue)declarations["margin-left"].Value).Unit);
        }

        [TestMethod]
        public void ParseStylesheet_EmptyString_ReturnsNoRulesets()
        {
            var stylesheet = _parser.ParseStylesheet("");
            Assert.AreEqual(0, stylesheet.RuleSets.Count);
        }

        [TestMethod]
        public void ParseStylesheet_SingleRuleset_ReturnsSingleRuleset()
        {
            var stylesheet = _parser.ParseStylesheet("* {display: block;}");
            Assert.AreEqual(1, stylesheet.RuleSets.Count);

            var ruleset = stylesheet.RuleSets.Single();
            Assert.AreEqual(true, ruleset.Selector.Match(new ElementNode()).IsSuccess);
            Assert.AreEqual("display", ruleset.Declarations.Single().PropertyName);
        }

        [TestMethod]
        public void ParseStylesheet_TwoRulesets_ReturnsBoth()
        {
            var stylesheet = _parser.ParseStylesheet("* {display: block;} #id {display:inline;}");
            Assert.AreEqual(2, stylesheet.RuleSets.Count);

            var ruleset1 = stylesheet.RuleSets.First();
            Assert.AreEqual("[OR [AND [**]]]", ruleset1.Selector.ToString());
            Assert.AreEqual("display", ruleset1.Declarations.Single().PropertyName);

            var ruleset2 = stylesheet.RuleSets.Last();
            Assert.AreEqual("[OR [AND [#id]]]", ruleset2.Selector.ToString());
            Assert.AreEqual("display", ruleset2.Declarations.Single().PropertyName);
        }
    }
}
