// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TipicalMembershipFunction.cs" company="I9 Tecnologia da Informação">
// Fuzzy Library Project  
// </copyright>
// <summary>
//   Interface for a FuzzyElement
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Fuzzy.Functions
{
    using System;

    /// <summary>
    /// Class to provide tipical membership functions
    /// </summary>
    public static class TipicalMembershipFunction
    {
        /// <summary>
        /// Zadeh Membership function (1 / (1 + Math.Pow(x / div, exp)))
        /// </summary>
        /// <param name="x">X value</param>
        /// <param name="exp">Exp value</param>
        /// <param name="div">Div value</param>
        /// <returns>Degree of membership</returns>
        public static double ZadehMembershipFunction(double x, double exp, double div)
        {
            return 1 / (1 + Math.Pow(x / div, exp));
        }
    }
}
