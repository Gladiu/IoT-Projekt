using DataTypes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace DesktopInterface.Control
{
    public static class ApplicationConfiguration
    {
        public static string IpAdress = "http://localhost";

        public static string Port = "";

        public static string ApiVersion = "";

        public static float SamplingTime = 1000;

        public static int SamplesCount = 100;

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
