using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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
                Config config = JsonConvert.DeserializeObject<Config>(lines[0]);
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
