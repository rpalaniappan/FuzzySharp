// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFuzzyValues.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Contracts.Collections
{
    using Entities;

    /// <summary>
    /// Representation of fuzzy values
    /// </summary>
    public interface IFuzzyValues
    {
        /// <summary>
        /// Add a fuzzy value
        /// </summary>
        /// <param name="name">Linguistic meaning of the value</param>
        /// <param name="fuzzySet">FuzzySet of the value</param>
        void AddFuzzyValue(string name, IFuzzySet fuzzySet);

        /// <summary>
        /// Get the FuzzySet for a fuzzy value
        /// </summary>
        /// <param name="name">FuzzyValue name</param>
        /// <returns>FuzzySet associated with the value</returns>
        IFuzzySet GetFuzzyValue(string name);

        /// <summary>
        /// Remove fuzzy value
        /// </summary>
        /// <param name="name">FuzzyValue name</param>
        /// <returns>FuzzySet associated with the value</returns>
        IFuzzySet RemoveFuzzyValue(string name);
    }
}
