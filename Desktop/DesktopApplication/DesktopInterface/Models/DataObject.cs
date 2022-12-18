using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopInterface.Models
{
    public class DataObject
    {
        public string name { get; set; }
        public float value { get; set; }
        public string unit { get; set; }

        public DataObject() 
        {
            //name = "";
            //value = 0;
            //unit= "";
        }
    }
}
