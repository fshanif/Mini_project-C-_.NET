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
    /// Interaction logic for manegerwind.xaml
    /// </summary>
    public partial class manegerwind : Window
    {
        //constructor
        public manegerwind()
        {
            InitializeComponent();
        }
        //button
        private void feeconclude_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new feeConcluded();
            tmp.ShowDialog();

        }
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new openWindow();
            tmp.ShowDialog();
        }
        private void lists_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new groupingListsWindow();
            tmp.ShowDialog();

        }
    }
}
