// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Condition.cs" company="I9 Tecnologia da Informação">
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
    public class Condition : ICondition
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public Condition()
        {
        }

        /// <summary>
        /// Variable Name
        /// </summary>
        public string Variable { get; set; }

        /// <summary>
        /// FuzzySet Name
        /// </summary>
        public string FuzzySetName { get; set; }

        /// <summary>
        /// FuzzySet
        /// </summary>
        public IFuzzySet FuzzySet { get; set; }

        /// <summary>
        /// Verify if the rule matches
        /// </summary>
        /// <param name="value">Value for match</param>
        /// <returns>True if matches</returns>
        public bool Matches(double value)
        {
            return value <= this.FuzzySet.MaxX && value >= this.FuzzySet.MinX;
        }
    }
}
