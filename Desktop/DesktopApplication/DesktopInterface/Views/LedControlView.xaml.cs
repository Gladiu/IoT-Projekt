using DesktopInterface.Control;
using DesktopInterface.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for LedControlView.xaml
    /// </summary>
    public partial class LedControlView : UserControl
    {
        public readonly static SolidColorBrush RedColor = new SolidColorBrush(Color.FromArgb(255, 255, 64, 14));

        public readonly static SolidColorBrush GreenColor = new SolidColorBrush(Color.FromArgb(255, 41, 142, 19));

        public LedControlView()
        {
            InitializeComponent();
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeGrid();
        }

        private void GridData_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SendCommand.Background = RedColor;
        }

        private void SendCommand_Click(object sender, RoutedEventArgs e)
        {
            SendCommand.Background = GreenColor;
        }

        private void ClearCommand_Click(object sender, RoutedEventArgs e)
        {
            SendCommand.Background = GreenColor;
        }

        public void InitializeGrid() 
        {
            if (WindowViewModel.Leds!.Count == 0)
                return;
            var LedStyle = (Style)FindResource("LedIndicatorStyle");
            var dataContext = DataContext as LedControlViewModel;
            if (dataContext != null)
            {
                for (int i = 0; i < dataContext.DisplaySizeX; i++)
                {
                    GridData.ColumnDefinitions.Add(new ColumnDefinition());
                    GridData.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Star);
                }

                for (int i = 0; i < dataContext.DisplaySizeY; i++)
                {
                    GridData.RowDefinitions.Add(new RowDefinition());
                    GridData.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Star);
                }

                for (int i = 0; i < dataContext.DisplaySizeX; i++)
                {
                    for (int j = 0; j < dataContext.DisplaySizeY; j++)
                    {
                        Button led = new Button()
                        {
                            Name = "LED" + i.ToString() + j.ToString(),
                            Style = LedStyle,
                            BorderThickness = new Thickness(2),
                        };
                        led.SetBinding(Button.CommandProperty, dataContext.GetCommandBinding(i, j));
                        led.SetBinding(Button.BackgroundProperty, dataContext.GetColordBinding(i, j));
                        Grid.SetColumn(led, i);
                        Grid.SetRow(led, j);
                        GridData.Children.Add(led);
                        RegisterName(led.Name, led);
                    }
                }
                dataContext.ButtonMatrixGrid = GridData;
            }
        }
    }
}
