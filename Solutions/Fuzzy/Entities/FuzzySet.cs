// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FuzzySet.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Entities
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Contracts.Entities;

    /// <summary>
    /// Implementation for a Fuzzy Set
    /// </summary>
    public class FuzzySet : IFuzzySet
    {
        #region Private Fields
        
        /// <summary>
        /// Delegate for a membership method
        /// </summary>
        private Delegate _membershipMethod;

        #endregion

        #region Constructor
        
        /// <summary>
        /// Initializes a new instance of the <see cref="FuzzySet"/> class. 
        /// </summary>
        public FuzzySet()
        {
            this.Elements = new SortedList<double, IFuzzyElement>();
        }

        #endregion

        #region Properties
        
        /// <summary>
        /// Fuzzy Elements in universe of discourse
        /// </summary>
        public SortedList<double, IFuzzyElement> Elements { get; set; }

        /// <summary>
        /// Get the max value of the universe of discourse
        /// </summary>
        public double MaxX
        {
            get { return this.Elements.Keys.LastOrDefault(); }
        }

        /// <summary>
        /// Get the min value of the universe of discouse
        /// </summary>
        public double MinX
        {
            get { return this.Elements.Keys.FirstOrDefault(); }
        }

        #endregion

        #region Public Methods
        
        /// <summary>
        /// Register a membership function
        /// </summary>
        /// <param name="function">Function for a degree of membership</param>
        public void RegisterMembershipFunction(Func<double, double> function)
        {
            this._membershipMethod = new Func<double, double>(function);
        }

        /// <summary>
        /// Calculate the membership degree
        /// </summary>
        /// <param name="x">Value in universe of discourse</param>
        /// <returns>The membership degree</returns>
        public double CalculateMembershipDegree(double x)
        {
            if (this._membershipMethod == null)
            {
                return 0;
            }

            return Convert.ToDouble(this._membershipMethod.DynamicInvoke(x));
        }

        /// <summary>
        /// Add a new Fuzzy element
        /// </summary>
        /// <param name="x">Value in the universe of discourse</param>
        public void Add(double x)
        {
            var element = new FuzzyElement { X = x, Value = this.CalculateMembershipDegree(x) };
            this.Elements.Add(element.X, element);
        }

        /// <summary>
        /// Add a range of fuzzy elements
        /// </summary>
        /// <param name="xs">Values in the universe of discourse</param>
        public void AddRange(double[] xs)
        {
            foreach (double t in xs)
            {
                this.Add(t);
            }
        }

        /// <summary>
        /// Add elements in the fuzzy set
        /// </summary>
        /// <param name="elements">Elements to add</param>
        public void AddElements(List<IFuzzyElement> elements)
        {
            elements.ForEach(e => this.Elements.Add(e.X, e));
        }

        /// <summary>
        /// Add element in the fuzzy set
        /// </summary>
        /// <param name="element">Elements to add</param>
        public void AddElement(IFuzzyElement element)
        {
            this.Elements.Add(element.X, element);
        }

        /// <summary>
        /// Returns a new empty fuzzy set
        /// </summary>
        /// <returns>New empty fuzzy set</returns>
        public IFuzzySet GetNewEmpty()
        {
            return new FuzzySet();
        }

        #endregion

        #region Override Methods

        /// <summary>
        /// GetEnumerator for FuzzySet
        /// </summary>
        /// <returns>IFuzzyElement</returns>
        public IEnumerator<IFuzzyElement> GetEnumerator()
        {
            return this.Elements.Select(fuzzyElement => fuzzyElement.Value).GetEnumerator();
        }

        /// <summary>
        /// Override ToString method
        /// </summary>
        /// <returns>String representig the object</returns>
        public override string ToString()
        {
            return string.Join(", ", this.Elements.Values);
        }

        /// <summary>
        /// GetEnumerator for FuzzySet
        /// </summary>
        /// <returns>IFuzzyElement</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion
    }
}
