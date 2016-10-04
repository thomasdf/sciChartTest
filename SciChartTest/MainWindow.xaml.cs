using SciChart.Charting.Model.DataSeries;
using SciChart.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SciChartTest {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {

		XyDataSeries<double, double> series = new XyDataSeries<double, double>();

		public MainWindow() {
			Loaded += MainWindow_OnLoaded;
            InitializeComponent();
		}

		private void MainWindow_OnLoaded(object sender, RoutedEventArgs e) {
			// GrowBy expands a gap around the data when zooming to extents
			sciChartSurface.DebugWhyDoesntSciChartRender = false;
			sciChartSurface.YAxis.GrowBy = new DoubleRange(0.2, 0.2);

			// VisibleRange may be set explicitly, or use AutoRange to zoom to extents
			sciChartSurface.XAxis.VisibleRange = new DoubleRange(0, 100000);
			sciChartSurface.YAxis.VisibleRange = new DoubleRange(-2, 2);

			// Add a data series and some data
			series.SeriesName = "Sinewave";
			series.AcceptsUnsortedData = true;

			// Note: Can also be set in Xaml, inside the tag
			lineSeries.DataSeries = series;
			for(int i = 0; i < 100000; i += 100) {
				produceData(100, i, 100000);
			}

		}

		public async void produceData(int timeout, double index, int totalcount) {
			double[] dataArray = await createData(timeout, index, totalcount);
			for(int i = 1; i<dataArray.Length; i++) {
				series.Append(index + i, dataArray[i]);
			}
		}

		public Task<double[]> createData(int timeout, double index, int totalcount) {
			return Task.Run(() => {
				Random rnd = new Random();
				Thread.Sleep(rnd.Next(timeout));
				double[] resultArray = new double[100];
				for(int i = 0; i < 100; i++) {
					resultArray[i] = Math.Sin(2 * Math.PI * (index+i) / totalcount);
				}
				return resultArray;
			});
		}
	}
}
