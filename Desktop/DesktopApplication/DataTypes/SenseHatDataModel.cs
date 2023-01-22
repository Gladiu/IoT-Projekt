using DataTypes.Interfaces;

namespace DataTypes
{
    public class SenseHatData : ISensehatData
    {
        public DataStruct? temperature;
        public DataStruct? humidity;
        public List<DataStruct> dataStructs = new List<DataStruct>();
        public SenseHatData() { }

        public List<DataStruct> getData() { return dataStructs; }
    }
}
