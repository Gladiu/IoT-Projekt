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
        private readonly float _samplingTime;
        private readonly DispatcherTimer _timer;
        private string header = "Datasets downloaded from sensehat";
        private List<DataObject>? dataObjects = new List<DataObject>();
        private List<List<string>>? units;
        public List<DataStruct>? DataStructs;
        private List<string>? selectedUnit;
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
            units = new List<List<string>>();
            selectedUnit = new List<string>();
            dataObjects = new List<DataObject>();
            GridData = new Grid();
            if (dataStructs != null) 
            {
                foreach (var data in dataStructs)
                {
                    units.Add(data.units);
                    selectedUnit.Add(data.defaultUnit);
                }
            }
            NotifyOfPropertyChange(() => SelectedUnit);
        }

        public string Header
        {
            get 
            {
                return header;
            }
            set 
            {
                header = value;
                NotifyOfPropertyChange(() => Header);
            }
        }

        public List<DataObject>? DataObjects
        {
            get { return dataObjects; }
            set 
            {
                dataObjects = value;
                NotifyOfPropertyChange(() => DataObjects);
            }
        }

        public List<List<string>>? Units
        {
            get { return units; }
            set { units = value; NotifyOfPropertyChange(() => Units); }
        }

        public Grid? GridData { get; set; }

        public List<string>? SelectedUnit
        {
            get 
            {
                return selectedUnit;
            }
            set 
            {
                selectedUnit = value;
                NotifyOfPropertyChange(() => SelectedUnit);
            }
        }

        private void LoadData() 
        {
            ApiHelper.GetDataObjectsList("get/DataObjects").ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    DataObjects = new List<DataObject>(task.Result!);
                }
            });

        }
        private void DispatchTimer() 
        {
            _timer.Stop();
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
            ApiHelper.PostSelectedUnits("post/DefaultUnits", SelectedUnit).ContinueWith(task => 
            {
                if (task.Result != null) 
                {
                    for (int i = 0; i < SelectedUnit?.Count; i++)
                    {
                        WindowViewModel.DataTypes[i].defaultUnit = SelectedUnit[i];
                    }
                }
            });

        }
    }
}
