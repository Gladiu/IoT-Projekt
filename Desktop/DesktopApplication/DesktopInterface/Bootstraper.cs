using Caliburn.Micro;
using DataTypes;
using DesktopInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DesktopInterface
{
    public class Bootstraper : BootstrapperBase
    {
        public Bootstraper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            ApiHelper.InitializeClient();
            DisplayRootViewForAsync<DataGridViewModel>();
        }
    }
}
