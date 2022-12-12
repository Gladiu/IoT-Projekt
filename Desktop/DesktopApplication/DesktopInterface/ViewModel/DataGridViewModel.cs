using DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using DesktopInterface.Control;

namespace DesktopInterface.ViewModel
{
    public class DataGridViewModel
    {
        public ObservableCollection<DataStruct> dataGridList = new ObservableCollection<DataStruct>();

        public DataGridViewModel(List<DataStruct> data) 
        {
            ObservableCollection<DataStruct> dataGridList = new ObservableCollection<DataStruct>(data);
        }
    }
}
