using BE;
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
    /// Interaction logic for grlistbynumpeople.xaml
    /// </summary>
    public partial class grlistbynumpeople : Window
    {
       //constructor
        public grlistbynumpeople()
        {
            InitializeComponent();
            var list = MainWindow.bl.groupingGRbynumofpeople();
            List<GuestRequest> guestRequests = new List<GuestRequest>();
            foreach (var item in list)
            {
                foreach (var gs in item)
                {
                    guestRequests.Add(gs);
                }
            }
            foreach (var item in guestRequests)
            {
                grgroupingnump.Children.Add(new grnumpeopleUserControl(item));
            }
        }
        //button
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void button_back_Click(object sender, RoutedEventArgs e)
        {            this.Close();

            Window tmp = new groupingListsWindow();
            tmp.ShowDialog();
        }
    }
}
