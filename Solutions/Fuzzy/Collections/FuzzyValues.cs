// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FuzzyValues.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Collections;
    using Contracts.Entities;

    /// <summary>
    /// Representation of fuzzy values
    /// </summary>
    public class FuzzyValues : IFuzzyValues
    {
        /// <summary>
        /// store the fuzzy values
        /// </summary>
        private readonly IDictionary<string, IFuzzySet> _values;

        /// <summary>
        /// Constructor
        /// </summary>
        public FuzzyValues()
        {
            this._values = new Dictionary<string, IFuzzySet>();
        }

        /// <summary>
        /// Add a fuzzy value
        /// </summary>
        /// <param name="name">Linguistic meaning of the value</param>
        /// <param name="fuzzySet">FuzzySet of the value</param>
        public void AddFuzzyValue(string name, IFuzzySet fuzzySet)
        {
            this._values.Add(name, fuzzySet);
        }

        /// <summary>
        /// Get the FuzzySet for a fuzzy value
        /// </summary>
        /// <param name="name">FuzzyValue name</param>
        /// <returns>FuzzySet associated with the value</returns>
        public IFuzzySet GetFuzzyValue(string name)
        {
            if (!this._values.ContainsKey(name))
            {
                throw new ArgumentException("The value does not exists in this collection.");
            }

            return this._values[name];
        }

        /// <summary>
        /// Remove fuzzy value
        /// </summary>
        /// <param name="name">FuzzyValue name</param>
        /// <returns>FuzzySet associated with the value</returns>
        public IFuzzySet RemoveFuzzyValue(string name)
        {
            if (!this._values.ContainsKey(name))
            {
                throw new ArgumentException("The value does not exists in this collection.");
            }

            var result = this._values[name];
            
            this._values.Remove(name);

            return result;
        }
    }
}
