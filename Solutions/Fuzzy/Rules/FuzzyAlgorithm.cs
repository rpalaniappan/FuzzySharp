// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FuzzyAlgorithm.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Entities;
    using Contracts.Rules;

    /// <summary>
    /// FuzzyAlgorithm
    /// </summary>
    public class FuzzyAlgorithm : IFuzzyAlgorithm
    {
        /// <summary>
        /// Connective Else
        /// </summary>
        private readonly Func<List<IFuzzySet>, IFuzzySet> _else;

        /// <summary>
        /// Connective AND
        /// </summary>
        private readonly Func<List<IFuzzyElement>, IFuzzyElement> _and;

        /// <summary>
        /// DegreeOfFulfillment
        /// </summary>
        private readonly Func<IFuzzySet, IFuzzyElement, IFuzzySet> _degreeOfFulfillment;

        /// <summary>
        /// Rules
        /// </summary>
        private readonly List<IRule> _rules;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectiveElse">Function for connective else</param>
        /// <param name="connectiveAnd">Function for connective and</param>
        /// <param name="degreeOfFulfillment">degree of Fulfillment</param>
        public FuzzyAlgorithm(Func<List<IFuzzySet>, IFuzzySet> connectiveElse,
                              Func<List<IFuzzyElement>, IFuzzyElement> connectiveAnd,
                              Func<IFuzzySet, IFuzzyElement, IFuzzySet> degreeOfFulfillment)
        {
            this._else = connectiveElse;
            this._and = connectiveAnd;
            this._degreeOfFulfillment = degreeOfFulfillment;
            this._rules = new List<IRule>();
        }

        /// <summary>
        /// Connective Else
        /// </summary>
        public Func<List<IFuzzySet>, IFuzzySet> ConnectiveElse
        {
            get { return this._else; }
        }

        /// <summary>
        /// Connective AND
        /// </summary>
        public Func<List<IFuzzyElement>, IFuzzyElement> ConnectiveAnd
        {
            get { return this._and; }
        }

        /// <summary>
        /// Connective AND
        /// </summary>
        public Func<IFuzzySet, IFuzzyElement, IFuzzySet> DegreeOfFulfillment
        {
            get { return this._degreeOfFulfillment; }
        }

        /// <summary>
        /// Rules
        /// </summary>
        public List<IRule> Rules
        {
            get { return this._rules; }
        }

        /// <summary>
        /// Calculate the fuzzySet result
        /// </summary>
        /// <param name="values">Parameters and values</param>
        /// <returns>Result fuzzySet</returns>
        public IFuzzySet CalculateResult(IDictionary<string, double> values)
        {
            var matcheds = this._rules.Where(x => x.Matches(values));

            var results = matcheds.Select(matched => this.CalculateResultArea(matched, values)).ToList();

            return this.ConnectiveElse(results);
        }

        private IFuzzySet CalculateResultArea(IRule rule, IDictionary<string, double> values)
        {
            var results = from ICondition condition in rule.Conditions
                          join value in values on condition.Variable equals value.Key
                          select condition.FuzzySet.Elements[value.Value];

            var element = this.ConnectiveAnd(results.ToList());

            return this.DegreeOfFulfillment(rule.Consequence.FuzzySet, element);
        }
    }
}
