using Fuzzy.Contracts.Entities;
using Fuzzy.Entities;

namespace Fuzzy.Functions.Tests
{
    using System;
    using System.Collections.Generic;
    using Contracts;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for Functions class
    /// </summary>
    [TestClass]
    public class FunctionsTests
    {
        /// <summary>
        /// Test the Union function
        /// </summary>
        [TestMethod]
        public void UnionTest()
        {
            var setA = new FuzzySet();
            var listA = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = 6, Value = 0.33 },
                               new FuzzyElement { X = 7, Value = 0.67 },
                               new FuzzyElement { X = 8, Value = 1 },
                               new FuzzyElement { X = 9, Value = 0.67 },
                               new FuzzyElement { X = 10, Value = 0.33 }
                           };
            setA.AddElements(listA);

            var setB = new FuzzySet();
            var listB = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = 3, Value = 0.20 },
                               new FuzzyElement { X = 4, Value = 0.60 },
                               new FuzzyElement { X = 5, Value = 1 },
                               new FuzzyElement { X = 6, Value = 0.60 },
                               new FuzzyElement { X = 7, Value = 0.20 }
                           };
            setB.AddElements(listB);

            var result = setA.Union(setB);

            Assert.AreEqual(result.Elements.Count, 8);
            Assert.AreEqual(result.Elements[3.0].Value, 0.2);
            Assert.AreEqual(result.Elements[4.0].Value, 0.6);
            Assert.AreEqual(result.Elements[5.0].Value, 1.0);
            Assert.AreEqual(result.Elements[6.0].Value, 0.6);
            Assert.AreEqual(result.Elements[7.0].Value, 0.67);
            Assert.AreEqual(result.Elements[8.0].Value, 1.0);
            Assert.AreEqual(result.Elements[9.0].Value, 0.67);
            Assert.AreEqual(result.Elements[10.0].Value, 0.33);
        }

        /// <summary>
        /// Test the Union function
        /// </summary>
        [TestMethod]
        public void IntersectionTest()
        {
            var setA = new FuzzySet();
            var listA = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = 6, Value = 0.33 },
                               new FuzzyElement { X = 7, Value = 0.67 },
                               new FuzzyElement { X = 8, Value = 1 },
                               new FuzzyElement { X = 9, Value = 0.67 },
                               new FuzzyElement { X = 10, Value = 0.33 }
                           };
            setA.AddElements(listA);

            var setB = new FuzzySet();
            var listB = new List<IFuzzyElement>
                           {
                               new FuzzyElement { X = 3, Value = 0.20 },
                               new FuzzyElement { X = 4, Value = 0.60 },
                               new FuzzyElement { X = 5, Value = 1 },
                               new FuzzyElement { X = 6, Value = 0.60 },
                               new FuzzyElement { X = 7, Value = 0.20 }
                           };
            setB.AddElements(listB);

            var result = setA.Intersection(setB);

            Assert.AreEqual(result.Elements.Count, 2);
            Assert.AreEqual(result.Elements[6.0].Value, 0.33);
            Assert.AreEqual(result.Elements[7.0].Value, 0.2);           
        }

        /// <summary>
        /// Test the GetRange function
        /// </summary>
        [TestMethod]
        public void GetRangeTest()
        {
            double initial = 1;
            var range = initial.GetRange(20, 0.5);

            Assert.AreEqual(range.Length, 39);
        }

        /// <summary>
        /// Test the GetRange function
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetRangeTestInvalidRange()
        {
            double initial = 10;
            initial.GetRange(10, 0.5);            
        }

        /// <summary>
        /// Test the GetValidRange function
        /// </summary>
        [TestMethod]
        public void GetValidRangeTest()
        {
            var result = 1.0.GetValidRange(10, 1, x => x % 2 == 0);

            Assert.AreEqual(result.Length, 5);
            Assert.AreEqual(result[0], 2.0);
            Assert.AreEqual(result[1], 4.0);
            Assert.AreEqual(result[2], 6.0);
        }

        /// <summary>
        /// Test the GetValidRange function
        /// </summary>
        [TestMethod]
        public void GetValidRangeTest02()
        {
            var result = (-10.0).GetValidRange(10, 0.01, x => (1 / (1 + (10 * Math.Pow(x - 2, 2)))) > 0.01);

            var a = result[0];
            var b = result[result.Length - 1];

            Assert.IsTrue(a > -1.17 && a < -1.13);
            Assert.IsTrue(b > 5.13 && b < 5.17);
        }

        /// <summary>
        /// Test the GetValidRange function
        /// </summary>
        [TestMethod]
        public void GetValidRangeTest03()
        {
            var result = (-10.0).GetValidRange(10, 0.01, new List<Func<double, bool>>
                                                             {
                                                                 x => (1 / (1 + (10 * Math.Pow(x - 2, 2)))) > 0.01,
                                                                 y => (1 / (1 + (2 * Math.Pow(y, 2)))) > 0.01
                                                             });

            //(1/(1 + (10*Math.Pow(x - 2, 2)))) > 0.01)

            var a = result[0];
            var b = result[result.Length - 1];

            Assert.IsTrue(a > -1.17 && a < -1.13);
            Assert.IsTrue(b > 5.13 && b < 5.17);
        }
    }
}
