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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BE;
namespace UI
{
    /// <summary>
    /// Interaction logic for grnumpeopleUserControl.xaml
    /// </summary>
    public partial class grnumpeopleUserControl : UserControl
    {
        GuestRequest Current = new GuestRequest();
        //constructor
        public grnumpeopleUserControl(GuestRequest guestRequest)
        {
            InitializeComponent();
            Current = guestRequest;
            
            ucguest.DataContext = Current;
            groupBox.Header = Current.baby + Current.Children + Current.Adults;
        }
    }
}
