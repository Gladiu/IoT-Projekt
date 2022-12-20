﻿using Caliburn.Micro;
using System.Collections.Generic;
using DataTypes;
using DesktopInterface.Control;

namespace DesktopInterface.ViewModels
{
    public class WindowViewModel : Conductor<object>
    {
        public static List<DataStruct>? DataTypes;
        private string _activeTab = "Entry";
        public string ActiveTab
        {
            get 
            {
                return _activeTab;
            }
            set 
            {
                _activeTab = value;
                NotifyOfPropertyChange(() => ActiveTab);
            }
        }
        public WindowViewModel()
        {
            ApiHelper.GetDataStructsList("dataTypes.php").ContinueWith(result => 
            {
                DataTypes = result.Result;
            });
            LoadEntryView("Entry");
        }
        public bool CanLoadEntryView(string activeTab) 
        {
            return activeTab != "Entry";
        }
        public void LoadEntryView(string activeTab)
        {
            ActivateItemAsync(new EntryViewModel()).ContinueWith(result => {
                if (result.IsCompletedSuccessfully)
                {
                    ActiveTab = "Entry";
                }
            }); ;
        }
        public bool CanLoadDataGridView(string activeTab)
        {
            return activeTab != "Data";
        }
        public void LoadDataGridView(string activeTab) 
        {
            ActivateItemAsync(new DataGridViewModel(DataTypes)).ContinueWith(result => {
                if (result.IsCompletedSuccessfully) 
                {
                    ActiveTab = "Data";
                }
            });
        }
        public bool CanLoadChartView(string activeTab) 
        {
            return activeTab != "Chart";
        }
        public void LoadChartView(string activeTab) 
        {
            ActivateItemAsync(new ChartViewModel(DataTypes)).ContinueWith(result => {
                if (result.IsCompletedSuccessfully)
                {
                    ActiveTab = "Chart";
                }
            });
        }
        public bool CanLoadLedControlView(string activeTab)
        {
            return activeTab != "LED";
        }
        public void LoadLedControlView(string activeTab)
        {
            ActivateItemAsync(new LedControlViewModel()).ContinueWith(result => {
                if (result.IsCompletedSuccessfully)
                {
                    ActiveTab = "LED";
                }
            });
        }
    }
}