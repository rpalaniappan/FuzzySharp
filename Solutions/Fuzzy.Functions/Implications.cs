// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Implications.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Functions
{
    using Contracts;

    /// <summary>
    /// Implications functions
    /// </summary>
    public static class Implications
    {
        /// <summary>
        /// Zadeh Max Min Implication Operator
        /// </summary>
        /// <param name="elementA">FuzzyElement A</param>
        /// <param name="elementB">FuzzyElement B</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement ZadehMaxMinImplication(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            var fuzzy = elementA.Clone();
            fuzzy.Value = elementA.Min(elementB).Max(elementA.Clone(x => x.Value = 1 - elementA.Value)).Value;

            return fuzzy;
        }

        /// <summary>
        /// Mamdani Min Implication Operator
        /// </summary>
        /// <param name="elementA">FuzzyElement A</param>
        /// <param name="elementB">FuzzyElement B</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement MamdaniMinImplication(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            var fuzzy = elementA.Clone();
            fuzzy.Value = elementA.Min(elementB).Value;

            return fuzzy;
        }

        /// <summary>
        /// Larsen Product Implication Operator
        /// </summary>
        /// <param name="elementA">FuzzyElement A</param>
        /// <param name="elementB">FuzzyElement B</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement LarsenProductImplication(this IFuzzyElement elementA, IFuzzyElement elementB)
        {
            var fuzzy = elementA.Clone();
            fuzzy.Value = elementA.Value * elementB.Value;

            return fuzzy;
        }
    }
}
