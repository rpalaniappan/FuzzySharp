using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Fuzzy.Collections;
using Fuzzy.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuzzy.Tests
{
    [TestClass]
    public class FuzzyValuesTests
    {
        [TestMethod]
        public void AddAndGetFuzzyValueTest()
        {
            var values = new FuzzyValues();

            var inseted = new FuzzySet();
            values.AddFuzzyValue("medium", inseted);

            var result = values.GetFuzzyValue("medium");

            Assert.AreEqual(inseted, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetFuzzyValueTestFailNameNotExists()
        {
            var values = new FuzzyValues();

            var inseted = new FuzzySet();
            values.AddFuzzyValue("medium", inseted);

            var result = values.GetFuzzyValue("Medium");

            Assert.AreEqual(inseted, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveFuzzyValueTestFailNameNotExists()
        {
            var values = new FuzzyValues();

            var inseted = new FuzzySet();
            values.AddFuzzyValue("medium", inseted);
            values.RemoveFuzzyValue("Medium");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RemoveFuzzyValueTestFailGetRemovedItem()
        {
            var values = new FuzzyValues();

            var inseted = new FuzzySet();
            values.AddFuzzyValue("medium", inseted);
            values.RemoveFuzzyValue("medium");
            values.GetFuzzyValue("medium");
        }
    }
}
