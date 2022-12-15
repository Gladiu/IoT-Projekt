using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DataTypes
{
    public static class ApiHelper
    {
        public static string baseUrl = "http://localhost";
        public static HttpClient ApiClient { get; set; }
        public static void InitializeClient() 
        {
            ApiClient = new HttpClient();
            //ApiClient.BaseAddress = new Uri("http://google.com");
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public static async Task<DataStruct> GetDataStruct(string getRequest) 
        {
            string url = $"{baseUrl}/{getRequest}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync(url))
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
        public static async Task<List<DataStruct>> GetDataStructsList(string getRequest)
        {
            string url = $"{baseUrl}/{getRequest}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.GetAsync(url))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        List<DataStruct> dataStructs = await response.Content.ReadFromJsonAsync<List<DataStruct>>();
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
                throw e;
            }
        }
        public static async Task<DataStruct> Post(string postUrl, object obj)
        {
            string url = $"{baseUrl}/{postUrl}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.PostAsJsonAsync(url, obj))
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
        public static async Task<DataStruct> Put(string putUrl, object obj)
        {
            string url = $"{baseUrl}/{putUrl}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.PutAsJsonAsync(url, obj))
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
        public static async Task<DataStruct> Delete(string deleteUrl)
        {
            string url = $"{baseUrl}/{deleteUrl}";
            try
            {
                using (HttpResponseMessage response = await ApiClient.DeleteAsync(url))
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
    }
}
