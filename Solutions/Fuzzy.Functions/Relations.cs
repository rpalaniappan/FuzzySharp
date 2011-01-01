// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Relations.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Relations for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Fuzzy.Contracts.Collections;
using Fuzzy.Contracts.Entities;

namespace Fuzzy.Functions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;

    /// <summary>
    /// Relations
    /// </summary>
    public static class Relations
    {
        /// <summary>
        /// Get a Cartesian Product 
        /// </summary>
        /// <param name="fuzzySetA">
        /// FuzzySet A
        /// </param>
        /// <param name="fuzzySetB">
        /// FuzzySet B
        /// </param>
        /// <param name="function">
        /// Function to apply
        /// </param>
        /// <param name="rowBuilder">
        /// The row Builder.
        /// </param>
        /// <returns>
        /// The get cartesian product.
        /// </returns>
        public static List<IFuzzyRow> GetCartesianProduct(
            this IFuzzySet fuzzySetA,
            IFuzzySet fuzzySetB,
            Func<IFuzzyElement, IFuzzyElement, IFuzzyElement> function,
            Func<IFuzzyRow> rowBuilder)
        {
            var result = new List<IFuzzyRow>();

            foreach (var elementA in fuzzySetA)
            {
                var row = rowBuilder();
                row.X = elementA.X;
                foreach (var elementB in fuzzySetB)
                {
                    row.Add(function(elementB, elementA));
                }

                result.Add(row);
            }

            return result;
        }
    }
}
