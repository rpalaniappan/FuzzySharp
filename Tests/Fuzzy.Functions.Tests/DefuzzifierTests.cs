using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Fuzzy.Contracts.Entities;
using Fuzzy.Defuzzifiers;
using Fuzzy.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuzzy.Functions.Tests
{
    [TestClass]
    public partial class DefuzzifierTests
    {
        [TestMethod]
        public void TestMethod1()
        {
            var setA = new FuzzySet();
            var listA = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = 6, Value = 0.30 },
                               new FuzzyElement { X = 7, Value = 0.60 },
                               new FuzzyElement { X = 8, Value = 1 }
                           };
            setA.AddElements(listA);

            var setB = new FuzzySet();
            var listB = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = 5, Value = 1 },
                               new FuzzyElement { X = 6, Value = 0.60 },
                               new FuzzyElement { X = 7, Value = 0.20 }
                           };
            setB.AddElements(listB);

            var list = new List<IFuzzySet> { setA, setB };

            var defuzzifier = new CenterOfSumDefuzzifier(Functions.Union);

            var result = defuzzifier.Defuzzifier(list).Round(3);

            Assert.AreEqual(result, 6.486);
        }
    }
}
