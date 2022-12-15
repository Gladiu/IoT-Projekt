using Caliburn.Micro;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DesktopInterface.ViewModels
{
    public class ChartViewModel : Screen
    {
        public SeriesCollection _activeSeries;
        public SeriesCollection ActiveSeries { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        public ChartViewModel()
        {
            Labels = new[] { "White", "Yellow", "Orange", "Green", "Blue", "Purple", "Brown", "Red", "Black" };
            Formatter = value => value.ToString("N");
        }
    }
}
