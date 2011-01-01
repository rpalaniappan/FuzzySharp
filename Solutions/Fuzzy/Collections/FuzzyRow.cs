// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FuzzyRow.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Collections
{
    using System.Collections.Generic;
    using Contracts.Collections;
    using Contracts.Entities;

    /// <summary>
    /// Representation of a Row of fuzzy element
    /// </summary>
    public class FuzzyRow : List<IFuzzyElement>, IFuzzyRow
    {
        /// <summary>
        /// Value on the universe of discourse
        /// </summary>
        public double X { get; set; }
    }
}
