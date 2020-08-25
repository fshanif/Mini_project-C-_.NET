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
    /// Interaction logic for deleteHostingUnitWindow.xaml
    /// </summary>
    public partial class deleteHostingUnitWindow : Window
    {
        //helper
        int helperID;
        //constructor
        public deleteHostingUnitWindow(int myID)
        {
            InitializeComponent();
            helperID = myID;
        }
        //button close
        private void button_feeclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //button back
        private void button_back_Click(object sender, RoutedEventArgs e)
        {

            Window tmp = new inpersonalArea(helperID);
            tmp.ShowDialog();   
            this.Close();

        }
        //button
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                HostingUnit helper = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(txt_removehostunit.Text));
                if (helper != null)
                {
                    MainWindow.bl.deleteHostingUnit(helper);
                    this.Close();
                    Window tmp = new openWindow();
                    tmp.ShowDialog();
                }
                else
                    MessageBox.Show("יחידה זו אינה קיימת", "שגיאה בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception x)
            {
                MessageBox.Show(x.Message, "שגיאה", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
