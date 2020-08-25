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
    /// Interaction logic for myhu.xaml
    /// </summary>
    public partial class myhu : Window
    {
        //helpers
        int index;
        int helper;
        //constructor
        public myhu(int id)
        {
            InitializeComponent();
            helper = id;
            CB_hostingunit.DataContext = MainWindow.bl.gethostingUnitKeyByTz(helper);
        }
        //LostFocus
        private void CB_hostingunit_LostFocus(object sender, RoutedEventArgs e)
        {
            tbHostName.Text = CB_hostingunit.Text;
        }
        //button
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Window tmp = new inpersonalArea(helper);
            tmp.ShowDialog();
        }
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void showButton_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        //SelectionChanged
        private void CB_hostingunit_SelectionChanged(object sender, SelectionChangedEventArgs e)

        {
            index = 0;
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));
            showTxtBx_FirstName.Text = Convert.ToString(x.Owner.PrivateName);
            showTxtBx_LastName.Text = Convert.ToString(x.Owner.FamilyName);
            showTxtBx_phoneNumber.Text = Convert.ToString(x.Owner.FhoneNumber);
            showTxtBx_HostingUnitName.Text = Convert.ToString(x.HostingUnitName);
            showTxtBx_maxGuest.Text = Convert.ToString(x.maxGuest);
            showTxtBx_priceAdults.Text = Convert.ToString(x.priceAdults);
            showTxtBx_priceChildren.Text = Convert.ToString(x.priceChildren);
            showcb_subareaHost.Text = Convert.ToString(x.SubArea);
            showTxtBx_mail.Text = Convert.ToString(x.Owner.MailAddress);
            showcb_med.Text =Convert.ToString( Convert.ToDouble(x.medsatisfication));
            if(x.Uris.Count!=0)
            image.Source = new BitmapImage(x.Uris[index]);
            else
            {
                right.Visibility = Visibility.Hidden;
                left.Visibility = Visibility.Hidden;

            }
        }
        //image button
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));

            if (index == 0)
            {
                index = x.Uris.Count - 1;
            }
            else
            {
                index--;
            }
            image.Source = new BitmapImage(x.Uris[index]);
        }

        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));

            if (index == x.Uris.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            image.Source = new BitmapImage(x.Uris[index]);

        }


    }
}
