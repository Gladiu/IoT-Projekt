using DesktopInterface.ViewModels;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DesktopInterface.Control
{
    public static class ApplicationConfiguration
    {
        private static string ipAdress = "http://192.168.1.97:5000";

        private static string port = "5000";

        private static string apiVersion = "";

        private static float samplingTime = 1000;

        private static int samplesCount = 100;

        public static float SamplingTime { get => samplingTime; set => samplingTime = value; }

        public static string ApiVersion { get => apiVersion; set => apiVersion = value; }

        public static string Port { get => port; set => port = value; }

        public static string IpAdress { get => ipAdress; set => ipAdress = value; }

        public static int SamplesCount { get => samplesCount; set => samplesCount = value; }

        public static void SaveConfiguration() 
        {
            string json= JsonConvert.SerializeObject(new Config());
            string[] lines =
            {
            json
            };

            File.WriteAllLinesAsync("AppConfiguration.json", lines);
        }

        public static void LoadConfiguration() 
        {
            string[]? lines = null;
            try
            {
                lines = File.ReadAllLines("AppConfiguration.json");
            }
            catch(Exception e) { Console.WriteLine(e.Message); }
            if (lines != null)
            {
                Config? config = JsonConvert.DeserializeObject<Config>(lines[0]);
                if (config != null) 
                {
                    IpAdress = config.IpAdress;
                    Port = config.Port;
                    ApiVersion = config.ApiVersion;
                    SamplingTime = config.SamplingTime;
                    SamplesCount = config.SamplesCount;
                }
            }
        }

        public static async Task UpdateDataTypes() 
        {
            WindowViewModel.DataTypes = await ApiHelper.GetDataStructsList();
        }
    }
}
