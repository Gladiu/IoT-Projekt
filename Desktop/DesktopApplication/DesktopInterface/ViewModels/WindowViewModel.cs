using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTypes;

namespace DesktopInterface.ViewModels
{
    public class WindowViewModel : Conductor<object>
    {
        public WindowViewModel()
        {
            LoadEntryView();
        }
        public bool CanLoadEntryView() 
        {
            return true;
        }
        public void LoadEntryView() 
        {
            ActivateItemAsync(new EntryViewModel()).ContinueWith(result => {
                if (!result.IsCompletedSuccessfully)
                {
                    
                }
            }); ;
        }
        public bool CanLoadDataGridView() 
        {
            return true;
        }
        public void LoadDataGridView() 
        {
            ActivateItemAsync(new DataGridViewModel()).ContinueWith(result => {
                if (!result.IsCompletedSuccessfully) 
                {
                    
                }
            });
        }
        public bool CanLoadChartView() 
        {
            return true;
        }
        public void LoadChartView() 
        {
            ActivateItemAsync(new ChartViewModel()).ContinueWith(result => {
                if (!result.IsCompletedSuccessfully)
                {
                }
            });
        }
    }
}
