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
    /// Interaction logic for hostWindow.xaml
    /// </summary>
    public partial class hostWindow : Window
    { bool exist;
        HostingUnit hu = new HostingUnit();
        bool selectcbsubarea = false;
        bool selectcbbank = false;
        //constructor
        public hostWindow()
        {
            InitializeComponent();
            exist = false;
            cb_subareaHost.ItemsSource = Enum.GetValues(typeof(areaHosting));
            hu.Owner = new Host();
            hu.Uris = new List<Uri>();
            hu.Diary = new bool[12, 31];
            hu.Owner.BankBranchDetails = new BankBranch();
            addHostingUnitGrid.DataContext = hu;
            cb_bank.ItemsSource = MainWindow.bl.GetBankBranchList();
        }
        //lost focus
        private void TxtBx_mail_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_mail.Text.EndsWith("@gmail.com"))
            {
                MessageBox.Show("@gmail.com הכנס מייל תקין עם סיומת של!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

                TxtBx_mail.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_mail.BorderBrush = Brushes.Gray;
            }
        private void TxtBx_FirstName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_FirstName.Text.All(char.IsLetter))
            {
                MessageBox.Show("יש להשתמש בטקסט בלבד", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_FirstName.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_FirstName.BorderBrush = Brushes.Gray;    
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
        private void TxtBx_phoneNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_phoneNumber.Text.StartsWith("05"))
            {
                MessageBox.Show("יש להקליד מספר טלפון תקין!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_phoneNumber.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_phoneNumber.BorderBrush = Brushes.Gray;

            if (TxtBx_phoneNumber.Text.Length<10)
            {
                MessageBox.Show("מספר טלפון מכיל עשרה מספרים!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_phoneNumber.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_phoneNumber.BorderBrush = Brushes.Gray;

        }
        private void TxtBx_bankAccountNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_bankAccountNumber.Text.All(char.IsDigit))
            {
                MessageBox.Show("יש להקליד מספרים בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_bankAccountNumber.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_bankAccountNumber.BorderBrush = Brushes.Gray;

        }
      
        
        private void TxtBx_HostingUnitName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!TxtBx_HostingUnitName.Text.All(char.IsLetter))
            {
                MessageBox.Show("יש להקליד אותיות בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

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
                TxtBx_code.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_code.BorderBrush = Brushes.Gray;

            if (TxtBx_code.Text == TxtBx_FirstName.Text)
            {
                MessageBox.Show("סיסמא שהיא שם המשתמש קל לגלות אותה", "סיסמא ברמה חלשה", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            if (TxtBx_code.Text == TxtBx_LastName.Text)
            {
                MessageBox.Show("סיסמא שהיא שם משפחה המשתמש קל לגלות אותה", "סיסמא ברמה חלשה", MessageBoxButton.OKCancel, MessageBoxImage.Warning);

            }
            if (TxtBx_code.Text == TxtBx_phoneNumber.Text)
            {
                MessageBox.Show("סיסמא שהיא מספר הטלפון של המשתמש קל לגלות אותה", "סיסמא ברמה חלשה", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            }
            if (exist == true)
            {


                Host hu = MainWindow.bl.GetHostingUnitsList().Find(item => item.Owner.HostKey == Convert.ToInt32(TxtBx_hostkey.Text)).Owner;
                if (hu.code != TxtBx_code.Text)
                {
                    MessageBox.Show("הסיסמה שגויה,\n נסה שוב!", "מארח קיים הכנס סיסמא קיימת", MessageBoxButton.OK, MessageBoxImage.Error);

                    TxtBx_code.BorderBrush = Brushes.Red;
                }
                else
                {
                    MessageBox.Show("מארח קיים הכנסת סיסמא קיימת", "מארח קיים שימו לב", MessageBoxButton.OK, MessageBoxImage.Information);

                    TxtBx_code.BorderBrush = Brushes.Gray;
                }
            }
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
                TxtBx_priceAdults.BorderBrush = Brushes.Gray;

        }
        private void TxtBx_priceChildren_LostFocus(object sender, RoutedEventArgs e)
        {
            if (TxtBx_priceChildren.Text.All(char.IsLetter))
            {
                MessageBox.Show("מחיר של ילד ותינוק צריך להכיל מספרים בלבד!", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);
                TxtBx_priceChildren.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_priceChildren.BorderBrush = Brushes.Gray;

        }
        private void TxtBx_hostkey_LostFocus(object sender, RoutedEventArgs e)
        {
            exist = false;
            if (!TxtBx_hostkey.Text.All(char.IsDigit))
            {
                MessageBox.Show("תעודת זהות יכולה להיות מורכבת ממספרים בלבד", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

                TxtBx_hostkey.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_hostkey.BorderBrush = Brushes.Gray;
            
            if (TxtBx_hostkey.Text.Length < 9)
            {
                MessageBox.Show("תעודת זהות בנויה מ9 ממספרים בלבד", "טעות בהכנסת נתונים", MessageBoxButton.OK, MessageBoxImage.Error);

                TxtBx_hostkey.BorderBrush = Brushes.Red;
            }
            else
                TxtBx_hostkey.BorderBrush = Brushes.Gray;
            if (MainWindow.bl.gethostingUnitKeyByTz(Convert.ToInt32(TxtBx_hostkey.Text)).Count != 0)
            {
              exist = true;
            }

        }
        //helper function
        bool ifAllFill()
        {
            if (TxtBx_FirstName.Text != "" && TxtBx_mail.Text != "" && TxtBx_LastName.Text != "" && TxtBx_phoneNumber.Text != ""
                && TxtBx_bankAccountNumber.Text != ""  
               && TxtBx_HostingUnitName.Text != "" && TxtBx_code.Text != "" && TxtBx_maxGuest.Text != "" && TxtBx_priceAdults.Text != ""
               && TxtBx_priceChildren.Text != "" && selectcbsubarea&& selectcbbank)
            {
                return true;
            }

            return false;
        }
        //button
        private void cumputter_Click(object sender, RoutedEventArgs e)
        {
            // Create OpenFileDialog
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();

            // Launch OpenFileDialog by calling ShowDialog method
            Nullable<bool> result = openFileDlg.ShowDialog();
            // Get the selected file name and display in a TextBox.
            // Load content of file in a TextBlock
            if (result == true)
            {
                TxtBx_uris.Text = openFileDlg.FileName;
            }
        }
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Window tmp = new openWindow();
            tmp.ShowDialog();
        }
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void GoBotton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.bl.GetHostingUnitsList().Exists(item => item.Owner.HostKey == Convert.ToInt32(TZ_box.Text)))
            {
                Host hu = MainWindow.bl.GetHostingUnitsList().Find(item => item.Owner.HostKey == Convert.ToInt32(TZ_box.Text)).Owner;
                if (hu.code == password_box.Password)
                {
                    this.Close();
                    new inpersonalArea(Convert.ToInt32(TZ_box.Text)).ShowDialog();
                }
                else
                {
                    MessageBox.Show("הסיסמה שגויה,\n נסה שוב!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
                MessageBox.Show("ת ז שהוכנסה שגויה,\n נסה שוב!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

        }
        private void buttonUris_Click(object sender, RoutedEventArgs e)
        {

            if (TxtBx_uris.Text != "")
            {
                hu.Uris.Add(new Uri(TxtBx_uris.Text));
                TxtBx_uris.Text = "";
            }
        }
        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            hu = new HostingUnit();
            hu.Uris = new List<Uri>();
            hu.Owner = new Host();
            hu.Diary = new bool[12, 31];
            hu.Owner.BankBranchDetails = new BankBranch();
            addHostingUnitGrid.DataContext = hu;
        }
        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Button_Add_Click(object sender, RoutedEventArgs e)
        {
            if (ifAllFill())
            {
                try
                {
                    if (TxtBx_mail.BorderBrush == Brushes.Gray &&
                        TxtBx_FirstName.BorderBrush == Brushes.Gray &&
                        TxtBx_LastName.BorderBrush == Brushes.Gray &&
                        TxtBx_phoneNumber.BorderBrush == Brushes.Gray &&
                        TxtBx_bankAccountNumber.BorderBrush == Brushes.Gray &&
                        TxtBx_HostingUnitName.BorderBrush == Brushes.Gray &&
                        TxtBx_code.BorderBrush == Brushes.Gray &&
                        TxtBx_maxGuest.BorderBrush == Brushes.Gray &&
                        TxtBx_priceAdults.BorderBrush == Brushes.Gray &&
                        TxtBx_priceChildren.BorderBrush == Brushes.Gray &&
                        TxtBx_hostkey.BorderBrush == Brushes.Gray) { 
                    // hu.Owner.CollectionClearance = cb_collectionClear;
                    MainWindow.bl.addHostingUnit(hu);
                    MessageBox.Show("ברוכה הבאה לאתר שלנו"+hu.Owner.PrivateName+" "+hu.Owner.FamilyName,"המארח "+hu.HostingUnitKey + " הוסף ", MessageBoxButton.OK, MessageBoxImage.Information);
                    hu = new HostingUnit();
                    hu.Owner = new Host();
                    hu.Uris = new List<Uri>();
                    hu.Diary = new bool[12, 31];
                    hu.Owner.BankBranchDetails = new BankBranch();
                    addHostingUnitGrid.DataContext = hu;
}
                    else MessageBox.Show("אנא מלא את כל השדות כראוי!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "ERROR", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            else
            {
                MessageBox.Show("אנא מלא את כל השדות!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Warning);

            }
        }

        //selection changed
        private void cb_subareaHost_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectcbsubarea = true;
            hu.SubArea = (areaHosting)cb_subareaHost.SelectedIndex;
        }
       //image button
        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            show_code_box.Text = password_box.Password;
        }

        private void cb_bank_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            selectcbbank = true;
            //hu.SubArea = (areaHosting)cb_subareaHost.SelectedIndex;
           hu.Owner.BankBranchDetails= (BankBranch)cb_bank.SelectedItem;
        }

        
    }
}
