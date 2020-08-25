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
    /// Interaction logic for openWindow.xaml
    /// </summary>
    public partial class openWindow : Window
    {
        //constructor
        public openWindow()
        {
            InitializeComponent();
        }
        //button
        private void Buttonmanager_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new managerWindow();
            tmp.ShowDialog();
        }

        private void Buttonguest_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new clientWindow();
            tmp.ShowDialog();
        }

        private void Buttonhost_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new hostWindow();
            tmp.ShowDialog();
        }

        private void Buttonclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}

