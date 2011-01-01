// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRule.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Rules
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Representation of a fuzzy rule
    /// </summary>
    public interface IRule
    {
        /// <summary>
        /// Rule Number
        /// </summary>
        int RuleNumber { get; set; }

        /// <summary>
        /// List of conditions on LHS
        /// </summary>
        IList<ICondition> Conditions { get; }

        /// <summary>
        /// The consequence of the rule (RHS)
        /// </summary>
        IConsequence Consequence { get; set; }

        /// <summary>
        /// Verify if the rule matches
        /// </summary>
        /// <param name="values">variables and values</param>
        /// <returns>
        /// True if matches
        /// </returns>
        bool Matches(IDictionary<string, double> values);
    }
}







