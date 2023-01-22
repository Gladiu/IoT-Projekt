using DesktopInterface.Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopInterface.Views
{
    /// <summary>
    /// Interaction logic for EntryView.xaml
    /// </summary>
    public partial class EntryView : UserControl
    {
        public EntryView()
        {
            InitializeComponent();
        }

        private async void Test_Click(object sender, RoutedEventArgs e)
        {
            await SetTestButtonColor();
        }

        private async Task<SolidColorBrush> TestApiConnection() 
        {
            var result = await ApiHelper.GetDataStructsList();
            if (result == null)
                return BrushColors.RedColor;
            else
                return BrushColors.GreenColor;
        }

        private async Task SetTestButtonColor() 
        {
            var result = await TestApiConnection();
            Test.Background = result;
        }

        private async void Test_Loaded(object sender, RoutedEventArgs e)
        {
            await SetTestButtonColor();
        }
    }
}
