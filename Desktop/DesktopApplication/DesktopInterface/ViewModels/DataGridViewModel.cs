using DataTypes;
using System;
using System.Collections.Generic;
using DesktopInterface.Control;
using Caliburn.Micro;
using System.Windows.Controls;
using System.Windows.Threading;
using DesktopInterface.Models;
using System.Windows.Data;

namespace DesktopInterface.ViewModels
{
    public class DataGridViewModel : Screen
    {
        private float _samplingTime;
        private DispatcherTimer _timer;
        private string _header = "Datasets downloaded from sensehat";
        private List<DataObject>? _dataObjects = new List<DataObject>();
        private List<List<string>>? _units;
        public List<DataStruct>? DataStructs;
        private List<string>? _selectedUnit;
        public DataGridViewModel()
        {
            _timer = new DispatcherTimer();
            _samplingTime = ApplicationConfiguration.SamplingTime;
            DispatchTimer();
        }

        public DataGridViewModel(List<DataStruct>? dataStructs)
        {
            _timer = new DispatcherTimer();
            _samplingTime = ApplicationConfiguration.SamplingTime;
            DispatchTimer();
            DataStructs = dataStructs;
            _units = new List<List<string>>();
            _selectedUnit = new List<string>();
            _dataObjects = new List<DataObject>();
            GridData = new Grid();
            if (dataStructs != null) 
            {
                foreach (var data in dataStructs)
                {
                    var units = data.units;
                    _units.Add(units);
                    _selectedUnit.Add(data.defaultUnit);
                }
            }
            NotifyOfPropertyChange(() => SelectedUnit);
        }

        public string Header
        {
            get 
            {
                return _header;
            }
            set 
            {
                _header = value;
                NotifyOfPropertyChange(() => Header);
            }
        }

        public List<DataObject>? DataObjects
        {
            get { return _dataObjects; }
            set 
            {
                _dataObjects = value;
                NotifyOfPropertyChange(() => DataObjects);
            }
        }

        public List<List<string>>? Units
        {
            get { return _units; }
            set { _units = value; NotifyOfPropertyChange(() => Units); }
        }

        public Grid? GridData { get; set; }

        public List<string>? SelectedUnit
        {
            get 
            {
                return _selectedUnit;
            }
            set 
            {
                _selectedUnit = value;
                NotifyOfPropertyChange(() => SelectedUnit);
            }
        }

        private void LoadData() 
        {
            ApiHelper.GetDataObjectsList("index.php").ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    DataObjects = new List<DataObject>(task.Result!);
                }
            });

        }
        private void DispatchTimer() 
        {
            _timer.Tick += new EventHandler(UpdateTimer_Tick);
            int interval = (int)_samplingTime;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            _timer.Start();
        }

        private void UpdateTimer_Tick(object? sender, EventArgs e)
        {
            LoadData();
        }

        public Binding GetCommandBinding(int x)
        {
            return new Binding($"SelectedUnit[{x}]");
        }

        public void SetDefaultUnits() 
        {
            ApiHelper.PostSelectedUnits("post/selected_units.json", SelectedUnit).ContinueWith(task => 
            {
                var result = task.Result;
            });
            for (int i = 0; i < SelectedUnit?.Count; i++) 
            {
                WindowViewModel.DataTypes[i]!.defaultUnit = SelectedUnit[i];
            }
        }
    }
}
