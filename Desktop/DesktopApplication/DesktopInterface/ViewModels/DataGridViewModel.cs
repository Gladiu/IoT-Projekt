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

namespace DesktopInterface.ViewModels
{
    public class DataGridViewModel : Screen
    {
        private string _header = "Datasets downloaded from sensehat";
        private BindableCollection<DataStruct> _dataStructs = new BindableCollection<DataStruct>();
        public DataGridViewModel()
        {
            
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
            }
        }

        public BindableCollection<DataStruct> DataGrid
        {
            get { return _dataStructs; }
            set { _dataStructs = value; }
        }
    }
}
