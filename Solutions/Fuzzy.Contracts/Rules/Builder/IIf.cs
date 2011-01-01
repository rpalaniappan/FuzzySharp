// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IIf.cs" company="I9 Tecnologia da Informação">
//   Fuzzy Library Project
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Rules.Builder
{
    /// <summary>
    /// Builder If
    /// </summary>
    public interface IIf
    {
        /// <summary>
        /// Setup If
        /// </summary>
        /// <param name="variable">Variable Name</param>
        /// <returns>Is</returns>
        IIs If(string variable);
    }
}
