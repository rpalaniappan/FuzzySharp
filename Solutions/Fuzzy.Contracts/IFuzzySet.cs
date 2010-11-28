// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFuzzySet.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Interface for a Fuzzy Set
    /// </summary>
    public interface IFuzzySet : IEnumerable<IFuzzyElement>
    {
        /// <summary>
        /// Fuzzy Elements in universe of discourse
        /// </summary>
        SortedList<double, IFuzzyElement> Elements { get; set; }
        
        /// <summary>
        /// Register a membership function
        /// </summary>
        /// <param name="function">Function for a degree of membership</param>
        void RegisterMembershipFunction(Func<double, double> function);

        /// <summary>
        /// Calculate the membership degree
        /// </summary>
        /// <param name="x">Value in universe of discourse</param>
        /// <returns>The membership degree</returns>
        double CalculateMembershipDegree(double x);

        /// <summary>
        /// Add a new Fuzzy element
        /// </summary>
        /// <param name="x">Value in the universe of discourse</param>
        void Add(double x);

        /// <summary>
        /// Add a range of fuzzy elements
        /// </summary>
        /// <param name="xs">Values in the universe of discourse</param>
        void AddRange(double[] xs);

        /// <summary>
        /// Add elements in the fuzzy set
        /// </summary>
        /// <param name="elements">Elements to add</param>
        void AddElements(List<IFuzzyElement> elements);

        /// <summary>
        /// Add element in the fuzzy set
        /// </summary>
        /// <param name="element">Elements to add</param>
        void AddElement(IFuzzyElement element);

        /// <summary>
        /// Returns a new empty fuzzy set
        /// </summary>
        /// <returns>New empty fuzzy set</returns>
        IFuzzySet GetNewEmpty();
    }
}
