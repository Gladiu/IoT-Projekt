using Caliburn.Micro;
using System.Collections.Generic;
using DataTypes;
using DesktopInterface.Control;
using System.Net;
using DesktopInterface.Models;

namespace DesktopInterface.ViewModels
{
    public class WindowViewModel : Conductor<IConductorExtension>
    {
        public static List<DataStruct>? DataTypes = new List<DataStruct>();

        public static List<Led>? Leds = new List<Led>();

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
            ApiHelper.GetDataStructsList().ContinueWith(task => 
            {
                DataTypes = task.Result;
            });

            ApiHelper.GetLeds().ContinueWith(task =>
            {
                if (task.Result != null) 
                {
                    foreach (var ledDto in task.Result) 
                    {
                        Leds!.Add(new Led(ledDto));
                    }
                }
            });

            LoadEntryView("Entry");
        }
        public bool CanLoadEntryView(string activeTab) 
        {
            return activeTab != "Entry";
        }
        public void LoadEntryView(string activeTab)
        {
            if (ActiveItem != null)
                ActiveItem.DisposeOfContents();
            ActivateItemAsync(new EntryViewModel()).ContinueWith(result => {
                if (result.IsCompletedSuccessfully)
                {
                    ActiveTab = "Entry";
                }
            });
        }
        public bool CanLoadDataGridView(string activeTab)
        {
            return activeTab != "Data";
        }
        public void LoadDataGridView(string activeTab) 
        {
            if (ActiveItem != null)
                ActiveItem.DisposeOfContents();
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
            if (ActiveItem != null)
                ActiveItem.DisposeOfContents();
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
            if (ActiveItem != null)
                ActiveItem.DisposeOfContents();
            ActivateItemAsync(new LedControlViewModel()).ContinueWith(result => {
                if (result.IsCompletedSuccessfully)
                {
                    ActiveTab = "LED";
                }
            });
        }
    }
}
