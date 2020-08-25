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
    /// Interaction logic for hostUserControl.xaml
    /// </summary>
    public partial class hostUserControl : UserControl
    {
        public Host h = new Host();
        //constructor
        public hostUserControl(Host host_)
        {
            InitializeComponent();
            h = host_;
            TxtBx_numhu.Text =Convert.ToString( MainWindow.bl.helpnumberHUforThisHost(h));
            uchost.DataContext = h;
        }
    }
}
