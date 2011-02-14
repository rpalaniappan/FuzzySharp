// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFuzzyAlgorithm.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Rules
{
    using System;
    using System.Collections.Generic;
    using Defuzzifier;
    using Entities;

    /// <summary>
    /// Representation of FuzzyAlgorithm
    /// </summary>
    public interface IFuzzyAlgorithm
    {
        /// <summary>
        /// Defuzzifier
        /// </summary>
        IDefuzzifier Defuzzifier { get; set; }

        /// <summary>
        /// Connective AND function
        /// </summary>
        Func<List<IFuzzyElement>, IFuzzyElement> ConnectiveAnd { get; }

        /// <summary>
        /// List of rules
        /// </summary>
        List<IRule> Rules { get; }

        /// <summary>
        /// Calculate the fuzzySet result
        /// </summary>
        /// <param name="values">Parameters and values</param>
        /// <returns>Crisp Value</returns>
        double CalculateResult(IDictionary<string, double> values);
    }
}
