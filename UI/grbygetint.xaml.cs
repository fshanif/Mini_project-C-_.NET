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
    /// Interaction logic for grbygetint.xaml
    /// </summary>
    public partial class grbygetint : Window
    {
        //constructor
        public grbygetint()
        {
            InitializeComponent();
        }
        //button
        private void button_close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            Window tmp = new groupingListsWindow();
            tmp.ShowDialog();


        }
        //lost focus
        private void TxtBx_answer_LostFocus(object sender, RoutedEventArgs e)
        {
            var list = MainWindow.bl.groupingGRbymaxOfPeople(Convert.ToInt32(TxtBx_answer.Text));
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
            TxtBx_answer.IsEnabled = false;
        }
      
    }
   
}
