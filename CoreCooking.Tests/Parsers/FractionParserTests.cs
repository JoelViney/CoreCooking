using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreCooking.Parsers
{
    [TestClass]
    public class FractionParserTests
    {
        [TestMethod]
        public void Half()
        {
            // Arrange

            // Act
            Fraction fraction = FractionParser.RealToFraction(0.5M);

            // Asert
            Assert.AreEqual("1/2", fraction.ToString());
        }

        [TestMethod]
        public void Quarter()
        {
            // Arrange

            // Act
            Fraction fraction = FractionParser.RealToFraction(0.25M);

            // Asert
            Assert.AreEqual("1/4", fraction.ToString());
        }


        [TestMethod]
        public void Third()
        {
            // Arrange

            // Act
            Fraction fraction = FractionParser.RealToFraction(0.3333M);

            // Asert
            Assert.AreEqual("1/3", fraction.ToString());
        }


        [TestMethod]
        public void OneAndAHalf()
        {
            // Arrange

            // Act
            Fraction fraction = FractionParser.RealToFraction(1.5M);

            // Asert
            Assert.AreEqual("1 1/2", fraction.ToString());
        }
    }
}
