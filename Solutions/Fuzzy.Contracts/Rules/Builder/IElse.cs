// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IElse.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Rules.Builder
{
    /// <summary>
    /// Setup Else
    /// </summary>
    public interface IElse
    {
        /// <summary>
        /// Setup Else
        /// </summary>
        /// <returns>If</returns>
        IIf Else();
    }
}
