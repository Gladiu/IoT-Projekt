namespace DataTypes
{
    public class DataStruct
    {   
        public string name { get; set; }

        public List<string> units { get; set; }

        public float value { get; set; }

        public string defaultUnit { get; set; }

        public DataStruct() 
        {
            name = string.Empty;
            units = new List<string>();
            defaultUnit = string.Empty;
        }
    }
}