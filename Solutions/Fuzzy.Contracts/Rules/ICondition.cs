// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICondition.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Rules
{
    using Entities;

    /// <summary>
    /// Conditin on LHS for Fuzzy Algorithm
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Variable Name
        /// </summary>
        string Variable { get; set; }

        /// <summary>
        /// Condition Name
        /// </summary>
        string FuzzySetName { get; set; }

        /// <summary>
        /// FuzzySet
        /// </summary>
        IFuzzySet FuzzySet { get; set; }

        /// <summary>
        /// Verify if the rule matches
        /// </summary>
        /// <param name="value">Value for match</param>
        /// <returns>True if matches</returns>
        bool Matches(double value);
    }
}
