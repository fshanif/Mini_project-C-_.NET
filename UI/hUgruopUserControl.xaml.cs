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
    /// Interaction logic for hUgruopUserControl.xaml
    /// </summary>
    public partial class hUgruopUserControl : UserControl
    {
        //helper
        HostingUnit Current = new HostingUnit();
        //constructor
        public hUgruopUserControl(HostingUnit hu)
        {
            InitializeComponent();
            Current = hu;
            ucguest.DataContext = Current;
            groupBox.Header = Current.SubArea;

        }
    }
}