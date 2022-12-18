using DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DesktopInterface.Control;
using Caliburn.Micro;
using System.Windows.Controls;
using System.Timers;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using System.Windows.Threading;

namespace DesktopInterface.ViewModels
{
    public class DataGridViewModel : Screen
    {
        private float _samplingTime;
        private DispatcherTimer _timer;
        private string _header = "Datasets downloaded from sensehat";
        private BindableCollection<DataStruct> _dataStructs = new BindableCollection<DataStruct>();
        public DataGridViewModel()
        {
            _timer = new DispatcherTimer();
            _samplingTime = ApplicationConfiguration.SamplingTime;
            DispatchTimer();
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

        public BindableCollection<DataStruct> DataStructs
        {
            get { return _dataStructs; }
            set 
            { 
                _dataStructs = value;
                NotifyOfPropertyChange(() => DataStructs);
            }
        }

        private void LoadData() 
        {
            ApiHelper.GetDataStructsList("data_list.json").ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    DataStructs = new BindableCollection<DataStruct>(task.Result);
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

        private void UpdateTimer_Tick(object sender, EventArgs e) 
        {
                LoadData();
        }
    }
}
