// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IResult.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Rules.Builder
{
    /// <summary>
    /// Setup result
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Setup Is Result
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Else</returns>
        IElse Is(string value);
    }
}
