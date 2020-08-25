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
    /// Interaction logic for updateHostingUnitWindow.xaml
    /// </summary>
    public partial class updateHostingUnitWindow : Window
    {
        bool selectcbbank = false;
        HostingUnit hu = new HostingUnit();
        int helperID;
        public updateHostingUnitWindow(int myid)
        {
            InitializeComponent();
            helperID = myid;
            CB_hostingunit.DataContext = MainWindow.bl.gethostingUnitKeyByTz(helperID);
            cb_subareaHost.ItemsSource = Enum.GetValues(typeof(areaHosting));
            hu.Owner = new Host();
            hu.Uris = new List<Uri>();
            hu.Diary = new bool[12, 31];
            hu.Owner.BankBranchDetails = new BankBranch();
            updateHostingUnit.DataContext = hu;
            cb_bank.ItemsSource = MainWindow.bl.GetBankBranchList();
        }
        private void CB_hostingunit_LostFocus(object sender, RoutedEventArgs e)
        {
            tbHostName.Text = CB_hostingunit.Text;
            //CB_hostingunit.Text
        }
        private void CB_hostingunit_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));
            hu.feeAll = x.feeAll;
            hu.HostingUnitKey = x.HostingUnitKey;
            hu.Owner.HostKey = x.Owner.HostKey;
            hu.Owner.PrivateName = x.Owner.PrivateName;
            hu.Owner.FamilyName = x.Owner.FamilyName;
            hu.Owner.FhoneNumber = x.Owner.FhoneNumber;
            hu.Owner.MailAddress = x.Owner.MailAddress;
            hu.Owner.BankAccountNumber = x.Owner.BankAccountNumber;
            hu.Owner.BankBranchDetails = x.Owner.BankBranchDetails;
            hu.Owner.CollectionClearance = x.Owner.CollectionClearance;
            hu.HostingUnitName = x.HostingUnitName;
            hu.Diary = x.Diary;
            hu.SubArea = x.SubArea;
            hu.maxGuest = x.maxGuest;
            hu.priceAdults = x.priceAdults;
            hu.priceChildren = x.priceChildren;
            hu.Owner.code = x.Owner.code;




            TxtBx_FirstName.Text = Convert.ToString(x.Owner.PrivateName);
            TxtBx_LastName.Text = Convert.ToString(x.Owner.FamilyName);
            TxtBx_phoneNumber.Text = Convert.ToString(x.Owner.FhoneNumber);
            TxtBx_HostingUnitName.Text = Convert.ToString(x.HostingUnitName);
            TxtBx_maxGuest.Text = Convert.ToString(x.maxGuest);
            TxtBx_priceAdults.Text = Convert.ToString(x.priceAdults);
            TxtBx_priceChildren.Text = Convert.ToString(x.priceChildren);
            cb_subareaHost.Text = Convert.ToString(x.SubArea);
            TxtBx_mail.Text = Convert.ToString(x.Owner.MailAddress);
            TxtBx_bankAccountNumber.Text = Convert.ToString(x.Owner.BankAccountNumber);
            cb_collectionClear.IsChecked = Convert.ToBoolean(x.Owner.CollectionClearance);
           cb_bank.Text= Convert.ToString(x.Owner.BankBranchDetails);
            TxtBx_code.Text = Convert.ToString(x.Owner.code);
            TxtBx_maxGuest.Text = Convert.ToString(x.maxGuest);
            TxtBx_priceAdults.Text = Convert.ToString(x.priceAdults);
            TxtBx_priceChildren.Text = Convert.ToString(x.priceChildren);
            txt_subareaHost.Text = Convert.ToString(x.SubArea);//


        }
        private void TxtBx_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_mail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("@gmail.com הסיומת של כתובת מייל צריך להיות!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_mail.BorderBrush = Brushes.Red;

            }
            else
                TxtBx_mail.BorderBrush = Brushes.Gray;

        }

        private void TxtBx_FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_FirstName.Text.All(char.IsLetter))
            {
                MessageBox.Show("שם צריך להיות מורכב מאותיות בלבד", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_FirstName.BorderBrush = Brushes.Red;

            }
            else
                TxtBx_FirstName.BorderBrush = Brushes.Gray;

        }

        private void TxtBx_LastName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_LastName.Text.All(char.IsLetter))
            {
                MessageBox.Show("שם צריך להיות מורכב מאותיות בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_LastName.BorderBrush = Brushes.Red;

            }
            else
                TxtBx_LastName.BorderBrush = Brushes.Gray;

        }

        private void TxtBx_phoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_phoneNumber.Text.StartsWith("05"))
            {
                MessageBox.Show("מספר טלפון צריך להיות מספר טלפון נייד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_phoneNumber.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_phoneNumber.BorderBrush = Brushes.Gray;

        }

        private void TxtBx_bankAccountNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_bankAccountNumber.Text.All(char.IsDigit))
            {
                MessageBox.Show("מספר חשבון בנק צריך להכיל מספרים בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_bankAccountNumber.BorderBrush = Brushes.Red;
            }
            TxtBx_bankAccountNumber.BorderBrush = Brushes.Gray;

        }

        private void Button_update_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                // hu.Owner.CollectionClearance = cb_collectionClear;
                hu.HostingUnitKey = Convert.ToInt32(CB_hostingunit.SelectedItem);
                if ( TxtBx_mail.BorderBrush == Brushes.Red || TxtBx_FirstName.BorderBrush == Brushes.Red
                                   || TxtBx_LastName.BorderBrush == Brushes.Red || TxtBx_phoneNumber.BorderBrush == Brushes.Red || TxtBx_bankAccountNumber.BorderBrush == Brushes.Red
 || TxtBx_HostingUnitName.BorderBrush == Brushes.Red || TxtBx_code.BorderBrush == Brushes.Red
                                  || TxtBx_maxGuest.BorderBrush == Brushes.Red || TxtBx_priceAdults.BorderBrush == Brushes.Red || TxtBx_priceAdults.BorderBrush == Brushes.Red)
                    MessageBox.Show("אנא מלא את השדות כראוי!", "אנא בדוק שנית את השדות ", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {

                    hu.SubArea = (areaHosting)cb_subareaHost.SelectedIndex;
                    MainWindow.bl.updateHostingUnit(hu);
                    MessageBox.Show("בקשה עודכנה!\n(:", "הבקשה עודכנה בהצלחה", MessageBoxButton.OK, MessageBoxImage.Information);
                    hu = new HostingUnit();

                    hu.Owner = new Host();
                    hu.Uris = new List<Uri>();
                    hu.Diary = new bool[12, 31];
                    hu.Owner.BankBranchDetails = new BankBranch();
                    cb_bank.Text = "--please choose--";
                    updateHostingUnit.DataContext = hu;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);

            }

        }
        private void TxtBx_HostingUnitName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_HostingUnitName.Text.All(char.IsLetter))
            {
                MessageBox.Show("שם המלון צריך להיות מורכב מאותיות בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_HostingUnitName.BorderBrush = Brushes.Red;

            }
            else
                TxtBx_HostingUnitName.BorderBrush = Brushes.Gray;

        }

        private void TxtBx_code_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_code.Text.All(char.IsLetterOrDigit))
            {
                MessageBox.Show("קוד יכול להכיל רק אותיות או מספרים!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_HostingUnitName.BorderBrush = Brushes.Red;
                TxtBx_code.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_HostingUnitName.BorderBrush = Brushes.Gray;

        }

        private void TxtBx_maxGuest_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBx_maxGuest.Text.Equals(0) && !TxtBx_maxGuest.Text.All(char.IsDigit))
            {
                MessageBox.Show("מספר מקסימלי של אורחים לא יכול להיות שווה ל 0\n ומספר מקסימלי של אורחים יכול להכיל מספרים בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_maxGuest.BorderBrush = Brushes.Red;
            }
            else
            {

                if (TxtBx_maxGuest.Text.Equals(0))
                {
                    MessageBox.Show("מספר מקסימלי של אורחים לא יכול להיות שווה ל 0!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                    TxtBx_maxGuest.BorderBrush = Brushes.Red;
                }
                else
                    TxtBx_maxGuest.BorderBrush = Brushes.Gray;

                if (!TxtBx_maxGuest.Text.All(char.IsDigit))
                {
                    MessageBox.Show("  מספר מקסימלי של אורחים יכול להכיל מספרים בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

                    TxtBx_maxGuest.BorderBrush = Brushes.Red;

                }
                else
                    TxtBx_maxGuest.BorderBrush = Brushes.Gray;

            }
        }

        private void TxtBx_priceAdults_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBx_priceAdults.Text.All(char.IsLetter))
            {
                MessageBox.Show("מחיר של מבוגר צריך להכיל מספרים בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_priceAdults.BorderBrush = Brushes.Red;

            }
            else
                TxtBx_maxGuest.BorderBrush = Brushes.Red;

        }

        private void TxtBx_priceChildren_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBx_priceChildren.Text.All(char.IsLetter))
            {
                MessageBox.Show("מחיר של ילד ותינוק צריך להכיל מספרים בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_priceAdults.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_maxGuest.BorderBrush = Brushes.Red;

        }


        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            HostingUnit x = MainWindow.bl.getHostingUnitByKey(Convert.ToInt32(CB_hostingunit.SelectedItem));
            hu.feeAll = x.feeAll;
            hu.HostingUnitKey = x.HostingUnitKey;
            hu.Owner.HostKey = x.Owner.HostKey;
            hu.Owner.PrivateName = x.Owner.PrivateName;
            hu.Owner.FamilyName = x.Owner.FamilyName;
            hu.Owner.FhoneNumber = x.Owner.FhoneNumber;
            hu.Owner.MailAddress = x.Owner.MailAddress;
            hu.Owner.BankAccountNumber = x.Owner.BankAccountNumber;
            hu.Owner.BankBranchDetails = x.Owner.BankBranchDetails;
            hu.Owner.BankBranchDetails.BankNumber = x.Owner.BankBranchDetails.BankNumber;
            hu.Owner.BankBranchDetails.BankName = x.Owner.BankBranchDetails.BankName;
            hu.Owner.BankBranchDetails.BranchNumber = x.Owner.BankBranchDetails.BranchNumber;
            hu.Owner.BankBranchDetails.BranchAddress = x.Owner.BankBranchDetails.BranchAddress;
            hu.Owner.BankBranchDetails.BranchCity = x.Owner.BankBranchDetails.BranchCity;
            hu.Owner.CollectionClearance = x.Owner.CollectionClearance;
            hu.HostingUnitName = x.HostingUnitName;
            hu.Diary = x.Diary;
            hu.SubArea = x.SubArea;
            hu.maxGuest = x.maxGuest;
            hu.priceAdults = x.priceAdults;
            hu.priceChildren = x.priceChildren;
            hu.Owner.code = x.Owner.code;


            TxtBx_FirstName.Text = Convert.ToString(x.Owner.PrivateName);
            TxtBx_LastName.Text = Convert.ToString(x.Owner.FamilyName);
            TxtBx_phoneNumber.Text = Convert.ToString(x.Owner.FhoneNumber);
            TxtBx_HostingUnitName.Text = Convert.ToString(x.HostingUnitName);
            TxtBx_maxGuest.Text = Convert.ToString(x.maxGuest);
            TxtBx_priceAdults.Text = Convert.ToString(x.priceAdults);
            TxtBx_priceChildren.Text = Convert.ToString(x.priceChildren);
            cb_subareaHost.Text = Convert.ToString(x.SubArea);
            TxtBx_mail.Text = Convert.ToString(x.Owner.MailAddress);
            TxtBx_bankAccountNumber.Text = Convert.ToString(x.Owner.BankAccountNumber);
            cb_collectionClear.IsChecked = Convert.ToBoolean(x.Owner.CollectionClearance);
            TxtBx_code.Text = Convert.ToString(x.Owner.code);
            TxtBx_maxGuest.Text = Convert.ToString(x.maxGuest);
            TxtBx_priceAdults.Text = Convert.ToString(x.priceAdults);
            TxtBx_priceChildren.Text = Convert.ToString(x.priceChildren);
            cb_subareaHost.Text = Convert.ToString(x.SubArea);
        }
        private void cb_collectionClear_LostFocus(object sender, RoutedEventArgs e)
        {

        }
       



        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Window tmp = new inpersonalArea(helperID);
            tmp.ShowDialog();
        }



        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        
        private void TxtBx_mail_GotFocus(object sender, RoutedEventArgs e)
        {
            TxtBx_mail.BorderBrush = Brushes.Black;
        }

        private void cb_bank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectcbbank = true;
            hu.Owner.BankBranchDetails = (BankBranch)cb_bank.SelectedItem;
        }
    }
}

