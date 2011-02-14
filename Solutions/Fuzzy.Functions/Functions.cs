// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Functions.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Entities;

    /// <summary>
    /// Provides Fuzzy functions
    /// </summary>
    public static class Functions
    {
        #region Function for FuzzySet's

        /// <summary>
        /// The union of all FuzzySets
        /// </summary>
        /// <param name="fuzzySets">List of all FuzzySets</param>
        /// <returns>FuzzySet result</returns>
        public static IFuzzySet Union(this List<IFuzzySet> fuzzySets)
        {
            var result = fuzzySets.FirstOrDefault().GetNewEmpty();

            var keys = new List<double>();
            fuzzySets.ForEach(x => keys.AddRange(x.Elements.Keys.Select(y => y)));
            var distinctKeys = keys.Distinct().ToList();

            var elements =
                distinctKeys.Select(
                    x => fuzzySets.Where(y => y.Elements.ContainsKey(x)).Select(z => z.Elements[x]).ToList().Max()).
                    ToList();

            result.AddElements(elements);

            return result;
        }

        /// <summary>
        /// Union Function (max)
        /// </summary>
        /// <param name="fuzzySetA">The fuzzy Set A.</param>
        /// <param name="fuzzySetB">The fuzzy Set B.</param>
        /// <returns>New FuzzySet</returns>
        public static IFuzzySet Union(this IFuzzySet fuzzySetA, IFuzzySet fuzzySetB)
        {
            var keys = fuzzySetA.Elements.Keys.Union(fuzzySetB.Elements.Keys);
            var result = keys.Select(key =>
                                         {
                                             var a = fuzzySetA.Elements.ContainsKey(key);
                                             var b = fuzzySetB.Elements.ContainsKey(key);
                                             
                                             if (a && b)
                                             {
                                                 return fuzzySetA.Elements[key].Max(fuzzySetB.Elements[key]);
                                             }

                                             if (a)
                                             {
                                                 return fuzzySetA.Elements[key];
                                             }

                                             return fuzzySetB.Elements[key];
                                         }).ToList();

            var set = fuzzySetA.GetNewEmpty();
            set.AddElements(result);

            return set;
        }

        #region Intersection Function's

        /// <summary>
        /// Intersectoin Function (max)
        /// </summary>
        /// <param name="fuzzySetA">The fuzzy Set A.</param>
        /// <param name="fuzzySetB">The fuzzy Set B.</param>
        /// <returns>New FuzzySet</returns>
        public static IFuzzySet Intersection(this IFuzzySet fuzzySetA, IFuzzySet fuzzySetB)
        {           
            return GenericIntersection(fuzzySetA, fuzzySetB, Min);
        }

        /// <summary>
        /// Intersectoin LukasiewiczAnd Function
        /// </summary>
        /// <param name="fuzzySetA">The fuzzy Set A.</param>
        /// <param name="fuzzySetB">The fuzzy Set B.</param>
        /// <returns>New FuzzySet</returns>
        public static IFuzzySet IntersectionLukasiewiczAnd(this IFuzzySet fuzzySetA, IFuzzySet fuzzySetB)
        {
            return GenericIntersection(fuzzySetA, fuzzySetB, LukasiewiczAnd);
        }

        /// <summary>
        /// Intersectoin GodelAnd Function
        /// </summary>
        /// <param name="fuzzySetA">The fuzzy Set A.</param>
        /// <param name="fuzzySetB">The fuzzy Set B.</param>
        /// <returns>New FuzzySet</returns>
        public static IFuzzySet IntersectionGodelAnd(this IFuzzySet fuzzySetA, IFuzzySet fuzzySetB)
        {
            return GenericIntersection(fuzzySetA, fuzzySetB, GodelAnd);
        }

        /// <summary>
        /// Intersectoin ProductAnd Function
        /// </summary>
        /// <param name="fuzzySetA">The fuzzy Set A.</param>
        /// <param name="fuzzySetB">The fuzzy Set B.</param>
        /// <returns>New FuzzySet</returns>
        public static IFuzzySet IntersectionProductAnd(this IFuzzySet fuzzySetA, IFuzzySet fuzzySetB)
        {
            return GenericIntersection(fuzzySetA, fuzzySetB, ProductAnd);
        }

        /// <summary>
        /// Intersectoin DrasticProductAnd Function
        /// </summary>
        /// <param name="fuzzySetA">The fuzzy Set A.</param>
        /// <param name="fuzzySetB">The fuzzy Set B.</param>
        /// <returns>New FuzzySet</returns>
        public static IFuzzySet IntersectionDrasticProductAnd(this IFuzzySet fuzzySetA, IFuzzySet fuzzySetB)
        {
            return GenericIntersection(fuzzySetA, fuzzySetB, DrasticProductAnd);
        }

        /// <summary>
        /// Intersectoin Function (max)
        /// </summary>
        /// <param name="fuzzySetA">The fuzzy Set A.</param>
        /// <param name="fuzzySetB">The fuzzy Set B.</param>
        /// <param name="andFunction">The and Function.</param>
        /// <returns>New FuzzySet</returns>
        public static IFuzzySet GenericIntersection(
                                                    this IFuzzySet fuzzySetA, 
                                                    IFuzzySet fuzzySetB, 
                                                    Func<IFuzzyElement, IFuzzyElement, IFuzzyElement> andFunction)
        {
            var result = from a in fuzzySetA.Elements.Values
                         join b in fuzzySetB.Elements.Values on a.X equals b.X
                         select andFunction(a, b);

            var set = fuzzySetA.GetNewEmpty();
            set.AddElements(result.ToList());

            return set;
        }

        /// <summary>
        /// Get a valid range of point based on the validates functions
        /// </summary>
        /// <param name="fuzzySetA">FuzzySet A</param>
        /// <param name="fuzzySetB">FuzzySet B</param>
        /// <param name="validates">Validate functions</param>
        /// <returns>Array of points</returns>
        public static double[] GetValidRange(this IFuzzySet fuzzySetA,
                                             IFuzzySet fuzzySetB,
                                             List<Func<double, double, bool>> validates)
        {
            var result = from a in fuzzySetA.Elements.Values
                         join b in fuzzySetB.Elements.Values on a.X equals b.X
                         where validates.TrueForAll(validate => validate(a.X, b.X))
                         select a.X;

            return result.ToArray();
        }

        #endregion

        #endregion

        #region Function for FuzzyElement's

        /// <summary>
        /// Max function
        /// </summary>
        /// <param name="elements">Fuzzy elements</param>
        /// <returns>Max Fuzzy element between A and B</returns>
        public static IFuzzyElement Max(this List<IFuzzyElement> elements)
        {
            var result = elements.FirstOrDefault().Clone();

            result.Value = elements.Select(x => x.Value).Max();

            return result;
        }

        /// <summary>
        /// Min function
        /// </summary>
        /// <param name="elements">Fuzzy elements</param>
        /// <returns>Max Fuzzy element between A and B</returns>
        public static IFuzzyElement Min(this List<IFuzzyElement> elements)
        {
            var result = elements.FirstOrDefault().Clone();

            result.Value = elements.Select(x => x.Value).Min();

            return result;
        }

        /// <summary>
        /// Max function
        /// </summary>
        /// <param name="elementA">Fuzzy element A</param>
        /// <param name="elementB">Fuzzy element B</param>
        /// <returns>Max Fuzzy element between A and B</returns>
        public static IFuzzyElement Max(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            if (elementA.Value >= elementB.Value)
            {
                return elementA;
            }

            return elementB;
        }

        /// <summary>
        /// Min function
        /// </summary>
        /// <param name="elementA">Fuzzy element A</param>
        /// <param name="elementB">Fuzzy element B</param>
        /// <returns>Max Fuzzy element between A and B</returns>
        public static IFuzzyElement Min(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            var result = elementA.Clone(x => x.Value = Math.Min(elementA.Value, elementB.Value));

            return result;
        }       

        #region Logics (T-Norms - AND)

        /// <summary>
        /// Godel logic (G) AND
        /// t-norm x * y = min(x; y)
        /// </summary>
        /// <param name="elementA">Fuzzy element A</param>
        /// <param name="elementB">Fuzzy element B</param>
        /// <returns>New element result o Godel Logic AND</returns>
        public static IFuzzyElement GodelAnd(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            return elementA.Min(elementB);
        }

        /// <summary>
        /// Lukasiewicz logic (L) AND
        /// x * y = max(0; x + y - 1)
        /// </summary>
        /// <param name="elementA">Fuzzy element A</param>
        /// <param name="elementB">Fuzzy element B</param>
        /// <returns>New element result o Lukasiewicz Logic AND</returns>
        public static IFuzzyElement LukasiewiczAnd(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            var temp = elementA.Clone();
            temp.Value = Math.Max(elementA.Value + elementB.Value - 1, 0);
            
            return temp;
        }

        /// <summary>
        /// Product logic (II) AND
        /// x * y = x * y
        /// </summary>
        /// <param name="elementA">Fuzzy element A</param>
        /// <param name="elementB">Fuzzy element B</param>
        /// <returns>New element result o Product Logic AND</returns>
        public static IFuzzyElement ProductAnd(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            var temp = elementA.Clone();
            temp.Value = elementA.Value * elementB.Value;

            return temp;
        }

        /// <summary>
        /// DrasticProduct logic (II) AND
        /// x1 if x2 == 1, x2 if x1 == 1, else 0
        /// </summary>
        /// <param name="elementA">Fuzzy element A</param>
        /// <param name="elementB">Fuzzy element B</param>
        /// <returns>New element result o Product Logic AND</returns>
        public static IFuzzyElement DrasticProductAnd(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            if (elementA == null || elementB == null)
            {
                throw new ArgumentNullException();
            }

            if (elementA.Value == 1)
            {
                return elementB;
            }

            if (elementB.Value == 1)
            {
                return elementA;
            }
            
            var temp = elementA.Clone();
            temp.Value = 0;

            return temp;
        }

        #endregion

        #endregion

        #region Helpers - Arrays

        /// <summary>
        /// Get a array range
        /// </summary>
        /// <param name="from">Inicial range</param>
        /// <param name="to">Final range</param>
        /// <param name="step">range step</param>
        /// <returns>Array representing the range</returns>
        public static double[] GetRange(this double from, double to, double step)
        {
            if (from >= to)
            {
                throw new ArgumentException("Invalid range. The 'from' number must be less then 'to' number.");
            }

            var list = new List<double>();
            while (from <= to)
            {
                list.Add(from);
                from += step;
            }

            return list.ToArray();
        }

        /// <summary>
        /// Get a array range
        /// </summary>
        /// <param name="from">Inicial range</param>
        /// <param name="to">Final range</param>
        /// <param name="step">range step</param>
        /// <param name="digits">Digits for rounding</param>
        /// <returns>Array representing the range</returns>
        public static double[] GetRoundedRange(this double from, double to, double step, int digits)
        {
            if (from >= to)
            {
                throw new ArgumentException("Invalid range. The 'from' number must be less then 'to' number.");
            }

            var list = new List<double>();
            while (from <= to)
            {
                list.Add(from);
                from = Math.Round(from + step, digits);
            }

            return list.ToArray();
        }

        /// <summary>
        /// The a range validating each value by validate function
        /// </summary>
        /// <param name="candidateFrom">Possible begin of result array</param>
        /// <param name="limit">Possible max value of a array</param>
        /// <param name="step">Step</param>
        /// <param name="validate">Function to validate each value</param>
        /// <returns>Double[]</returns>
        public static double[] GetValidRange(this double candidateFrom, double limit, double step, Func<double, bool> validate)
        {
            return GetValidRange(candidateFrom, limit, step, new List<Func<double, bool>> { validate });            
        }

        /// <summary>
        /// The a range validating each value by validate function
        /// </summary>
        /// <param name="candidateFrom">Possible begin of result array</param>
        /// <param name="limit">Possible max value of a array</param>
        /// <param name="step">Step</param>
        /// <param name="validates">List of function to validate each value</param>
        /// <returns>Double[]</returns>
        public static double[] GetValidRange(this double candidateFrom, double limit, double step, List<Func<double, bool>> validates)
        {
            var list = new List<double>();

            while (candidateFrom <= limit)
            {
                double @from = candidateFrom;
                if (validates.TrueForAll(x => x(from)))
                {
                    list.Add(candidateFrom);
                }

                candidateFrom += step;
            }

            return list.ToArray();
        }

        #endregion

        #region Helpers - Double

        /// <summary>
        /// Test if a value is between other two
        /// </summary>
        /// <param name="value">The value</param>
        /// <param name="upper">Upper limit</param>
        /// <param name="lower">Lower limit</param>
        /// <returns>True is the value is between upper and lower</returns>
        public static bool IsBetween(this double value, double upper, double lower)
        {
            return value <= upper && value >= lower;
        }

        /// <summary>
        /// Round a double valu
        /// </summary>
        /// <param name="value">Value</param>
        /// <param name="digits">Number of digits</param>
        /// <returns>Double value</returns>
        public static double Round(this double value, int digits)
        {
            return Math.Round(value, digits);
        }

        #endregion
    }
}
