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
    using Entities;

    /// <summary>
    /// Representation of FuzzyAlgorithm
    /// </summary>
    public interface IFuzzyAlgorithm
    {
        /// <summary>
        /// Connective ELSE function
        /// </summary>
        Func<List<IFuzzySet>, IFuzzySet> ConnectiveElse { get; }

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
        /// <returns>Result fuzzySet</returns>
        IFuzzySet CalculateResult(IDictionary<string, double> values);
    }
}
