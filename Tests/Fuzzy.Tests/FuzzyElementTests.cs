using Fuzzy.Entities;

namespace Fuzzy.Tests
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Test for FuzzyElement class
    /// </summary>
    [TestClass]
    public class FuzzyElementTests
    {
        /// <summary>
        /// Test ToString method
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            var element = new FuzzyElement { X = 6, Value = 0.33 };
            
            Assert.AreEqual(element.ToString(), "0.33/6");
        }

        /// <summary>
        /// Test for Equals method
        /// </summary>
        [TestMethod]
        public void EqualsTest()
        {
            var elementA = new FuzzyElement { X = 6, Value = 0.33 };
            var elementB = new FuzzyElement { X = 6, Value = 0.33 };

            Assert.IsTrue(elementA.Equals(elementB));
            Assert.IsTrue(elementA == elementB);
            Assert.IsFalse(elementA != elementB);
            Assert.IsTrue(elementA.GetHashCode() == elementB.GetHashCode());

            var elementC = new FuzzyElement { X = 6, Value = 0.31 };

            Assert.IsFalse(elementA.Equals(elementC));
            Assert.IsFalse(elementA == elementC);
            Assert.IsTrue(elementA != elementC);
            Assert.IsFalse(elementA.GetHashCode() == elementC.GetHashCode());
        }
    }
}
