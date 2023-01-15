using DataTypes;
using DesktopInterface.Dtos;
using DesktopInterface.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace DesktopInterface.Control
{
    public static class ApiHelper
    {
        public static HttpClient? ApiClient { get; set; }
        public static void InitializeClient() 
        {
            ApiClient = new HttpClient();
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<List<DataStruct>?> GetDataStructsList()
        {
            if (ApiClient == null) 
            {
                return null;
            }
            var requestUri = string.Format(CultureInfo.InvariantCulture, ApiRoutes.GetDataStructs);
            try
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync(requestUri))
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
        public static async Task<List<DataObject>?> GetDataObjectsList()
        {
            if (ApiClient == null)
            {
                return null;
            }
            var requestUri = string.Format(CultureInfo.InvariantCulture, ApiRoutes.GetDataObjects);
            try
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync(requestUri))
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

        public static async Task<List<LedDto>?> GetLeds()
        {
            if (ApiClient == null)
            {
                return null;
            }
            var requestUri = string.Format(CultureInfo.InvariantCulture, ApiRoutes.GetLeds);
            try
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync(requestUri))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        List<LedDto>? leds = await response.Content.ReadFromJsonAsync<List<LedDto>>();
                        return leds;
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

        public static async Task<string?> PostLeds(List<LedDto> data)
        {
            string? responseText = null;

            try
            {
                var requestUri = string.Format(CultureInfo.InvariantCulture, ApiRoutes.PostLeds);
                //var requestData = new FormUrlEncodedContent(data);
                var result = await ApiClient.PostAsJsonAsync(requestUri, data);
                responseText = await result.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                Debug.WriteLine("NETWORK ERROR");
                Debug.WriteLine(e);
            }

            return responseText;
        }

        public static async Task<string?> PostSelectedUnits(List<string>? obj)
        {
            var requestUri = string.Format(CultureInfo.InvariantCulture, ApiRoutes.PostDefaultUnits);
            try
            {
                if (ApiClient == null) 
                {
                    return null;
                }
                using (HttpResponseMessage response = await ApiClient.PostAsJsonAsync(requestUri, obj))
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
            if (ApiClient == null)
                return;
            try 
            {
                ApiClient.BaseAddress = new Uri(ApplicationConfiguration.IpAdress);
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.Message); 
                return;
            }
        }
    }
}
