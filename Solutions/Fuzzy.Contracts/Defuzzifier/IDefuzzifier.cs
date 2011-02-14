// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDefuzzifier.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Defuzzifier
{
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    /// Defuzzifier
    /// </summary>
    public interface IDefuzzifier
    {
        /// <summary>
        /// Last FuzzySet Result
        /// </summary>
        IFuzzySet LastResult { get; }

         /// <summary>
         /// Defuzzifie the value
         /// </summary>
         /// <param name="fuzzySets">FuzzySets matched rules</param>
         /// <returns>Crisp value</returns>
        double Defuzzifier(List<IFuzzySet> fuzzySets);
    }
}
