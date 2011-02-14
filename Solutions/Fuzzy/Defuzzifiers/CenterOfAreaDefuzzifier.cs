// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CenterOfAreaDefuzzifier.cs" company="I9 Tecnologia da Informação">
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
    /// Defuzzifier using center of area (COA) method
    /// </summary>
    public class CenterOfAreaDefuzzifier : IDefuzzifier
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
        public CenterOfAreaDefuzzifier(Func<List<IFuzzySet>, IFuzzySet> connectiveElse)
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
            if (fuzzySets.Count == 0)
            {
                return 0;
            }

            var fuzzySet = this._connectiveElse(fuzzySets);
            this._lastResult = fuzzySet;

            var upper = fuzzySet.Select(x => x.Value * x.X).Sum();
            var lower = fuzzySet.Select(x => x.Value).Sum();

            return upper / lower;
        }
    }
}
