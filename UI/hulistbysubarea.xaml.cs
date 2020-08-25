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
using System.Windows.Shapes;
using BE;
namespace UI
{
    /// <summary>
    /// Interaction logic for hulistbysubarea.xaml
    /// </summary>
    public partial class hulistbysubarea : Window
    {
        //constructor
        public hulistbysubarea()
        {
            InitializeComponent();
            var list = MainWindow.bl.groupingHUbyarea();
            List<HostingUnit> hulist = new List<HostingUnit>();
            foreach (var item in list)
            {
                foreach (var gs in item)
                {
                    hulist.Add(gs);
                }
            }
            foreach (var item in hulist)
            {
                hugroupingbyarea.Children.Add(new hUgruopUserControl(item));
            }
        }
        //button
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new groupingListsWindow();
            tmp.ShowDialog();
        }
    }
}
