using HtmlConsole.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HtmlConsole.Tests.Css
{
    [TestClass]
    public class ColorTests
    {
        [TestMethod]
        public void FromBytes_All255s_ReturnsFullWhite()
        {
            var actual = Color.FromBytes(255, 255, 255);

            Assert.AreEqual(1 ,actual.Red);
            Assert.AreEqual(1, actual.Green);
            Assert.AreEqual(1, actual.Blue);
            Assert.AreEqual(1, actual.Alpha);
        }

        [TestMethod]
        public void FromHexString_FFFFFFWithHash_ReturnsFullWhite()
        {
            var actual = Color.FromHexString("#ffffff");

            Assert.AreEqual(1, actual.Red);
            Assert.AreEqual(1, actual.Green);
            Assert.AreEqual(1, actual.Blue);
            Assert.AreEqual(1, actual.Alpha);
        }

        [TestMethod]
        public void FromHexString_FFFFFFWithHash_ReturnsRed()
        {
            var actual = Color.FromHexString("#ff0000");

            Assert.AreEqual(1, actual.Red);
            Assert.AreEqual(0, actual.Green);
            Assert.AreEqual(0, actual.Blue);
            Assert.AreEqual(1, actual.Alpha);
        }

        [TestMethod]
        public void FromHexString_FFFWithHash_ReturnsFullWhite()
        {
            var actual = Color.FromHexString("#fff");

            Assert.AreEqual(1, actual.Red);
            Assert.AreEqual(1, actual.Green);
            Assert.AreEqual(1, actual.Blue);
            Assert.AreEqual(1, actual.Alpha);
        }

        [TestMethod]
        public void FromHexString_F00WithHash_ReturnsRed()
        {
            var actual = Color.FromHexString("#f00");

            Assert.AreEqual(1, actual.Red);
            Assert.AreEqual(0, actual.Green);
            Assert.AreEqual(0, actual.Blue);
            Assert.AreEqual(1, actual.Alpha);
        }

        [TestMethod]
        public void FromHexString_FFFFFFWithoutHash_ReturnsNull()
        {
            var actual = Color.FromHexString("ffffff");

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void FromHexString_InvalidString_ReturnsNull()
        {
            var actual = Color.FromHexString("sdf sdfgs");

            Assert.IsNull(actual);
        }

        [TestMethod]
        public void FromNamedColor_Black_ReturnsFullBlack()
        {
            var actual = Color.FromNamedColor("black");

            Assert.AreEqual(0, actual.Red);
            Assert.AreEqual(0, actual.Green);
            Assert.AreEqual(0, actual.Blue);
            Assert.AreEqual(1, actual.Alpha);
        }

        [TestMethod]
        public void FromNamedColor_Red_ReturnsRed()
        {
            var actual = Color.FromNamedColor("red");

            Assert.AreEqual(1, actual.Red);
            Assert.AreEqual(0, actual.Green);
            Assert.AreEqual(0, actual.Blue);
            Assert.AreEqual(1, actual.Alpha);
        }
    }
}
