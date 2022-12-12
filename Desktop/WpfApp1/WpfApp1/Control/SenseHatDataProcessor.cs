
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DataTypes;

namespace WpfApp1.Control
{
    public class SenseHatDataProcessor
    {   
        public SenseHatDataProcessor() { }
        public static async Task<DataStruct> LoadTemperatureData() 
        {
            string url = "http://localhost/temperature.json";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode) 
                {
                    DataStruct dataStruct = await response.Content?.ReadFromJsonAsync<DataStruct>();
                    return dataStruct;
                } 
                else 
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
