// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CenterOfSumDefuzzifier.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Defuzzifier
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Defuzzifiers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Defuzzifier;
    using Contracts.Entities;

    /// <summary>
    /// Defuzzifier using center of sum (COS) method
    /// </summary>
    public class CenterOfSumDefuzzifier : IDefuzzifier
    {
        /// <summary>
        /// Connective Else
        /// </summary>
        private readonly Func<List<IFuzzySet>, IFuzzySet> _connectiveElse;

        /// <summary>
        /// Last Result
        /// </summary>
        private IFuzzySet _lastResult;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="connectiveElse">Connective Else Function</param>
        public CenterOfSumDefuzzifier(Func<List<IFuzzySet>, IFuzzySet> connectiveElse)
        {
            this._connectiveElse = connectiveElse;
        }

        /// <summary>
        /// Last Result
        /// </summary>
        public IFuzzySet LastResult
        {
            get { return this._lastResult; }
        }

        /// <summary>
        /// Defuzzifie the value
        /// </summary>
        /// <param name="fuzzySets">FuzzySets matched rules</param>
        /// <returns>Crisp value</returns>
        public double Defuzzifier(List<IFuzzySet> fuzzySets)
        {
            var fuzzySet = this._connectiveElse(fuzzySets);
            this._lastResult = fuzzySet;

            var keys = new List<double>();
            fuzzySets.ForEach(x => keys.AddRange(x.Elements.Keys.Select(y => y)));
            var distinctKeys = keys.Distinct().ToList();
            distinctKeys.Sort();

            var upper = distinctKeys.Select(
                x => x * fuzzySets.Where(
                    y => y.Elements.ContainsKey(x)).Select(z => z.Elements[x].Value).Sum()).Sum();

            var lower = distinctKeys.Select(
                x => fuzzySets.Where(
                    y => y.Elements.ContainsKey(x)).Select(z => z.Elements[x].Value).Sum()).Sum();

            return upper / lower;
        }
    }
}
