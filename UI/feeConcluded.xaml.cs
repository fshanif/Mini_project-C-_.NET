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

namespace UI
{
    /// <summary>
    /// Interaction logic for feeConcluded.xaml
    /// </summary>
    public partial class feeConcluded : Window
    {
        //constructor
        public feeConcluded()
        {
            InitializeComponent();
           
            DataContext = MainWindow.bl.GetHostingUnitsList();
            txt_showfee.Text = Convert.ToString(MainWindow.bl.feeForAllDays());
        }
        // button
        private void button_feeclose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        // button
        private void button_back_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

            Window tmp = new manegerwind();
            tmp.ShowDialog();
        }
    }
}
