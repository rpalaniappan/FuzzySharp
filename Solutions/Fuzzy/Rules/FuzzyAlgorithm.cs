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
    using Contracts.Defuzzifier;
    using Contracts.Entities;
    using Contracts.Functions;
    using Contracts.Rules;

    /// <summary>
    /// FuzzyAlgorithm
    /// </summary>
    public class FuzzyAlgorithm : IFuzzyAlgorithm
    {
        #region Private Fields

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
        /// Store functions reference
        /// </summary>
        private readonly List<object> _functions;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectiveAnd">Function for connective and</param>
        /// <param name="degreeOfFulfillment">degree of Fulfillment</param>
        /// <param name="defuzzifier">Defuzzifier</param>
        public FuzzyAlgorithm(IGenericElementFromFuzzyElementsFunction connectiveAnd,
                              IGenericDegreeOfFulfillmentFunction degreeOfFulfillment, 
                              IDefuzzifier defuzzifier)
        {
            this._functions = new List<object> { connectiveAnd, degreeOfFulfillment };

            this.Defuzzifier = defuzzifier;
            this._and = connectiveAnd.Execute;
            this._degreeOfFulfillment = degreeOfFulfillment.Execute;
            this._rules = new List<IRule>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectiveAnd">Function for connective and</param>
        /// <param name="degreeOfFulfillment">degree of Fulfillment</param>
        /// <param name="defuzzifier">Defuzzifier</param>
        public FuzzyAlgorithm(Func<List<IFuzzyElement>, IFuzzyElement> connectiveAnd,
                              Func<IFuzzySet, IFuzzyElement, IFuzzySet> degreeOfFulfillment,
                              IDefuzzifier defuzzifier)
        {
            this.Defuzzifier = defuzzifier;
            this._and = connectiveAnd;
            this._degreeOfFulfillment = degreeOfFulfillment;
            this._rules = new List<IRule>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Defuzzifier
        /// </summary>
        public IDefuzzifier Defuzzifier { get; set; }

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

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate the fuzzySet result
        /// </summary>
        /// <param name="values">Parameters and values</param>
        /// <returns>Result fuzzySet</returns>
        public double CalculateResult(IDictionary<string, double> values)
        {
            var results = this.CalculateFuzzyResult(values);

            return this.Defuzzifier.Defuzzifier(results);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Calculate the fuzzySet result
        /// </summary>
        /// <param name="values">Parameters and values</param>
        /// <returns>Result fuzzySet</returns>
        private List<IFuzzySet> CalculateFuzzyResult(IDictionary<string, double> values)
        {
            var matcheds = this._rules.Where(x => x.Matches(values));

            var results = matcheds.Select(matched => this.CalculateResultArea(matched, values)).ToList();

            return results;
        }

        /// <summary>
        /// Calculate the result fuzzy
        /// </summary>
        /// <param name="rule">Rule</param>
        /// <param name="values">values</param>
        /// <returns>Result fuzzy set</returns>
        private IFuzzySet CalculateResultArea(IRule rule, IEnumerable<KeyValuePair<string, double>> values)
        {
            var results = from ICondition condition in rule.Conditions
                          join value in values on condition.Variable equals value.Key
                          select condition.FuzzySet.Elements[value.Value];

            var element = this.ConnectiveAnd(results.ToList());

            return this.DegreeOfFulfillment(rule.Consequence.FuzzySet, element);
        }

        #endregion
    }
}
