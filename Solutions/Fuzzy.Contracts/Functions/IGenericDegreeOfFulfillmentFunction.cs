// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IGenericDegreeOfFulfillmentFunction.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Functions
{
    using Entities;

    /// <summary>
    /// Execute generic fuzzy function
    /// </summary>
    public interface IGenericDegreeOfFulfillmentFunction
    {
        /// <summary>
        /// Generic Fuzzy function execution
        /// </summary>
        /// <param name="values">FuzzySets</param>
        /// <param name="element">fuzzy element</param>
        /// <returns>FuzzyElement</returns>
        IFuzzySet Execute(IFuzzySet values, IFuzzyElement element);
    }
}
