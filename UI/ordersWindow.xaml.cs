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
    /// Interaction logic for ordersWindow.xaml
    /// </summary>
    public partial class ordersWindow : Window
    {
        //helper
        int helper_id;
        //constructor
        public ordersWindow(int id)
        {
            InitializeComponent();
            helper_id = id;
        }
        //button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Window tmp = new orderListWindow(helper_id);
            tmp.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            List<int> x = MainWindow.bl.gethostingUnitKeyByTz(helper_id);
            List<HostingUnit> list = new List<HostingUnit>();
            foreach (int item in x)
            {
                list.Add(MainWindow.bl.getHostingUnitByKey(item));
            }

            Window tmp = new clientListWindow(list);
            tmp.ShowDialog();
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new inpersonalArea(helper_id);
            tmp.ShowDialog();
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}