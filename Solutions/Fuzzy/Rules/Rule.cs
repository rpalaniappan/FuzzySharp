// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Rule.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Rules
{
    using System.Collections.Generic;
    using Contracts.Rules;

    /// <summary>
    /// Rule
    /// </summary>
    public class Rule : IRule
    {
        /// <summary>
        /// List of conditons
        /// </summary>
        private readonly List<ICondition> _conditions;

        /// <summary>
        /// Constructor
        /// </summary>
        public Rule()
        {
            this._conditions = new List<ICondition>();
            this.Consequence = new Consequence();
        }

        /// <summary>
        /// Rule Number
        /// </summary>
        public int RuleNumber { get; set; }

        /// <summary>
        /// Conditions
        /// </summary>
        public IList<ICondition> Conditions
        {
            get { return this._conditions; }
        }

        /// <summary>
        /// FuzzySet Consequence
        /// </summary>
        public IConsequence Consequence { get; set; }

        /// <summary>
        /// Verify if all conditions match
        /// </summary>
        /// <param name="values">variables and values</param>
        /// <returns>
        /// True if matches
        /// </returns>
        public bool Matches(IDictionary<string, double> values)
        {
            return this._conditions.TrueForAll(x => x.Matches(values[x.Variable]));
        }
    }
}
