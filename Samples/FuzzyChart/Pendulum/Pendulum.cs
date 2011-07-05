using System;
using System.Collections.Generic;
using Fuzzy.Collections;
using Fuzzy.Contracts.Collections;
using Fuzzy.Contracts.Entities;
using Fuzzy.Contracts.Rules;
using Fuzzy.Defuzzifiers;
using Fuzzy.Entities;
using Fuzzy.Functions;
using Fuzzy.Rules.Builder;
using MathWorks.MATLAB.NET.Arrays;

namespace FuzzyChart.Pendulum
{
    /// <summary>
    /// Problema do Pendulo Invertido
    /// </summary>
    public class Pendulum : IDisposable
    {
        #region Fields

        /// <summary>
        /// Step in the universe of discouse
        /// </summary>
        private const double Step = 0.05;

        /// <summary>
        /// Round digits
        /// </summary>
        private const int RoundDigits = 2;

        private bool _continue;

        #endregion

        #region Constructor

        /// <summary>
        /// Construtor
        /// </summary>
        public Pendulum()
        {            
            this.FuzzySetError = new List<IFuzzySet>();
            this.FuzzySetDeltaError = new List<IFuzzySet>();
            this.FuzzySetForce = new List<IFuzzySet>();
            this.FuzzyValues = new FuzzyValues();

            this.CreateFuzzySetError();
            this.CreateFuzzySetDeltaError();
            this.CreateFuzzySetForce();
            this.CreateRules();
        }

        #endregion

        #region Properties

        /// <summary>
        /// FuzzySet Error
        /// </summary>
        public IList<IFuzzySet> FuzzySetError { get; set; }

        /// <summary>
        /// FuzzySet Delta Error
        /// </summary>
        public IList<IFuzzySet> FuzzySetDeltaError { get; set; }

        /// <summary>
        /// FuzzySet Force
        /// </summary>
        public IList<IFuzzySet> FuzzySetForce { get; set; }

        /// <summary>
        /// Values
        /// </summary>
        public IFuzzyValues FuzzyValues { get; set; }

        /// <summary>
        /// Algorithm
        /// </summary>
        public IFuzzyAlgorithm Algorithm { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate
        /// </summary>
        /// <returns>
        /// The output
        /// </returns>
        public IEnumerable<Output> Calculate()
        {
            _continue = true;

            yield return new Output(Math.PI / 4, Math.PI / 4, 0);

            var pendulum = new Pendulo.Pendulo();

            var param = new Dictionary<string, double>();

            MWArray forca = 0.0;
            MWArray angulo = Math.PI / 4;
            double lastAng = 0;

            for (int i = 0; _continue; i++)
            {
                var result = (MWNumericArray)pendulum.minvpe2(forca, angulo);
                double error = result[1].ToScalarDouble().Round(1);
                double delta = (lastAng - error).Round(1);

                param["error"] = error;
                param["deltaerror"] = delta;

                double fuzzyResult = this.Algorithm.CalculateResult(param);
                forca = fuzzyResult;
                angulo = error;
                lastAng = error;

                yield return new Output(error, delta, fuzzyResult);
            }

            pendulum.Dispose();
            yield break;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Degree of fulfillment
        /// </summary>
        /// <param name="fuzzySet">FuzzySet</param>
        /// <param name="fuzzyElement">FuzzyElement</param>
        /// <returns>FuzzySet</returns>
        private static IFuzzySet DegreeOfFulfillment(IFuzzySet fuzzySet, IFuzzyElement fuzzyElement)
        {
            var result = fuzzySet.GetNewEmpty();

            foreach (var element in fuzzySet)
            {
                result.AddElement(element.Min(fuzzyElement));
            }

            return result;
        }

        /// <summary>
        /// Create the rules
        /// </summary>
        private void CreateRules()
        {
            // cria as regras
            var builder = new RuleBuilder(this.FuzzyValues, Functions.Min, DegreeOfFulfillment,
                                          new CenterOfAreaDefuzzifier(Functions.Union));

            builder
                .If("error").Is("EN").And().If("deltaerror").Is("DMP").Then("output").Is("FMN").Else()
                .If("error").Is("EN").And().If("deltaerror").Is("DP").Then("output").Is("FN").Else()
                .If("error").Is("EN").And().If("deltaerror").Is("DZ").Then("output").Is("FZ").Else()
                .If("error").Is("EN").And().If("deltaerror").Is("DN").Then("output").Is("FN").Else()
                .If("error").Is("EZ").Then("output").Is("FZ").Else()
                .If("error").Is("EP").And().If("deltaerror").Is("DP").Then("output").Is("FP").Else()
                .If("error").Is("EP").And().If("deltaerror").Is("DZ").Then("output").Is("FZ").Else()
                .If("error").Is("EP").And().If("deltaerror").Is("DN").Then("output").Is("FP").Else()
                .If("error").Is("EP").And().If("deltaerror").Is("DMN").Then("output").Is("FMP");

            this.Algorithm = builder.FuzzyAlgorithm;
        }

        /// <summary>
        /// CreateFuzzySetError
        /// </summary>
        private void CreateFuzzySetError()
        {
            //armazena os FuzzySets e seu valor linguistico
            var values = this.FuzzyValues;

            // Valores para erro (angulo do pendulo)
            var set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(-6.30, -3.15, 0, x));
            set.AddRange((-3.15).GetRoundedRange(0, Step, RoundDigits));
            this.FuzzySetError.Add(set);
            values.AddFuzzyValue("EN", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(-0.50, 0, 0.50, x));
            set.AddRange((-0.50).GetRoundedRange(0.50, Step, RoundDigits));
            this.FuzzySetError.Add(set);
            values.AddFuzzyValue("EZ", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(0, 3.15, 6.30, x));
            set.AddRange(0d.GetRoundedRange(3.15, Step, RoundDigits));
            this.FuzzySetError.Add(set);
            values.AddFuzzyValue("EP", set);
        }

        /// <summary>
        /// CreateFuzzySetDeltaError
        /// </summary>
        private void CreateFuzzySetDeltaError()
        {
            //armazena os FuzzySets e seu valor linguistico
            var values = this.FuzzyValues;

            // Valores para delta erro (angulo do pendulo)
            var set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(-4.30, -3.15, -2.0, x));
            set.AddRange((-3.15).GetRoundedRange(-2.0, Step, RoundDigits));
            this.FuzzySetDeltaError.Add(set);
            values.AddFuzzyValue("DMN", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(-2.5, -1.25, 0, x));
            set.AddRange((-2.5).GetRoundedRange(0, Step, RoundDigits));
            this.FuzzySetDeltaError.Add(set);
            values.AddFuzzyValue("DN", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(-0.5, 0, 0.5, x));
            set.AddRange((-0.5).GetRoundedRange(0.5, Step, RoundDigits));
            this.FuzzySetDeltaError.Add(set);
            values.AddFuzzyValue("DZ", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(0.0, 1.25, 2.5, x));
            set.AddRange(0.0.GetRoundedRange(2.5, Step, RoundDigits));
            this.FuzzySetDeltaError.Add(set);
            values.AddFuzzyValue("DP", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(2.0, 3.15, 4.30, x));
            set.AddRange(2.0.GetRoundedRange(3.15, Step, RoundDigits));
            this.FuzzySetDeltaError.Add(set);
            values.AddFuzzyValue("DMP", set);

            //var set = new FuzzySet();
            //set.RegisterMembershipFunction(
            //    x => TipicalMembershipFunction.TriangularMembershipFunction(-6.30, -3.15, -0.30, x));
            //set.AddRange((-3.15).GetRoundedRange(-0.30, Step, RoundDigits));
            //this.FuzzySetDeltaError.Add(set);
            //values.AddFuzzyValue("DN", set);

            //set = new FuzzySet();
            //set.RegisterMembershipFunction(
            //    x => TipicalMembershipFunction.TriangularMembershipFunction(-0.30, 0, 0.30, x));
            //set.AddRange((-0.30).GetRoundedRange(0.30, Step, RoundDigits));
            //this.FuzzySetDeltaError.Add(set);
            //values.AddFuzzyValue("DZ", set);

            //set = new FuzzySet();
            //set.RegisterMembershipFunction(
            //    x => TipicalMembershipFunction.TriangularMembershipFunction(0.30, 3.15, 6.30, x));
            //set.AddRange(0.30.GetRoundedRange(3.15, Step, RoundDigits));
            //this.FuzzySetDeltaError.Add(set);
            //values.AddFuzzyValue("DP", set);
        }

        /// <summary>
        /// CreateFuzzySetDeltaError
        /// </summary>
        private void CreateFuzzySetForce()
        {
            //armazena os FuzzySets e seu valor linguistico
            var values = this.FuzzyValues;

            // Valores para delta erro (angulo do pendulo)
            var set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(-110, -70.0, -30.0, x));
            set.AddRange((-70.0).GetRoundedRange(-30.0, Step, RoundDigits));
            this.FuzzySetForce.Add(set);
            values.AddFuzzyValue("FMN", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(-60.0, -30.0, 0.0, x));
            set.AddRange((-60.0).GetRoundedRange(0.0, Step, RoundDigits));
            this.FuzzySetForce.Add(set);
            values.AddFuzzyValue("FN", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(-15, 0, 15, x));
            set.AddRange((-15.0).GetRoundedRange(15.0, Step, RoundDigits));
            this.FuzzySetForce.Add(set);
            values.AddFuzzyValue("FZ", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(0, 30, 60, x));
            set.AddRange(0d.GetRoundedRange(60, Step, RoundDigits));
            this.FuzzySetForce.Add(set);
            values.AddFuzzyValue("FP", set);

            set = new FuzzySet();
            set.RegisterMembershipFunction(
                x => TipicalMembershipFunction.TriangularMembershipFunction(30, 70, 110, x));
            set.AddRange(30d.GetRoundedRange(70, Step, RoundDigits));
            this.FuzzySetForce.Add(set);
            values.AddFuzzyValue("FMP", set);
        }

        #endregion

        #region IDisposable Implementation

        public void Dispose()
        {
            this._continue = false;            
        }

        #endregion
    }
}
