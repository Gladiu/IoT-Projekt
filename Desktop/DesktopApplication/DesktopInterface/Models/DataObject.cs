namespace DesktopInterface.Models
{
    public class DataObject
    {
        public string name { get; set; }
        public float value { get; set; }
        public string unit { get; set; }

        public DataObject() 
        {
            name = string.Empty;
            value = 0;
            unit = string.Empty;
        }
    }
}
