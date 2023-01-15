﻿using Caliburn.Micro;
using DesktopInterface.Control;
using DesktopInterface.Models;

namespace DesktopInterface.ViewModels
{
    public class EntryViewModel : Screen, IConductorExtension
    {
        private string? _url = "https://b6bd4311-6494-495a-a73c-25ae508bb185.mock.pstmn.io";

        private string? _port = "";

        private string? _apiVersion = "v1";

        private float? _samplingTime = 1000;

        private int? _samplesCount = 10;

        public string? Url 
        {
            get { return _url; }
            set
            { 
                _url = value;
                if (_url != null)
                {
                    ApplicationConfiguration.IpAdress = _url;
                    ApiHelper.UpdateApiClient();
                }
                NotifyOfPropertyChange(() => Url);
            }
        }

        public string? Port
        {
            get { return _port; }
            set
            {
                _port = value;
                if (_port != null) 
                {
                    ApplicationConfiguration.IpAdress = _port;
                }
                NotifyOfPropertyChange(() => Port);
            }
        }

        public string? ApiVersion
        {
            get { return _apiVersion; }
            set
            {
                _apiVersion = value;
                if (_apiVersion != null) 
                {
                    ApplicationConfiguration.IpAdress = _apiVersion;
                }
                NotifyOfPropertyChange(() => ApiVersion);
            }
        }

        public float? SamplingTime
        {
            get { return _samplingTime; }
            set
            {
               _samplingTime = value;
                if (_samplingTime != null) 
                {
                    ApplicationConfiguration.SamplingTime = _samplingTime.Value;
                }
                NotifyOfPropertyChange(() => SamplingTime);
            }
        }

        public int? SamplesCount
        {
            get 
            {
                return _samplesCount;
            }
            set 
            {
                _samplesCount = value;
                if (_samplesCount != null)
                {
                    ApplicationConfiguration.SamplesCount = _samplesCount.Value;
                }
                NotifyOfPropertyChange(() => SamplesCount);

            }
        }

        public EntryViewModel()
        {
            _url = ApplicationConfiguration.IpAdress;
            _port = ApplicationConfiguration.Port;
            _apiVersion = ApplicationConfiguration.ApiVersion;
            _samplesCount = ApplicationConfiguration.SamplesCount;
            _samplingTime = ApplicationConfiguration.SamplingTime;
        }

        public void SaveSettings() 
        {
            ApplicationConfiguration.SaveConfiguration();
        }

        public void LoadSettings() 
        {
            ApplicationConfiguration.LoadConfiguration();
            Url = ApplicationConfiguration.IpAdress;
            Port = ApplicationConfiguration.Port;
            ApiVersion = ApplicationConfiguration.ApiVersion;
            SamplesCount = ApplicationConfiguration.SamplesCount;
            SamplingTime = ApplicationConfiguration.SamplingTime;
            ApiHelper.UpdateApiClient();
        }

        public void DisposeOfContents()
        {
            // Intentionally left empty
        }
    }
}
