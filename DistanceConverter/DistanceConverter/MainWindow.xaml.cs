using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace DistanceConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DistanceConverterVM distanceConverterVm = new DistanceConverterVM();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = distanceConverterVm;
            
            // Add unit items to listboxes
            List<string> unitNamesList = distanceConverterVm.GetDistanceUnitsNames();

            foreach (string unitName in unitNamesList)
            {
                lstbxTo.Items.Add(unitName);
                lstbxFrom.Items.Add(unitName);
            }
        }

        private void Button_Convert_Click(object sender, RoutedEventArgs e)
        {
            distanceConverterVm.Convert();
        }

        private void MassValue_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only numbers to be typed
            e.Handled = new Regex("[^0-9.-]+").IsMatch(e.Text);
        }
    }
}
