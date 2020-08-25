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
using BL;

namespace UI
{
    /// <summary>
    /// Interaction logic for managerWindow.xaml
    /// </summary>
    public partial class managerWindow : Window
    {
        //helper
        manager x = new manager();
        //constructor
        public managerWindow()
        {
            InitializeComponent();
        }
        //image button
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            show_code_box.Text = password_box.Password;
        }

        //button
        private void GoBotton_Click(object sender, RoutedEventArgs e)
        {
            if (password_box.Password == x.code)
            {
                this.Close();
                Window tmp = new manegerwind();
                tmp.ShowDialog();
            }
            else
            {
                MessageBox.Show("הסיסמה שגויה,\n נסה שוב!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

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
    }
}
