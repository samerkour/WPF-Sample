using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LiveCharts;
using LiveCharts.Configurations;
using LiveCharts.Defaults;
using WPF_Presentation.ViewModel;

namespace WPF_Presentation.Control
{
    /// <summary>
    /// Interaction logic for PointStateExample.xaml
    /// </summary>
    public partial class PointStateExample : UserControl
    {
        protected DataReceiver _dataReceiver;
        public PointStateExample()
        {
            InitializeComponent();

            _dataReceiver = new DataReceiver();
            _dataReceiver.DataReceived += OnDataReceiver;

            var r = new Random();

            Values = new ChartValues<ObservableValue>
            {
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400)),
                new ObservableValue(r.Next(10, 400))
            };

            Title = "Point State Chart";

            PointCount = Values.Count;

            //Lets define a custom mapper, to set fill and stroke
            //according to chart values...
            Mapper = Mappers.Xy<ObservableValue>()
                .X((item, index) => index)
                .Y(item => item.Value)
                .Fill(item => item.Value > 200 ? DangerBrush : null)
                .Stroke(item => item.Value > 200 ? DangerBrush : null);

            Formatter = x => x + " ms";

            DangerBrush = new SolidColorBrush(Color.FromRgb(238, 83, 80));

            DataContext = this;
        }

        public string? Title { get; set; }
        public int? PointCount { get; set; } 

        public Func<double, string> Formatter { get; set; }
        public ChartValues<ObservableValue> Values { get; set; }
        public Brush DangerBrush { get; set; }
        public CartesianMapper<ObservableValue> Mapper { get; set; }

        private void UpdateDataOnClick(object sender, RoutedEventArgs e)
        {
            var r = new Random();
            foreach (var observable in Values)
            {
                observable.Value = r.Next(10, 400);
            }
        }


        private void ButtonStartDataReceive_Click(object sender, RoutedEventArgs e)
        {

            var threadParameters = new System.Threading.ThreadStart(delegate { ChartUpdateSafe("This text was set safely."); });
            var thread2 = new System.Threading.Thread(threadParameters);
            thread2.Start();
        }

        /// <summary>
        /// Safe cross-thread call Use the Invoke method.
        /// Reference: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/controls/how-to-make-thread-safe-calls?view=netdesktop-6.0
        /// </summary>
        /// <param name="text"></param>
        public void ChartUpdateSafe(string text)
        {

            //_dataReceiver.DataReceived += _chartModel.OnDataReceiver;

            _dataReceiver.Start();

            //if (MainChart.InvokeRequired)
            //{
            //    // Call this same method but append THREAD2 to the text
            //    Action safeWrite = delegate { WriteTextSafe($"{text} (THREAD2)"); };
            //    textBox1.Invoke(safeWrite);
            //}
            //else
            //    textBox1.Text = text;
        }


        public void OnDataReceiver(object source, EventArgs e)
        {
            //MessageBox.Show("New Data Received ...");
            var r = new Random();
            foreach (var observable in Values)
            {
                observable.Value = r.Next(10, 400);
            }
        }

        private void LoadXMLOnClick(object sender, RoutedEventArgs e)
        {
            var data = new ChartDataViewModel();

            Title = data.Title;
            PointCount = data.Count;

            for (int i=0; i < Values.Count; i++)
            {
                Values[i].Value = data.ChartValues[i].Value;
            }

        }
    }
}
