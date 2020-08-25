using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for updateOrderStatusWindow.xaml
    /// </summary>
    public partial class updateOrderStatusWindow : Window
    {
        //helper
        Order o = new Order();

        //constructor
        public updateOrderStatusWindow(Order order)
        {
            o.GuestRequestKey = order.GuestRequestKey;
            o.HostingUnitKey = order.HostingUnitKey;
            o.OrderDate = order.OrderDate;
            o.OrderKey = order.OrderKey;
            o.Status = order.Status;
            o.totalPrice = order.totalPrice;
            o.CreateDate = order.CreateDate;

            InitializeComponent();
            cb_statustoUpdate.ItemsSource = Enum.GetValues(typeof(orderStatus)).Cast<orderStatus>();
        }

        //Button
        private void b_update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.bl.updateOrderStatus(o, (orderStatus)cb_statustoUpdate.SelectedItem);//@@@@@@@@@@@222
                BackgroundWorker backgroundWorker = new BackgroundWorker();
                if ((orderStatus)cb_statustoUpdate.SelectedItem==orderStatus.emailSent) {
                    backgroundWorker.DoWork += BackgroundWorker_DoWork;
                    backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
                    backgroundWorker.RunWorkerAsync();
                    }
                MessageBox.Show("הסטטוס עודכן בהצלחה!", "העדכון הוצלח", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
                HostingUnit hu = MainWindow.bl.getHostingUnitByKey(o.HostingUnitKey);
                Window tmp = new inpersonalArea(hu.Owner.HostKey);
                tmp.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("אין אפשרות לשנות סטטוס כעת", "העדכון נכשל", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                MainWindow.bl.sendAnEmail(o);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "בעיה במייל ", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //  b.Close();
            MessageBox.Show("\nהאימייל נשלח","תודה שביצעתם את ההזמנה דרכינו:) ", MessageBoxButton.OK, MessageBoxImage.Information);

        }






        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit h = MainWindow.bl.getHostingUnitByKey(o.HostingUnitKey);
            this.Close();
            Window tmp = new inpersonalArea(h.Owner.HostKey);

            // Window tmp = new openWindow();
            tmp.ShowDialog();
        }
        //image button
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MainWindow.bl.updateOrderStatus(o, (orderStatus)cb_statustoUpdate.SelectedItem);//@@@@@@@@@@@222
                MessageBox.Show("הסטטוס עודכן בהצלחה!", "העדכון הוצלח", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();

                Window tmp = new orderListWindow(Convert.ToInt32(MainWindow.bl.getGuestRequestByKey(o.GuestRequestKey).ID));
                tmp.ShowDialog();
            }
            catch (Exception x)
            {
                MessageBox.Show("\nהזמנה נסגרה בחוסר הענות לקוח" +
                    "אין אפשרות לשנות סטטוס כעת", "העדכון נכשל", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
