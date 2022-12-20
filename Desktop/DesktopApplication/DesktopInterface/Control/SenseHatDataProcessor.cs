
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using DataTypes;

namespace DesktopInterface.Control
{
    public class SenseHatDataProcessor
    {
        public SenseHatDataProcessor() { }
        public static async Task<DataStruct?> LoadTemperatureData() 
        {
            string url = "http://localhost/temperature.json";
            try
            {
                if (ApiHelper.ApiClient == null) 
                {
                    return null;
                }
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        DataStruct? dataStruct = await response.Content.ReadFromJsonAsync<DataStruct>();
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
                Console.WriteLine($"Caught exception:{e.Message}");
                return null;
            }
        }
        public static async Task<DataStruct?> LoadHumidityData()
        {
            string url = "http://localhost/humidity.json";
            try
            {
                if (ApiHelper.ApiClient == null) 
                {
                    return null;
                }
                using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        DataStruct? dataStruct = await response.Content.ReadFromJsonAsync<DataStruct>();
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
                Console.WriteLine($"Caught exception:{e.Message}");
                return null;
            }
        }

        public static async Task<List<DataStruct>?> LoadData()
        {
            string getUrl = "data_list.json";
            List<DataStruct>? result =  await ApiHelper.GetDataStructsList(getUrl);
            var result2 = await ApiHelper.Put("apiPut", result!);
            return result;
        }
    }
}
