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
    /// Interaction logic for hostbynumofhu.xaml
    /// </summary>
    public partial class hostbynumofhu : Window
    {
        //constructor
        public hostbynumofhu()
        {
            InitializeComponent();
            var list = MainWindow.bl.groupinghostbynumofHU();
            List<Host> hlist = new List<Host>();
            foreach (var item in list)
            {
                foreach (var gs in item)
                {
                    hlist.Add(gs);
                }
            }
            foreach (var item in hlist)
            {
                hugroupingbyarea.Children.Add(new hostUserControl(item));
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
