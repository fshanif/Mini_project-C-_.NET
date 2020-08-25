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
    /// Interaction logic for clientListWindow.xaml
    /// </summary>
    public partial class clientListWindow : Window
    {
        //helpers
        GuestRequest gr = new GuestRequest();
        HostingUnit hu = new HostingUnit();
        int x1 = 0;
        List<HostingUnit> hostingUnitsnew = new List<HostingUnit>();

        List<int> listgrkByArea = new List<int>();


        //constructor
        public clientListWindow(List<HostingUnit> hostingUnits)
        {
            InitializeComponent();
            foreach(HostingUnit item in hostingUnits)
            {
                hostingUnitsnew.Add(item);
            }
           // List<Order> olist = MainWindow.bl.GetOrderList();
            foreach(HostingUnit item1 in hostingUnits)
            {
                foreach(GuestRequest item2 in MainWindow.bl.GetGuestRequestList())
                {
                    Order o = MainWindow.bl.GetOrderList().Find(item => item.GuestRequestKey == item2.GuestRequestKey);
                    if (o == null) {
                        if (Convert.ToString(item1.SubArea) == Convert.ToString(item2.Area) || Convert.ToString(item2.Area) == "all")
                            listgrkByArea.Add(item2.GuestRequestKey);
                    }
                }
            }
            cb_grKey.DataContext = listgrkByArea;

        }



        // button
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void cb_grKey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
           GuestRequest x = MainWindow.bl.getGuestRequestByKey(Convert.ToInt32(cb_grKey.SelectedItem));
            x1 =Convert.ToInt32( x.ID);
            gr.GuestRequestKey = x.GuestRequestKey;
            gr.ID = x.ID;
            gr.Adults = x.Adults;
            gr.Area = x.Area;
            gr.baby = x.baby;
            gr.Children = x.Children;
            gr.ChildrensAttractions = x.ChildrensAttractions;
            gr.EntryDate = x.EntryDate;
            gr.FamilyName = x.FamilyName;
            gr.Garden = x.Garden;
            gr.GuestRequestKey = x.GuestRequestKey;
            gr.Jacuzzi = x.Jacuzzi;
            gr.MailAddress = x.MailAddress;
            gr.Pool = x.Pool;
            gr.PrivateName = x.PrivateName;
            gr.RegistrationDate = x.RegistrationDate;
            gr.ReleaseDate = x.ReleaseDate;
            gr.Status = x.Status;
            gr.SubArea = x.SubArea;
            gr.Type = x.Type;
            show_TxtBx_FirstName1.Text = Convert.ToString(gr.PrivateName);
            show_TxtBx_LastName1.Text = Convert.ToString(gr.FamilyName);
            show_TxtBx_mail1.Text = Convert.ToString(gr.MailAddress);
           show_TxtBx_numadults1.Text = Convert.ToString(gr.Adults);
            show_TxtBx_numbaby1.Text = Convert.ToString(gr.baby);
            show_TxtBx_numkids1.Text = Convert.ToString(gr.Children);
            show_txt_area1.Text = Convert.ToString(gr.Area);
            show_txt_garden1.Text = Convert.ToString(gr.Garden);
            show_txt_jacuzzi1.Text = Convert.ToString(gr.Jacuzzi);
            show_txt_pool1.Text = Convert.ToString(gr.Pool);
           show_txt_type1.Text = Convert.ToString(gr.Type);
            show_txt_subarea1.Text = Convert.ToString(gr.SubArea);
            show_txt_childrenatr1.Text = Convert.ToString(gr.ChildrensAttractions);
            show_DP_entrydate1.SelectedDate = gr.EntryDate;
            show_DP_RegistrationDate.SelectedDate = gr.RegistrationDate;
            show_DP_releasedate1.SelectedDate = gr.ReleaseDate;
         
        }

        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            x1=hostingUnitsnew[0].Owner.HostKey;
            this.Close();
            
            Window tmp = new ordersWindow(Convert.ToInt32(x1));
            tmp.ShowDialog();
        }

        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Buttonaddorder_Click(object sender, RoutedEventArgs e)
        {
            try
            {  
                if (!(txt_hostingUnitName.Text == ""))
                {
                    Order o = new Order();
                    o.Status = orderStatus.NotYetAddressed;
                    o.GuestRequestKey = gr.GuestRequestKey;
                    o.HostingUnitKey = hu.HostingUnitKey;
                    //o.OrderDate=
                    o.CreateDate = DateTime.Now.Date;
                    MainWindow.bl.addOrder(o);
                    listgrkByArea.Remove(o.GuestRequestKey);
                    //  MainWindow.bl.updateOrderStatus(o, orderStatus.emailSent);
                    cb_grKey.DataContext = listgrkByArea;

                }
                else
                    MessageBox.Show("אנא הכנס מספר יחידה", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            MessageBox.Show("ההזמנה הוספה בהצלחה", "הוספת הזמנה", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
            Window tmp = new clientListWindow(hostingUnitsnew);
            tmp.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!(txt_hostingUnitName.Text == ""))
                {
                    HostingUnit hostingUnit2 = new HostingUnit();
                    hostingUnit2 = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(txt_hostingUnitName.Text));
                    this.Close();
                    Window tmp = new groupByMaxPeople(hu);
                    tmp.ShowDialog();
                }
                else
                    MessageBox.Show("אנא הכנס מספר יחידה", "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }


        //LostFocus
        private void txt_hostingUnitName_LostFocus(object sender, RoutedEventArgs e)
        {
            int hukey = Convert.ToInt32(txt_hostingUnitName.Text);
            hu = hostingUnitsnew.Find(item => item.HostingUnitKey == hukey);
        }

       
    }
}
