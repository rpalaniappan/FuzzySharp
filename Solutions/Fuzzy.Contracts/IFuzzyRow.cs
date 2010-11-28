// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFuzzyRow.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts
{
    using System.Collections.Generic;

    /// <summary>
    /// Fuzzy Row of element
    /// </summary>
    public interface IFuzzyRow : IList<IFuzzyElement>
    {
        /// <summary>
        /// Value on the universe of discourse
        /// </summary>
        double X { get; set; }
    }
}
