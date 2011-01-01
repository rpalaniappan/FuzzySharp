﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Compositor.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Linq;
using Fuzzy.Contracts.Collections;
using Fuzzy.Contracts.Entities;

namespace Fuzzy.Functions
{
    using System.Collections.Generic;
    using Contracts;

    /// <summary>
    /// Class to provide matrix composition
    /// </summary>
    public static class Compositor
    {
        /// <summary>
        /// Get Max-Min Composition
        /// </summary>
        /// <param name="fuzzySet">
        /// The fuzzySet
        /// </param>
        /// <param name="matrix">
        /// The matrix of relation
        /// </param>
        /// <returns>
        /// The max min composition.
        /// </returns>
        public static IFuzzySet MaxMinComposition(this IFuzzySet fuzzySet, List<IFuzzyRow> matrix)
        {
            var join = from IFuzzyRow row in matrix join IFuzzyElement element in fuzzySet on row.X equals element.X select row;
            
            var result = fuzzySet.GetNewEmpty();

            foreach (var row in join)
            {
                foreach (var element in row)
                {
                    var min = element.Min(fuzzySet.Elements[row.X]);

                    if (result.Elements.ContainsKey(min.X))
                    {
                        result.Elements[min.X] = result.Elements[min.X].Max(min);
                    }
                    else
                    {
                        result.AddElement(min);
                    }
                }
            }

            return result;
        }
    }
}
