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
    /// Interaction logic for orderListWindow.xaml
    /// </summary>
    public partial class orderListWindow : Window
    {
        //helper
        Order o = new Order();
        int helpID;

        //constructor
        public orderListWindow(int id)
        {
            InitializeComponent();
            helpID = id;
            CB_orderforhost.DataContext = MainWindow.bl.getordersKeyByTz(helpID);
        }
        //LostFocus
        private void CB_orderforhost_LostFocus(object sender, RoutedEventArgs e)
        {
            ordernum.Text = showTxtBx_OrderKey.Text;
        }
        //SelectionChanged
        private void CB_orderforhost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Order order = new Order();
            order = MainWindow.bl.GetOrderList().Find(item => item.OrderKey == Convert.ToInt32(CB_orderforhost.SelectedItem)); // MainWindow.bl.getordersKeyByTz(Convert.ToInt32(CB_orderforhost.Text));
            o.OrderKey = order.OrderKey;
            o.Status = order.Status;
            o.totalPrice = order.totalPrice;
            o.OrderDate = order.OrderDate;
            o.CreateDate = order.CreateDate;
            o.GuestRequestKey = order.GuestRequestKey;
            o.HostingUnitKey = order.HostingUnitKey;
            showTxtBx_CreateDate.Text = Convert.ToString(o.CreateDate.Date);
            showTxtBx_GuestRequestKey.Text = Convert.ToString(o.GuestRequestKey);
            showTxtBx_HostingUnitKey.Text = Convert.ToString(o.HostingUnitKey);
            showTxtBx_OrderDate.Text = Convert.ToString(o.OrderDate.Date);
            showTxtBx_OrderKey.Text = Convert.ToString(o.OrderKey);
            showTxtBx_Status.Text = Convert.ToString(o.Status);
            showTxtBx_totalPrice.Text = Convert.ToString(o.totalPrice);
        }
        //Button
        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new inpersonalArea(helpID);
            tmp.ShowDialog();
        }

        private void b_updateOrderStatus_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CB_orderforhost.Text == "")
                    MessageBox.Show("אנא בחר הזמנה לעדכון", "לא נבחר הזמנה לעדכון", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    this.Close();
                    Window tmp = new updateOrderStatusWindow(o);
                    tmp.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
