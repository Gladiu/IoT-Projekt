using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopInterface.Control
{
    public class Config
    {
        public string IpAdress = "localhost";

        public string Port = "";

        public string ApiVersion = "";

        public float SamplingTime = 1000;

        public int SamplesCount = 100;

        public Config()
        {
            IpAdress = ApplicationConfiguration.IpAdress;
            Port = ApplicationConfiguration.Port;
            ApiVersion = ApplicationConfiguration.ApiVersion;
            SamplesCount = ApplicationConfiguration.SamplesCount;
            SamplingTime = ApplicationConfiguration.SamplingTime;
        }
        public Config(string ipadress, string port, string api, float samplesT, int samplesC)
        {
            IpAdress = ipadress;
            Port = port;
            ApiVersion = api;
            SamplingTime = samplesT;
            SamplesCount = samplesC;
        }
    }
}
