// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Modifiers.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Modifiers for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Fuzzy.Contracts.Entities;

namespace Fuzzy.Functions
{
    using System;
    using Contracts;

    /// <summary>
    /// Modifiers extension methods
    /// </summary>
    public static class Modifiers
    {
        /// <summary>
        /// Apply Very modifier to FuzzyElement
        /// </summary>
        /// <param name="fuzzyElement">FuzzyElement</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement Very(this IFuzzyElement fuzzyElement)
        {
            fuzzyElement.Value = Math.Pow(fuzzyElement.Value, 2);

            return fuzzyElement;
        }

        /// <summary>
        /// Apply MoreOrLess modifier to FuzzyElement
        /// </summary>
        /// <param name="fuzzyElement">FuzzyElement</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement MoreOrLess(this IFuzzyElement fuzzyElement)
        {
            fuzzyElement.Value = Math.Pow(fuzzyElement.Value, 0.5);

            return fuzzyElement;
        }

        /// <summary>
        /// Apply Plus modifier to FuzzyElement
        /// </summary>
        /// <param name="fuzzyElement">FuzzyElement</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement Plus(this IFuzzyElement fuzzyElement)
        {
            fuzzyElement.Value = Math.Pow(fuzzyElement.Value, 1.25);

            return fuzzyElement;
        }

        /// <summary>
        /// Apply Minus modifier to FuzzyElement
        /// </summary>
        /// <param name="fuzzyElement">FuzzyElement</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement Minus(this IFuzzyElement fuzzyElement)
        {
            fuzzyElement.Value = Math.Pow(fuzzyElement.Value, 0.75);

            return fuzzyElement;
        }

        /// <summary>
        /// Apply Indeed modifier to FuzzyElement
        /// </summary>
        /// <param name="fuzzyElement">FuzzyElement</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement Indeed(this IFuzzyElement fuzzyElement)
        {
            fuzzyElement.X = Math.Round(fuzzyElement.X, 0);

            return fuzzyElement;
        }

        /// <summary>
        /// Apply Over modifier to FuzzyElement
        /// </summary>
        /// <param name="fuzzyElement">FuzzyElement</param>
        /// <param name="maxX">Max value of x</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement Over(this IFuzzyElement fuzzyElement, double maxX)
        {
            if (fuzzyElement.X >= maxX)
            {
                fuzzyElement.Value = 1 - fuzzyElement.Value;
                
                return fuzzyElement;
            }

            fuzzyElement.Value = 0;

            return fuzzyElement;
        }

        /// <summary>
        /// Apply Under modifier to FuzzyElement
        /// </summary>
        /// <param name="fuzzyElement">FuzzyElement</param>
        /// <param name="minX">Max value of x</param>
        /// <returns>FuzzyElement</returns>
        public static IFuzzyElement Under(this IFuzzyElement fuzzyElement, double minX)
        {
            if (fuzzyElement.X <= minX)
            {
                fuzzyElement.Value = 1 - fuzzyElement.Value;

                return fuzzyElement;
            }

            fuzzyElement.Value = 0;

            return fuzzyElement;
        }
    }
}
