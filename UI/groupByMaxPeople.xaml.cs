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
    /// Interaction logic for groupByMaxPeople.xaml
    /// </summary>
    public partial class groupByMaxPeople : Window
    {
        HostingUnit HostingUnit1 = new HostingUnit();
        //constructor
        public groupByMaxPeople(HostingUnit hu)
        {
            InitializeComponent();
            HostingUnit1 = hu;
            var list = MainWindow.bl.groupingGRbymaxOfPeople(HostingUnit1.maxGuest);
            List<GuestRequest> grlist = new List<GuestRequest>();
            foreach (var item in list)
            {
                foreach (var gs in item)
                {
                    grlist.Add(gs);
                }
            }
            foreach (var item in grlist)
            {
                hugroupingbyarea.Children.Add(new grnumpeopleUserControl(item));
            }

        }
        //button
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            int y = HostingUnit1.Owner.HostKey;
            this.Close();
            Window tmp = new inpersonalArea(y);
            tmp.ShowDialog();
        }
        private void Image_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
