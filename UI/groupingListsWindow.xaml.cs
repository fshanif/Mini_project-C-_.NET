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
    /// Interaction logic for groupingListsWindow.xaml
    /// </summary>
    //hostbynumofhu
    public partial class groupingListsWindow : Window
    {
        //constructor
        public groupingListsWindow()
        {
            InitializeComponent();
        }
        //button
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Window tmp = new manegerwind();
            tmp.ShowDialog();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
           this.Close();
            Window tmp = new grlistbyarea();
                tmp.ShowDialog();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new grlistbynumpeople();
            tmp.ShowDialog();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new hulistbysubarea();
            tmp.ShowDialog(); 
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new hostbynumofhu();
            tmp.ShowDialog();

        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new grbygetint();
            tmp.ShowDialog();
        }
    }
}
