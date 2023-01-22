using Caliburn.Micro;
using DataTypes;
using DesktopInterface.Control;
using DesktopInterface.ViewModels;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DesktopInterface.Views
{
    /// <summary>
    /// Interaction logic for DataGridView.xaml
    /// </summary>
    public partial class DataGridView : UserControl
    {
        public DataGridView()
        {
            InitializeComponent();
        }

        private void MainGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGridViewModel? dataContext = DataContext as DataGridViewModel;
            if (dataContext != null)
            {

                for (int i = 0; i < 2; i++)
                {
                    GriddData.ColumnDefinitions.Add(new ColumnDefinition());
                    GriddData.ColumnDefinitions[i].Width = new GridLength(1, GridUnitType.Auto);
                }

                for (int i = 0; i < dataContext.Units!.Count; i++)
                {
                    GriddData.RowDefinitions.Add(new RowDefinition());
                    GriddData.RowDefinitions[i].Height = new GridLength(1, GridUnitType.Auto);
                }

                for (int i = 0; i < dataContext?.Units.Count; i++)
                {
                    var dataStruct = dataContext?.DataStructs?[i];
                    TextBlock block = new();
                    if (dataStruct != null)
                    {
                        block = new TextBlock
                        {
                            Name = "TextBlock" + i.ToString(),
                            Text = dataStruct.name,
                            TextAlignment = TextAlignment.Right,
                            Margin = new Thickness(0, 0, 5, 0),
                        };
                    }
                    ComboBox Box = new()
                    {
                        Name = "ComboBox" + i.ToString(),
                        ItemsSource = dataContext?.Units?[i],
                        SelectedItem = dataContext?.SelectedUnit?[i],
                    };
                    Box.SetBinding(ComboBox.SelectedItemProperty, dataContext?.GetCommandBinding(i));
                    Grid.SetColumn(block, 0);
                    Grid.SetRow(block, i);
                    Grid.SetColumn(Box, 1);
                    Grid.SetRow(Box, i);
                    GriddData.Children.Add(block);
                    RegisterName(block.Name, block);
                    GriddData.Children.Add(Box);
                    RegisterName(Box.Name, Box);
                }
                dataContext!.GridData = GriddData;
            }
        }
    }
}
