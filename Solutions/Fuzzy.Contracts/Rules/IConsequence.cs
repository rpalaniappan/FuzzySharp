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
    ///  Consequence on RHS for Fuzzy Algorithm
    /// </summary>
    public interface IConsequence
    {
        /// <summary>
        /// Variable Name
        /// </summary>
        string Variable { get; set; }

        /// <summary>
        /// FuzzySet
        /// </summary>
        IFuzzySet FuzzySet { get; set; }
    }
}
