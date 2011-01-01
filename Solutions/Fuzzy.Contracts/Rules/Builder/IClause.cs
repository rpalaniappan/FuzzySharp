// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClause.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Rules.Builder
{
    /// <summary>
    /// Setup Clause
    /// </summary>
    public interface IClause
    {
        /// <summary>
        /// Setup And
        /// </summary>
        /// <returns>If</returns>
        IIf And();

        /// <summary>
        /// Setup then
        /// </summary>
        /// <param name="output">Output variable</param>
        /// <returns>Result</returns>
        IResult Then(string output);
    }
}
