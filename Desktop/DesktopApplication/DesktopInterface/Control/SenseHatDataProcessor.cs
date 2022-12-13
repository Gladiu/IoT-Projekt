
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Documents;
using DataTypes;

namespace DesktopInterface.Control
{
    public class SenseHatDataProcessor
    {   
        public SenseHatDataProcessor() { }
        public static async Task<DataStruct> LoadTemperatureData() 
        {
            string url = "http://localhost/temperature.json";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        DataStruct dataStruct = await response.Content.ReadFromJsonAsync<DataStruct>();
                        return dataStruct;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e) 
            {
                throw e;
            }
        }
        public static async Task<DataStruct> LoadHumidityData()
        {
            string url = "http://localhost/humidity.json";
            try
            {
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        DataStruct dataStruct = await response.Content.ReadFromJsonAsync<DataStruct>();
                        return dataStruct;
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static async Task<List<DataStruct>> LoadData()
        {
            string url = "http://localhost/data_list.json";
            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    List<DataStruct> dataStructs = await response.Content?.ReadFromJsonAsync<List<DataStruct>>();
                    return dataStructs;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
