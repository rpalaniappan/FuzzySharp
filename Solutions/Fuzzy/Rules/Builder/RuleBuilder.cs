// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleBuilder.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   RuleBuilder
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Fuzzy.Contracts.Defuzzifier;

namespace Fuzzy.Rules.Builder
{
    using System;
    using System.Collections.Generic;
    using Contracts.Collections;
    using Contracts.Entities;
    using Contracts.Functions;
    using Contracts.Rules;
    using Contracts.Rules.Builder;

    /// <summary>
    /// Rule builder
    /// </summary>
    public class RuleBuilder : IIf, IIs, IClause, IResult, IElse
    {
        #region Private Fields

        /// <summary>
        /// Fuzzy Values
        /// </summary>
        private readonly IFuzzyValues _fuzzyValues;

        /// <summary>
        /// Store last condition
        /// </summary>
        private ICondition _lastCondition;

        /// <summary>
        /// Store last rule
        /// </summary>
        private IRule _lastRule;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fuzzyValues">
        /// The fuzzy Values.
        /// </param>
        /// <param name="connectiveAnd">Function for connective and</param>
        /// <param name="degreeOfFulfillment">degree of Fulfillment</param>
        /// <param name="defuzzifier">Defuzzifier</param>
        /// <returns>
        /// New FuzzyAlgorithm
        /// </returns>
        public RuleBuilder(IFuzzyValues fuzzyValues, 
                           IGenericElementFromFuzzyElementsFunction connectiveAnd,
                           IGenericDegreeOfFulfillmentFunction degreeOfFulfillment,
                           IDefuzzifier defuzzifier)
        {
            this.FuzzyAlgorithm = new FuzzyAlgorithm(connectiveAnd, degreeOfFulfillment, defuzzifier);
            this._fuzzyValues = fuzzyValues;
            this._lastRule = new Rule();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fuzzyValues">
        /// The fuzzy Values.
        /// </param>
        /// <param name="connectiveAnd">Function for connective and</param>
        /// <param name="degreeOfFulfillment">degree of Fulfillment</param>
        /// <param name="defuzzifier">Defuzzifier</param>
        /// <returns>
        /// New FuzzyAlgorithm
        /// </returns>
        public RuleBuilder(IFuzzyValues fuzzyValues,
                           Func<List<IFuzzyElement>, IFuzzyElement> connectiveAnd, 
                           Func<IFuzzySet, IFuzzyElement, IFuzzySet> degreeOfFulfillment,
                           IDefuzzifier defuzzifier)
        {
            this.FuzzyAlgorithm = new FuzzyAlgorithm(connectiveAnd, degreeOfFulfillment, defuzzifier);
            this._fuzzyValues = fuzzyValues;
            this._lastRule = new Rule();
        }

        #endregion

        #region Properties

        /// <summary>
        /// FuzzyAlgorithm 
        /// </summary>
        public IFuzzyAlgorithm FuzzyAlgorithm { get; private set; }

        #endregion

        #region Configuration Interfaces Implementation

        /// <summary>
        /// If clause
        /// </summary>
        /// <param name="variable">Variable name</param>
        /// <returns>Is clause</returns>
        public IIs If(string variable)
        {
            this._lastCondition = new Condition() { Variable = variable };

            return this;
        }

        /// <summary>
        /// Is clause
        /// </summary>
        /// <param name="value">value</param>
        /// <returns>Cluase clause</returns>
        IClause IIs.Is(string value)
        {
            var fuzzySet = this._fuzzyValues.GetFuzzyValue(value);
            this._lastCondition.FuzzySet = fuzzySet;
            this._lastCondition.FuzzySetName = value;

            return this;
        }

        /// <summary>
        /// And clause
        /// </summary>
        /// <returns>If clause</returns>
        public IIf And()
        {
            this._lastRule.Conditions.Add(this._lastCondition);

            return this;
        }

        /// <summary>
        /// Then clause
        /// </summary>
        /// <param name="output">variable output name</param>
        /// <returns>Result clause</returns>
        public IResult Then(string output)
        {
            this._lastRule.Conditions.Add(this._lastCondition);

            this._lastRule.Consequence.Variable = output;

            return this;
        }

        /// <summary>
        /// Is Clause
        /// </summary>
        /// <param name="value">variable name</param>
        /// <returns>Else clause</returns>
        IElse IResult.Is(string value)
        {
            var fuzzySet = this._fuzzyValues.GetFuzzyValue(value);
            this._lastRule.Consequence.FuzzySet = fuzzySet;

            this.FuzzyAlgorithm.Rules.Add(this._lastRule);
            this._lastRule.RuleNumber = this.FuzzyAlgorithm.Rules.Count;

            return this;
        }

        /// <summary>
        /// Else clause
        /// </summary>
        /// <returns>IF clause</returns>
        public IIf Else()
        {
            this._lastRule = new Rule();

            return this;
        }

        #endregion
    }
}
