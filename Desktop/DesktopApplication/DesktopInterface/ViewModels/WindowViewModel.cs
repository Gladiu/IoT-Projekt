using Caliburn.Micro;
using System.Collections.Generic;
using DataTypes;
using DesktopInterface.Control;
using System.Net;
using DesktopInterface.Models;
using System.Threading;
using System.Windows;
using System.Windows.Controls.Primitives;

namespace DesktopInterface.ViewModels
{
    public class WindowViewModel : Conductor<IConductorExtension>
    {
        public static List<DataStruct>? DataTypes = new List<DataStruct>();

        public static List<Led>? Leds = new List<Led>();

        private string _activeTab = "None";

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
            InitDataTypes();
            UpdateLeds();
            LoadEntryView(ActiveTab);
        }
        public bool CanLoadEntryView(string activeTab) 
        {
            return activeTab != "Entry" && activeTab != "None";
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
            return activeTab != "Data" && activeTab != "None";
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
            return activeTab != "Chart" && activeTab != "None";
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
            return activeTab != "LED" && activeTab != "None";
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

        private void InitDataTypes() 
        {
            ApiHelper.GetDataStructsList().ContinueWith(task =>
            {
                if (task.Result == null) 
                {
                    ActiveTab = "None";
                    PopUpError();
                }
                DataTypes = task.Result;
            });
        }

        public static void UpdateDataTypes() 
        {
            ApiHelper.GetDataStructsList().ContinueWith(task =>
            {
                DataTypes = task.Result;
            });
        }

        public static void UpdateLeds() 
        {
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
        }

        private void PopUpError() 
        {
            MessageBox.Show("Wrong API IP. Check IP, save settings and then relaunch application!", "Invalid IP!", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
