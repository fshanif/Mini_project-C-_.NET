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

namespace UI
{
    /// <summary>
    /// Interaction logic for inpersonalArea.xaml
    /// </summary>
    public partial class inpersonalArea : Window
    {
        //helper
        int helperID = 0;
        
        //constructor
        public inpersonalArea(int myid)
        {
            InitializeComponent();
            helperID = myid;
        }
        //Button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new updateHostingUnitWindow(helperID);
            tmp.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new deleteHostingUnitWindow(helperID);
            tmp.ShowDialog();
        }

        private void button_pback_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new hostWindow();
            tmp.ShowDialog();
        }

        private void button_pclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new ordersWindow(helperID);
            tmp.ShowDialog();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new myhu(helperID);
            tmp.ShowDialog();
        }
    }

}

