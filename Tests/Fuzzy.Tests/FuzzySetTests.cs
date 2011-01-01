using Fuzzy.Entities;

namespace Fuzzy.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Tests for FuzzySet class
    /// </summary>
    [TestClass]
    public class FuzzySetTests
    {
        /// <summary>
        /// Test for membership register
        /// </summary>
        [TestMethod]
        public void CalculateMembershipDegreeTest()
        {
            var set = new FuzzySet();
            set.RegisterMembershipFunction(Function);

            var value = set.CalculateMembershipDegree(10);

            Assert.AreEqual(value, 0.038461538461538464);
        }

        /// <summary>
        /// Test AddRange method
        /// </summary>
        [TestMethod]
        public void AddRangeTest()
        {
            var set = new FuzzySet();
            set.RegisterMembershipFunction(Function);

            double[] values = { 6, 7, 8, 9, 10, 1, 2, 3, 4, 5 };
            set.AddRange(values);

            Assert.AreEqual(set.Elements.Count, values.Length);

            var cursor = set.Elements.GetEnumerator();
            double i = 1;
            while (cursor.MoveNext())
            {
                Assert.AreEqual(cursor.Current.Value.X, i);
                i += 1;
            }
        }

        /// <summary>
        /// Test for ToString method
        /// </summary>
        [TestMethod]
        public void ToStringTest()
        {
            var set = new FuzzySet();
            set.RegisterMembershipFunction(new Func<double, double>(x => x));
            double[] values = { 6, 7, 8, 9, 10 };
            set.AddRange(values);

            var expected = "1/6, 1/7, 1/8, 1/9, 1/10";
            var result = set.ToString();
            
            Assert.AreEqual(expected, result);
        }

        /// <summary>
        /// The membership function
        /// </summary>
        /// <param name="x">Value in the universe of discouse</param>
        /// <returns>Degree of membership</returns>
        private static double Function(double x)
        {
            var result = 1 / (1 + Math.Pow(x - 15, 2));
            return result;
        }
    }
}
