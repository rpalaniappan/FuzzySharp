using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Fuzzy.Collections;
using Fuzzy.Contracts;
using Fuzzy.Contracts.Entities;
using Fuzzy.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fuzzy.Functions.Tests
{
    [TestClass]
    public class RelationsTests
    {
        [TestMethod]
        public void GetCartesianProductMamdaniMinTest()
        {
            var setA = new FuzzySet();
            var list = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = 2, Value = 0.5 },
                               new FuzzyElement { X = 3, Value = 1.0 },
                               new FuzzyElement { X = 4, Value = 0.5 }
                           };
            setA.AddElements(list);

            var setB = new FuzzySet();
            list = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = 5, Value = 0.33 },
                               new FuzzyElement { X = 6, Value = 0.67 },
                               new FuzzyElement { X = 7, Value = 1.0 },
                               new FuzzyElement { X = 8, Value = 0.67 },
                               new FuzzyElement { X = 9, Value = 0.33 }
                           };
            setB.AddElements(list);

            var result = Relations.GetCartesianProduct(setA, setB, Implications.MamdaniMinImplication, () => new FuzzyRow());

            Assert.AreEqual(result.ElementAt(0).ElementAt(0).Value, (new FuzzyElement { X = 2, Value = 0.33 }).Value);
            Assert.AreEqual(result.ElementAt(0).ElementAt(1).Value, (new FuzzyElement { X = 2, Value = 0.5 }).Value);
            Assert.AreEqual(result.ElementAt(0).ElementAt(2).Value, (new FuzzyElement { X = 2, Value = 0.5 }).Value);
            Assert.AreEqual(result.ElementAt(0).ElementAt(3).Value, (new FuzzyElement { X = 2, Value = 0.5 }).Value);
            Assert.AreEqual(result.ElementAt(0).ElementAt(4).Value, (new FuzzyElement { X = 2, Value = 0.33 }).Value);
            Assert.AreEqual(result.ElementAt(1).ElementAt(0).Value, (new FuzzyElement { X = 2, Value = 0.33 }).Value);
            Assert.AreEqual(result.ElementAt(1).ElementAt(1).Value, (new FuzzyElement { X = 2, Value = 0.67 }).Value);
            Assert.AreEqual(result.ElementAt(1).ElementAt(2).Value, (new FuzzyElement { X = 2, Value = 1.0 }).Value);
            Assert.AreEqual(result.ElementAt(1).ElementAt(3).Value, (new FuzzyElement { X = 2, Value = 0.67 }).Value);
            Assert.AreEqual(result.ElementAt(2).ElementAt(4).Value, (new FuzzyElement { X = 2, Value = 0.33 }).Value);
            Assert.AreEqual(result.ElementAt(2).ElementAt(0).Value, (new FuzzyElement { X = 2, Value = 0.33 }).Value);
            Assert.AreEqual(result.ElementAt(2).ElementAt(1).Value, (new FuzzyElement { X = 2, Value = 0.5 }).Value);
            Assert.AreEqual(result.ElementAt(2).ElementAt(2).Value, (new FuzzyElement { X = 2, Value = 0.5 }).Value);
            Assert.AreEqual(result.ElementAt(2).ElementAt(3).Value, (new FuzzyElement { X = 2, Value = 0.5 }).Value);
            Assert.AreEqual(result.ElementAt(2).ElementAt(4).Value, (new FuzzyElement { X = 2, Value = 0.33 }).Value);
        }

        [TestMethod]
        public void GetCartesianProductLarsenProductTest()
        {
            var setA = new FuzzySet();
            var list = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = -1, Value = 0.33 },
                               new FuzzyElement { X = 0, Value = 0.67 },
                               new FuzzyElement { X = 1, Value = 1.0 },
                               new FuzzyElement { X = 2, Value = 0.75 },
                               new FuzzyElement { X = 3, Value = 0.5 },
                               new FuzzyElement { X = 4, Value = 0.25 }
                           };
            setA.AddElements(list);

            var setB = new FuzzySet();
            list = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = -4, Value = 0.5 },
                               new FuzzyElement { X = -3, Value = 1.0 },
                               new FuzzyElement { X = -2, Value = 0.67 },
                               new FuzzyElement { X = -1, Value = 0.33 }
                           };
            setB.AddElements(list);

            var result = Relations.GetCartesianProduct(setA, setB, Implications.LarsenProductImplication, () => new FuzzyRow());

            Assert.AreEqual(result.ElementAt(0).ElementAt(0).Value, (new FuzzyElement { X = 2, Value = 0.165 }).Value);
            Assert.AreEqual(result.ElementAt(0).ElementAt(1).Value, (new FuzzyElement { X = 2, Value = 0.33 }).Value);
            Assert.AreEqual(result.ElementAt(0).ElementAt(2).Value.Round(4), (new FuzzyElement { X = 2, Value = 0.2211 }).Value);
            Assert.AreEqual(result.ElementAt(0).ElementAt(3).Value.Round(4), (new FuzzyElement { X = 2, Value = 0.1089 }).Value);
            Assert.AreEqual(result.ElementAt(1).ElementAt(0).Value, (new FuzzyElement { X = 2, Value = 0.335 }).Value);
            Assert.AreEqual(result.ElementAt(1).ElementAt(1).Value, (new FuzzyElement { X = 2, Value = 0.67 }).Value);
            Assert.AreEqual(result.ElementAt(1).ElementAt(2).Value.Round(4), (new FuzzyElement { X = 2, Value = 0.4489 }).Value);
            Assert.AreEqual(result.ElementAt(1).ElementAt(3).Value.Round(4), (new FuzzyElement { X = 2, Value = 0.2211 }).Value);            
        }
    }
}
