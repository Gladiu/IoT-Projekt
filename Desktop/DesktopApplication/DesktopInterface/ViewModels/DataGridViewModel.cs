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
        private string _header = "Datasets downloaded from sensehat";
        private ICommand _loadDataCommand;
        private BindableCollection<DataStruct> _dataStructs = new BindableCollection<DataStruct>();
        public DataGridViewModel()
        {
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

        public void LoadData() 
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
            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += new EventHandler(UpdateTimer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void UpdateTimer_Tick(object sender, EventArgs e) 
        {
                LoadData();
        }
    }
}
