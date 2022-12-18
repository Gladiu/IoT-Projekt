using Caliburn.Micro;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using DesktopInterface.Control;
using System.Reflection;
using System.Reflection.PortableExecutable;
using DataTypes;
using System.ComponentModel.DataAnnotations;
using System.Windows.Threading;
using System.Diagnostics.Metrics;
using OxyPlot.Axes;
using System.Windows.Media.Media3D;
using AxisPosition = OxyPlot.Axes.AxisPosition;

namespace DesktopInterface.ViewModels
{
    public class ChartViewModel : Screen
    {
        private DispatcherTimer _timer;

        private int _createdSamples;

        private string _selectedUnit;

        private string _selectedType;

        private List<string> _units;

        private List<string> _dataTypes;

        private int _samples;

        private float _samplingTime;

        private float _time;

        private LineSeries _data;

        private PlotModel _plot;

        public PlotModel Plot 
        { 
            get 
            {
                return _plot;
            } 
            set 
            {
                _plot = value;
                NotifyOfPropertyChange(() => Plot);
            } 
        }

        public List<string> DataTypes
        {
            get 
            {
                return _dataTypes;
            }
            set 
            {
                _dataTypes = value;
                NotifyOfPropertyChange(() => DataTypes);
            } 
        }

        public float SamplingTime
        {
            get
            {
                return _samplingTime;
            }
            set
            {
                if (value >= 99 && value <= 10000)
                {
                    _samplingTime = value;
                    UpdateTimerInstance();
                    NotifyOfPropertyChange(() => SamplingTime);
                }
            }
        }

        public string SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                ChangePlottedData(value);
                _selectedType = value;
                NotifyOfPropertyChange(() => SelectedType);
            }
        }

        public List<string> Units
        {
            get 
            {
                return _units;
            }
            set 
            {
                _units = value;
            }
        }

        public string SelectedUnit
        {
            get
            {
                return _selectedUnit;
            }
            set
            {
                _selectedUnit = value;
                UpdateUnit();
                NotifyOfPropertyChange(() => SelectedUnit);
            }
        }

        public ChartViewModel()
        {
            _plot = new PlotModel { Title = "Data" };
            _plot.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
            _samples = 10;
            _samplingTime = 1000;
            _selectedType = "";
            _dataTypes = new List<string>();
        }

        public ChartViewModel(List<DataStruct> dataStructs)
        {
            _samples = ApplicationConfiguration.SamplesCount;
            _samplingTime = ApplicationConfiguration.SamplingTime;
            _createdSamples = 0;
            _dataTypes = new List<string>();
            if (dataStructs != null)
            {
                foreach (var data in dataStructs)
                {
                    _dataTypes.Add(data.name);
                }
            }

            _units = dataStructs.FirstOrDefault().units;
            _selectedUnit = dataStructs.FirstOrDefault().defaultUnit;
            _selectedType = _dataTypes[0];
            ChangePlottedData(_dataTypes?.FirstOrDefault());
            DispatchTimer();
        }

        private void UpdateData()
        {
            ApiHelper.GetDataObjectsList("index.php").ContinueWith(result =>
            {
                var dataObjects = result.Result;
                foreach (var dataObject in dataObjects)
                {
                    if (dataObject.name == _selectedType)
                    {
                        _plot.Title = _selectedType;
                        var s = (LineSeries)_plot.Series?.FirstOrDefault();
                        if (_createdSamples < _samples)
                        {
                            s.Points.Add(new DataPoint(_time, dataObject.value));
                            _createdSamples++;
                            _data = s;
                        }
                        else if (_createdSamples >= _samples)
                        {
                            var dataPoint = new DataPoint(_time, dataObject.value);
                            s.Points.Add(dataPoint);
                            s.Points.RemoveAt(0);
                            _data = s;
                            _createdSamples++;
                        }
                    }
                }
                _time = _time + _samplingTime / 1000;

                Plot = _plot;
                Plot.InvalidatePlot(true); ;
            });
        }

        private void DispatchTimer()
        {
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(UpdateTimer_Tick);
            int interval = (int)_samplingTime;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            _timer.Start();
        }

        private void UpdateTimerInstance() 
        {
            _timer.Stop();
            _timer = new DispatcherTimer();
            _timer.Tick += new EventHandler(UpdateTimer_Tick);
            int interval = (int)_samplingTime;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            _timer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            UpdateData();
        }

        private void ChangePlottedData(string type) 
        {
            _plot = new PlotModel { Title = type };
            _plot.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });
            _plot.Axes.Add(new LinearAxis() { Title = $"{_selectedUnit}", Position = AxisPosition.Left });
            _plot.Axes.Add(new LinearAxis() { Title = $"time[s]", Position = AxisPosition.Bottom });
            //_plot.Series.Add(_data);
            _createdSamples = 0;
            Plot = _plot;
        }

        private void UpdateUnit() 
        {
            _plot = new PlotModel { Title = _selectedType };
            _plot.Series.Add(new LineSeries { LineStyle = LineStyle.Solid });
            _plot.Axes.Add(new LinearAxis() { Title = $"{_selectedUnit}", Position = AxisPosition.Left });
            _plot.Axes.Add(new LinearAxis() { Title = $"time[s]", Position = AxisPosition.Bottom });
            //_plot.Series.Add(_data);
            _createdSamples = 0;
            Plot = _plot;
        }
    }
}
