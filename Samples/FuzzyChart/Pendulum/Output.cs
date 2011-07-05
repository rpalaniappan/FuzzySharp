namespace FuzzyChart.Pendulum
{
    /// <summary>
    /// Output of system 
    /// </summary>
    public class Output
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="error">Error</param>
        /// <param name="delta">Delta Error</param>
        /// <param name="force">force</param>
        public Output(double error, double delta, double force)
        {
            this.DeltaError = delta;
            this.Error = error;
            this.Force = force;
        }

        /// <summary>
        /// Error
        /// </summary>
        public double Error { get; private set; }

        /// <summary>
        /// Delta Error
        /// </summary>
        public double DeltaError { get; private set; }

        /// <summary>
        /// Force
        /// </summary>
        public double Force { get; private set; }
    }
}
