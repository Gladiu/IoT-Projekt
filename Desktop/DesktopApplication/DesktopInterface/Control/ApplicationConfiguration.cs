using DataTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DesktopInterface.Control
{
    public static class ApplicationConfiguration
    {
        private static string ipAdress = "http://localhost";

        private static string port = "";

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
            string[] lines = File.ReadAllLines("AppConfiguration.json");
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
    }
}
