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
    }
}
