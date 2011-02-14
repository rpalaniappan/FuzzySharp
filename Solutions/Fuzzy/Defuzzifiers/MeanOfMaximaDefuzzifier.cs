// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeanOfMaximaDefuzzifier.cs" company="I9 Tecnologia da Informação">
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
    public class MeanOfMaximaDefuzzifier : IDefuzzifier
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
        public MeanOfMaximaDefuzzifier(Func<List<IFuzzySet>, IFuzzySet> connectiveElse)
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

            var max = (from IFuzzyElement element in fuzzySet
                       select element.Value).Max();

            var med = (from IFuzzyElement element in fuzzySet
                       where element.Value == max
                       select element.X).Average();

            return med;
        }
    }
}
