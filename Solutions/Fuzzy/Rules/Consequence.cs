// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Consequence.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Rules
{
    using Contracts.Entities;
    using Contracts.Rules;

    /// <summary>
    /// Conditin on LHS for Fuzzy Algorithm
    /// </summary>
    public class Consequence : IConsequence
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Consequence()
        {
        }

        /// <summary>
        /// Variable Name
        /// </summary>
        public string Variable { get; set; }

        /// <summary>
        /// FuzzySet
        /// </summary>
        public IFuzzySet FuzzySet { get; set; }
    }
}
