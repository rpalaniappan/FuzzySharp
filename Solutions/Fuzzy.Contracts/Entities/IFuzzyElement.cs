// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFuzzyElement.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Entities
{
    using System;

    /// <summary>
    /// Interface for a FuzzyElement
    /// </summary>
    public interface IFuzzyElement : IComparable
    {
        /// <summary>
        /// Element of universe of discourse
        /// </summary>
        double X { get; set; }

        /// <summary>
        /// Degree of menbership
        /// </summary>
        double Value { get; set; }

        /// <summary>
        /// Clone the element
        /// </summary>
        /// <returns>A new clone element</returns>
        IFuzzyElement Clone();

        /// <summary>
        /// Clone the element
        /// </summary>
        /// <param name="setValues">
        /// The set Values.
        /// </param>
        /// <returns>
        /// A new clone element
        /// </returns>
        IFuzzyElement Clone(Action<IFuzzyElement> setValues);
    }
}
