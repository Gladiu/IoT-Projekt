using DataTypes;
using DesktopInterface.Dtos;
using DesktopInterface.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopInterface.Control
{
    public static class ApiHelper
    {
        private static string baseUrl = "https://b6bd4311-6494-495a-a73c-25ae508bb185.mock.pstmn.io";
        public static HttpClient? ApiClient { get; set; }
        public static void InitializeClient() 
        {
            ApiClient = new HttpClient();
            //ApiClient.BaseAddress = new Uri("http://google.com");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<DataStruct?> GetDataStruct(string getRequest) 
        {
            if (ApiClient == null) 
            {
                return null;
            }
            string url = $"{baseUrl}/{getRequest}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync(url))
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
        public static async Task<List<DataStruct>?> GetDataStructsList(string getRequest)
        {
            if (ApiClient == null) 
            {
                return null;
            }
            string url = $"{baseUrl}/{getRequest}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        List<DataStruct>? dataStructs = await response.Content.ReadFromJsonAsync<List<DataStruct>>();
                        return dataStructs;
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
        public static async Task<List<DataObject>?> GetDataObjectsList(string getRequest)
        {
            if (ApiClient == null)
            {
                return null;
            }
            string url = $"{baseUrl}/{getRequest}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var dat = await response.Content.ReadAsStringAsync();
                        List<DataObject>? dataObjects = JsonSerializer.Deserialize<List<DataObject>>(dat);
                        //List<DataObject> dataObjects = await response.Content.ReadFromJsonAsync<List<DataObject>>();
                        return dataObjects;
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
        public static async Task<DataStruct?> Post(string postUrl, object obj)
        {
            if (ApiClient == null)
            {
                return null;
            }
            string url = $"{baseUrl}/{postUrl}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.PostAsJsonAsync(url, obj))
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
        public static async Task<DataStruct?> Put(string putUrl, object obj)
        {
            if (ApiClient == null)
            {
                return null;
            }
            string url = $"{baseUrl}/{putUrl}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.PutAsJsonAsync(url, obj))
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
        public static async Task<DataStruct?> Delete(string deleteUrl)
        {
            if (ApiClient == null)
            {
                return null;
            }
            string url = $"{baseUrl}/{deleteUrl}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.DeleteAsync(url))
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
        public static async Task<string?> PostControlRequest(List<LedDto> data)
        {
            string? responseText = null;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string url = $"{baseUrl}/post/Leds";
                    //var requestData = new FormUrlEncodedContent(data);
                    // Sent POST request
                    var result = await client.PostAsJsonAsync(url, data);
                    // Read response content
                    responseText = await result.Content.ReadAsStringAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseText;
        }

        public static async Task<string?> PostSelectedUnits(string postUrl, List<string>? obj)
        {
            string url = $"{baseUrl}/{postUrl}";
            try
            {
                if (ApiClient == null) 
                {
                    return null;
                }
                using (HttpResponseMessage response = await ApiClient.PostAsJsonAsync(url, obj))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        string? data = await response.Content.ReadAsStringAsync();
                        return data;
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

        public static void UpdateApiClient() 
        {
            baseUrl = ApplicationConfiguration.IpAdress;
        }
    }
}
