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
    /// Interaction logic for clientWindow.xaml
    /// </summary>
    public partial class clientWindow : Window
    {      
        //helpers

        int index =0;

        GuestRequest gr;
        bool select_cmbPool = false;
        bool select_cmbarea = false;
        bool select_cmbchildrenatr = false;
        bool select_cmbtype = false;
        bool select_cmbgarden = false; 
        bool select_cmbjacuzzi = false;
        bool select_entry = false;
        bool select_release = false;
       
        //constructor
        public clientWindow()
        {

            InitializeComponent();
            cb_pool.ItemsSource = Enum.GetValues(typeof(options));
            cb_garden.ItemsSource = Enum.GetValues(typeof(options));
            cb_jacuzzi.ItemsSource = Enum.GetValues(typeof(options));
            cb_childrenatr.ItemsSource = Enum.GetValues(typeof(options));
            cb_subarea.ItemsSource = Enum.GetValues(typeof(subareaHosting));
            cb_type.ItemsSource = Enum.GetValues(typeof(typeHosting));
            cb_area.ItemsSource = Enum.GetValues(typeof(areaHosting));
        
            CB_hostingunit.DataContext = MainWindow.bl.Gethostingunitkeys();
            TxtBx_numkids.Text = "";
            this.DP_entrydate.SelectedDate = DateTime.Now.Date;
            this.DP_releasedate.SelectedDate = DateTime.Now.Date;
            gr=new GuestRequest();
            gr.EntryDate = new DateTime(2020, 09, 02);
            //gr.EntryDate = new DateTime(2020,02,08);
            gr.ReleaseDate = new DateTime(2020, 09, 02);

            addgrgrid.DataContext = gr;

        }

        //LostFocus
        private void TxtBx_FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_FirstName.Text.All(char.IsLetter))
            {
                // MessageBox.Show("שם צריך להיות מורכב מאותיות בלבד!");
             //   Image myimage = Image.FromFile("C:/Users/elina/Desktop/c++/מיני פרויקט/פרויקט 2020/PL/UI/Images\like.jpg");

               
                MessageBox.Show("יש להשתמש בטקסט בלבד", "טעות בהכנסת נתונים",MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_FirstName.BorderBrush = Brushes.Red;

            }
            else
                TxtBx_FirstName.BorderBrush = Brushes.Gray;

        }
        private void TxtBx_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_mail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("@gmail.com יש למלא איימל תקין\n עם סיומת!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_mail.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_mail.BorderBrush = Brushes.Gray;
        }
        private void TxtBx_LastName_LostFocus(object sender, RoutedEventArgs e)
        {

            if (!TxtBx_LastName.Text.All(char.IsLetter))
            {
                MessageBox.Show("יש להשתמש בטקסט בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_LastName.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_LastName.BorderBrush = Brushes.Gray;

        }
        private void TxtBx_numadults_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_numadults.Text.All(char.IsDigit))
            {
                    MessageBox.Show("נא להשלים במספר מבוגרים מספר בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_numadults.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_numadults.BorderBrush = Brushes.Gray;
        }
        private void TxtBx_numkids_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_numkids.Text.All(char.IsDigit))
            {
                MessageBox.Show("נא להשלים במספר ילדים מספר בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_numkids.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_numkids.BorderBrush = Brushes.Gray;


        }
        private void DP_releasedate_LostFocus(object sender, RoutedEventArgs e)
        {
            gr.ReleaseDate = Convert.ToDateTime(DP_releasedate.SelectedDate);
            select_release = true;


        }
        private void DP_entrydate_LostFocus(object sender, RoutedEventArgs e)
        {
            gr.EntryDate = Convert.ToDateTime(DP_entrydate.SelectedDate);
            select_entry = true;
          
        }
        private void TxtBx_numbaby_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_numbaby.Text.All(char.IsDigit))
            {
                MessageBox.Show("נא להשלים במספר תינוקות מספר בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_numbaby.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_numbaby.BorderBrush = Brushes.Gray;

        }
        private void CB_hostingunit_LostFocus(object sender, RoutedEventArgs e)
        {
            tbHostName.Text = CB_hostingunit.Text;
            //CB_hostingunit.Text
        }
        private void TxtBx_id_LostFocus(object sender, RoutedEventArgs e)
        {
            if(!TxtBx_id.Text.All(char.IsDigit))
            {
                MessageBox.Show("תעודת זהות  מורכבת ממספרים בלבד", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

                TxtBx_id.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_id.BorderBrush = Brushes.Gray;
            if (TxtBx_id.Text.Length < 9)
            {
                MessageBox.Show("תעודת זהות בנויה מ9 ממספרים בלבד", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

                TxtBx_id.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_id.BorderBrush = Brushes.Gray;

        }
       
        // helper func
        bool ifAllFill()
        {
            if (select_cmbPool == true && TxtBx_mail.Text != "" && TxtBx_FirstName.Text != "" &&
                TxtBx_LastName.Text != "" && (TxtBx_numadults.Text != "0" || TxtBx_numkids.Text != "0") &&
                select_cmbarea&&select_cmbtype&&select_cmbchildrenatr&&
                select_entry &&select_release&& select_cmbgarden&&select_cmbjacuzzi)
            {                                                                                    
                return true;
            }

            return false;
        }
      
        
        //button
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Window tmp = new openWindow();
            tmp.ShowDialog();
        }
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (ifAllFill())
            {
                try
                {
              if(TxtBx_FirstName.BorderBrush == Brushes.Gray && 
                        TxtBx_mail.BorderBrush == Brushes.Gray&&
                        TxtBx_LastName.BorderBrush == Brushes.Gray&&
                        (TxtBx_numadults.BorderBrush == Brushes.Gray||
                        TxtBx_numkids.BorderBrush == Brushes.Gray )&&
                        TxtBx_id.BorderBrush == Brushes.Gray)
                    {

                     MainWindow.bl.addGuestRequest(gr);

                    MessageBox.Show("בקשה הוספה בהצלחה!\n מקווים שתהנה בחופשה(:","הבקשה הוספה לרשימת הבקשות",MessageBoxButton.OK, MessageBoxImage.Information);
                    gr = new GuestRequest();
                        gr.EntryDate = DateTime.Now;
                        gr.ReleaseDate = DateTime.Now;
                        cb_area.Text = "";
                        cb_subarea.Text = "";
                        cb_garden.Text = " ";
                        cb_jacuzzi.Text = "";
                        cb_childrenatr.Text = "";
                        cb_type.Text = "";
                        cb_area.Text = "";
                        cb_pool.Text = "";
                    addgrgrid.DataContext = gr;
                   } 
              else
                        MessageBox.Show("השדות אינם מלאות כראוי,\n אנא בדוק שנית", "חסרים נתונים", MessageBoxButton.OK, MessageBoxImage.Error);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());

                }
            }
            else
                MessageBox.Show("את|ה מתבקש למלא את כל השדות", "חסרים נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        //מופעלת כשבוחרים משהו 
         private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
         private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
         private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {

            gr = new GuestRequest();
            addgrgrid.DataContext = gr;
        }
         private void butten_showgrs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (MainWindow.bl.GetGuestRequestList().Exists(item => item.ID == (Convert.ToString(txt_id.Text))))
                    {

                    cb_grkey.BorderBrush = Brushes.Gray;
                    cb_grkey.DataContext = MainWindow.bl.listMyRequest(txt_id.Text);
                    txt_id.IsReadOnly=true;
                }
            else
                {
                    cb_grkey.BorderBrush = Brushes.Red;

                    MessageBox.Show("תעודת זהות לא קיימת", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {         

         //   cb_grkey.ItemsSource = null;

            txt_id.IsReadOnly = false;
            txt_id.BorderBrush = Brushes.Red;
            txt_id.Background = Brushes.White;
        }

        private void button_zion_Click(object sender, RoutedEventArgs e)
        {
            gr = MainWindow.bl.getGuestRequestByKey(Convert.ToInt32(cb_grkey.SelectedItem));
            Order o = MainWindow.bl.GetOrderList().Find(item => item.GuestRequestKey == gr.GuestRequestKey);
            
            Window tmp = new givezion(Convert.ToInt32(o.HostingUnitKey));
            this.Close();
            tmp.ShowDialog();
        }


        //SelectionChanged
        private void cb_pool_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            select_cmbPool = true;
            gr.Pool = (options)cb_pool.SelectedIndex;
        }

        private void cb_area_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            select_cmbarea = true;
            gr.Area = (areaHosting)cb_area.SelectedIndex;
        }

        private void cb_childrenatr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            select_cmbchildrenatr = true;
            gr.ChildrensAttractions = (options)cb_childrenatr.SelectedIndex;
        }

        private void cb_type_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            select_cmbtype = true;
            gr.Type = (typeHosting)cb_type.SelectedIndex;
        }

        private void cb_garden_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            select_cmbgarden = true;
            gr.Garden = (options)cb_garden.SelectedIndex;

        }

        private void cb_jacuzzi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            select_cmbjacuzzi = true;
            gr.Jacuzzi = (options)cb_jacuzzi.SelectedIndex;
        }

        private void CB_hostingunit_SelectionChanged(object sender, SelectionChangedEventArgs e)
       
        {
            index = 0;
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));
            showTxtBx_FirstName.Text =Convert.ToString( x.Owner.PrivateName);
            showTxtBx_LastName.Text = Convert.ToString(x.Owner.FamilyName);
            showTxtBx_phoneNumber.Text = Convert.ToString(x.Owner.FhoneNumber);
            showTxtBx_HostingUnitName.Text = Convert.ToString(x.HostingUnitName);
            showTxtBx_maxGuest.Text = Convert.ToString(x.maxGuest);
            showTxtBx_priceAdults.Text = Convert.ToString(x.priceAdults);
            showTxtBx_priceChildren.Text = Convert.ToString(x.priceChildren);
            showcb_subareaHost.Text = Convert.ToString(x.SubArea);
            showTxtBx_mail.Text = Convert.ToString(x.Owner.MailAddress);
            showcb_med.Text = Convert.ToString(x.medsatisfication);
            if (x.Uris.Count != 0)
            {
                image.Source = new BitmapImage(x.Uris[index]);
            }
            else {
                left.Visibility = Visibility.Hidden;
                right.Visibility = Visibility.Hidden;
            }
        }
        private void cb_grkey_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GuestRequest gr = new GuestRequest();
            Order o = new Order();
            gr = MainWindow.bl.getGuestRequestByKey(Convert.ToInt32(cb_grkey.SelectedItem)); //gr = MainWindow.bl.getGuestRequestByKey(Convert.ToInt32(cb_grkey.SelectedItem));
            o = MainWindow.bl.GetOrderList().Find(item => item.GuestRequestKey == gr.GuestRequestKey);
            if (o == null)
            {
                button_zion.Visibility = Visibility.Hidden;
            }
            else
            {
                if (o.Status == orderStatus.closedResponse)
                {
                    // button_zion.Visibility = Visibility.Collapsed;
                    button_zion.Visibility = Visibility.Visible;
                }
                else
                {
                    button_zion.Visibility = Visibility.Hidden;
                }
            }
            show_TxtBx_mail1.Text = Convert.ToString(gr.MailAddress);
            show_TxtBx_id1.Text = Convert.ToString(gr.ID);
            show_TxtBx_FirstName1.Text = Convert.ToString(gr.PrivateName);
            show_TxtBx_LastName1.Text = Convert.ToString(gr.FamilyName);
            show_TxtBx_numadults1.Text = Convert.ToString(gr.Adults);
            show_TxtBx_numkids1.Text = Convert.ToString(gr.Children);
            show_cb_pool1.Text = Convert.ToString(gr.Pool);
            show_cb_jacuzzi1.Text = Convert.ToString(gr.Jacuzzi);
            show_cb_garden1.Text = Convert.ToString(gr.Garden);
            show_DP_entrydate1.Text = Convert.ToString(gr.EntryDate);
            show_DP_releasedate1.Text = Convert.ToString(gr.ReleaseDate);
            show_TxtBx_numbaby1.Text = Convert.ToString(gr.baby);
            show_cb_area1.Text = Convert.ToString(gr.Area);
            show_cb_subarea1.Text = Convert.ToString(gr.SubArea);
            show_cb_childrenatr1.Text = Convert.ToString(gr.ChildrensAttractions);
            show_cb_type1.Text = Convert.ToString(gr.Type);

        }

        //image buttons
        private void ChangeImage(object sender, MouseButtonEventArgs e)
        {
           HostingUnit hu= MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));
           image.Source = new BitmapImage(hu.Uris[0]);//#################################
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));

            if (index == 0)
            {
                index = x.Uris.Count;
            }
            else {  
                index--;
            }
            image.Source = new BitmapImage(x.Uris[index]);

        }

        private void Image_Click_1(object sender, RoutedEventArgs e)
        {
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));

            if (index == x.Uris.Count-1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            image.Source = new BitmapImage(x.Uris[index]);

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));

            if (index == 0)
            {
                index = x.Uris.Count-1;
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

        private void cb_subarea_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gr.SubArea = (subareaHosting)cb_subarea.SelectedIndex;
        }

       

       
    }
}