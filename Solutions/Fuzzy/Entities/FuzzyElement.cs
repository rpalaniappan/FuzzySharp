// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FuzzyElement.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Entities
{
    using System;
    using System.Globalization;
    using Contracts.Entities;

    /// <summary>
    /// Implementação para FuzzyElement
    /// </summary>
    public class FuzzyElement : IFuzzyElement
    {
        #region Private Fields

        /// <summary>
        /// Degree of menbership
        /// </summary>
        private double _value;

        #endregion

        #region Properties

        /// <summary>
        /// Element of universe of discourse
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Degree of menbership
        /// </summary>
        public double Value
        {
            get { return this._value; }
            set { this._value = Math.Min(value, 1); }
        }

        /// <summary>
        /// Clone the object
        /// </summary>
        /// <returns>New cloned objetc</returns>
        public IFuzzyElement Clone()
        {
            return new FuzzyElement { Value = this.Value, X = this.X };
        }

        /// <summary>
        /// Clone IFuzzyElement and set it's values by function
        /// </summary>
        /// <param name="setValues">Function to set it's values</param>
        /// <returns>IFuzzyElement</returns>
        public IFuzzyElement Clone(Action<IFuzzyElement> setValues)
        {
            var fuzzy = this.Clone();
            setValues(fuzzy);
            
            return fuzzy;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Overload the operator ==
        /// </summary>
        /// <param name="elementA">FuzzyElement A</param>
        /// <param name="elementB">FuzzyElement B</param>
        /// <returns>True if equals</returns>
        public static bool operator ==(FuzzyElement elementA, FuzzyElement elementB)
        {
            return elementA.Equals(elementB);
        }

        /// <summary>
        /// Overload the operator !=
        /// </summary>
        /// <param name="elementA">FuzzyElement A</param>
        /// <param name="elementB">FuzzyElement B</param>
        /// <returns>True if equals</returns>
        public static bool operator !=(FuzzyElement elementA, FuzzyElement elementB)
        {
            return !(elementA == elementB);
        }

        #endregion

        #region IComparable Implementation

        /// <summary>
        /// Implementação do IComparable
        /// </summary>
        /// <param name="obj">Objeto a ser comparado</param>
        /// <returns>Resultado da comparação</returns>
        public int CompareTo(object obj)
        {
            var compar = (IFuzzyElement)obj;
            if (this.X == compar.X)
            {
                return 0;
            }

            if (this.X > compar.X)
            {
                return 1;
            }

            return -1;
        }

        #endregion

        #region Overrides for Equals, ToString and GetHashCode

        /// <summary>
        /// Override for Equals method
        /// </summary>
        /// <param name="obj">FuzzyElement object</param>
        /// <returns>True if equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var fuzzy = obj as FuzzyElement;

            return fuzzy != null && this.X == fuzzy.X && this.Value == fuzzy.Value;
        }

        /// <summary>
        /// Override for GetHashCode method
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            int hash = Convert.ToInt32(this.X);
            hash = (hash * 17) + this.Value.ToString().Length;
            hash *= 17;
            hash += this.Value.GetHashCode();

            return hash;
        }

        /// <summary>
        /// Override method ToString()
        /// </summary>
        /// <returns>String representing the object</returns>
        public override string ToString()
        {
            return string.Format("{0}/{1}", this.Value.ToString(CultureInfo.InvariantCulture), this.X);
        }

        #endregion
    }
}
