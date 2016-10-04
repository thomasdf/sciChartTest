using System;
using System.Threading;

namespace SciChartTest {
	internal class DataCreator {
		MainWindow Recepient;
		int timeOut;
		int dataCount;

		public DataCreator(MainWindow sender, int timeOut, int dataCount) {
			Recepient = sender;
			this.timeOut = timeOut;
			this.dataCount = dataCount;
		}

		public async void createData(int timeout, int dataCount = 1000) {
			double data;
			for(int i = 0; i < dataCount; i++) {
				data = Math.Sin(2 * Math.PI * i / dataCount);
				Thread.Sleep(timeOut);
            }
        }
	}
}