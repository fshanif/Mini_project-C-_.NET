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
    /// Interaction logic for GuestRequestUserControl.xaml
    /// </summary>
    public partial class GuestRequestUserControl : UserControl
    {
        public GuestRequest Current = new GuestRequest();
        //constructor
        public GuestRequestUserControl(GuestRequest guestRequest)
        {
            InitializeComponent();
            Current = guestRequest;
            ucguest.DataContext = Current;
        }
    }
}
