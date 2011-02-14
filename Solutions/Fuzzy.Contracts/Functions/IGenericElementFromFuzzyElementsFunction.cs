// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenericElementFromFuzzyElementsFunction.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Functions
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Execute generic fuzzy function
    /// </summary>
    public interface IGenericElementFromFuzzyElementsFunction
    {
        /// <summary>
        /// Generic Fuzzy function execution
        /// </summary>
        /// <param name="values">List of fuzzySets</param>
        /// <returns>FuzzyElement</returns>
        IFuzzyElement Execute(List<IFuzzyElement> values);
    }
}
