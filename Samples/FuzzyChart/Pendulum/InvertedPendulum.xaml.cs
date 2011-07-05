using System;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using Fuzzy.Contracts.Entities;
using Visifire.Charts;

namespace FuzzyChart.Pendulum
{
    /// <summary>
    /// Interaction logic for PenduloInvertido.xaml
    /// </summary>
    public partial class InvertedPendulum
    {
        private readonly Pendulum _pendulo;

        public InvertedPendulum()
        {
            InitializeComponent();

            var pendulo = new Pendulum();

            pendulo.FuzzySetError.ToList().ForEach(x => this.AddLineSerie(x, this.chartError));
            pendulo.FuzzySetDeltaError.ToList().ForEach(x => this.AddLineSerie(x, this.chartDeltaError));
            pendulo.FuzzySetForce.ToList().ForEach(x => this.AddLineSerie(x, this.chartForce));

            this._pendulo = pendulo;
        }

        /// <summary>
        /// Update info
        /// </summary>
        /// <param name="output">Params</param>
        protected void UpdatePendulo(Output output)
        {
            penduloBox.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal,
                new Action(
                    delegate
                    {
                        double x2 = 0;
                        double y2 = 0;

                        penduloBox.Children.Clear();
                        x2 = penduloBox.Width / 2;
                        y2 = penduloBox.Height - 2;

                        int penduloSize = 130;

                        var angle = Math.Abs(output.Error);
                        var xt = Math.Sin(angle) * penduloSize;
                        var yt = Math.Cos(angle) * penduloSize;

                        var x1 = output.Error > 0 ? x2 + xt : x2 - xt;
                        var y1 = y2 - yt;

                        var line = new Line
                        {
                            Stroke = Brushes.LightSteelBlue,
                            X1 = x1,
                            X2 = x2,
                            Y1 = y1,
                            Y2 = y2,
                            HorizontalAlignment = HorizontalAlignment.Left,
                            VerticalAlignment = VerticalAlignment.Center,
                            StrokeThickness = 5
                        };

                        penduloBox.Children.Add(line);

                        txtError.Text = output.Error.ToString();
                        txtDelta.Text = output.DeltaError.ToString();
                        txtForce.Text = output.Force.ToString();

                    }));
        }

        /// <summary>
        /// Add a line serie
        /// </summary>
        /// <param name="fuzzySet">FuzzySet to represent in a chart</param>
        /// <param name="chart">The chart</param>
        protected void AddLineSerie(IFuzzySet fuzzySet, Chart chart)
        {
            this.AddGenericSerie(fuzzySet, RenderAs.Line, chart);
        }

        /// <summary>
        /// Add generic serie
        /// </summary>
        /// <param name="fuzzySet">FuzzySet to represent in a chart</param>
        /// <param name="render">The render.</param>
        /// <param name="chart">Chart</param>
        protected void AddGenericSerie(IFuzzySet fuzzySet, RenderAs render, Chart chart)
        {
            // Create a new instance of DataSeries
            var dataSeries = new DataSeries { RenderAs = render };

            foreach (var element in fuzzySet.Elements)
            {
                var dataPoint = new DataPoint
                {
                    XValue = element.Value.X,
                    YValue = element.Value.Value,
                    ShowInLegend = false
                };
                dataSeries.DataPoints.Add(dataPoint);
            }

            // Add dataSeries to Series collection.
            chart.Series.Add(dataSeries);
        }

        /// <summary>
        /// Click btStart
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void BtIniciarClick(object sender, RoutedEventArgs e)
        {
            btIniciar.IsEnabled = false;
            var thread = new Thread(this.Start);
            thread.Start();
        }

        /// <summary>
        /// Start the process 
        /// </summary>
        private void Start()
        {
            foreach (var output in this._pendulo.Calculate())
            {
                this.UpdatePendulo(output);
                Thread.Sleep(2000);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this._pendulo.Dispose();
        }
    }
}
