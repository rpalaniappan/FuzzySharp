// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIs.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Rules.Builder
{
    /// <summary>
    /// Setup Is
    /// </summary>
    public interface IIs
    {
        /// <summary>
        /// Setup Is
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns>Clause</returns>
        IClause Is(string value);
    }
}
