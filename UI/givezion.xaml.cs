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
    /// Interaction logic for givezion.xaml
    /// </summary>
    public partial class givezion : Window
    {
        HostingUnit hu = new HostingUnit();
        //constructor
        public givezion(int hukey)
        {
            InitializeComponent();
            hu = MainWindow.bl.getHostingUnitByKey(hukey);
            
        }
        //image button
        private void zion1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hu.numawful1++;
            MainWindow.bl.medgrade(hu);
            MessageBox.Show("תודה רבה על המשוב", "תודה(:", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
            Window tmp = new clientWindow();
            tmp.ShowDialog();
        }
        //image button
        private void zion2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hu.numbad2++;
            MainWindow.bl.medgrade(hu);
            MessageBox.Show("תודה רבה על המשוב", "תודה(:", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
            Window tmp = new clientWindow();
            tmp.ShowDialog();
        }
        //image button
        private void zion3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hu.numfine3++;
            MainWindow.bl.medgrade(hu);
            MessageBox.Show("תודה רבה על המשוב", "תודה(:", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
            Window tmp = new clientWindow();
            tmp.ShowDialog();
        }
        //image button
        private void zion4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hu.numgood4++;
            MainWindow.bl.medgrade(hu);
            MessageBox.Show("תודה רבה על המשוב", "תודה(:", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
            Window tmp = new clientWindow();
            tmp.ShowDialog();
        }
        //image button
        private void zion5_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            hu.numperfect5++;
            MainWindow.bl.medgrade(hu);
            MessageBox.Show("תודה רבה על המשוב", "תודה(:", MessageBoxButton.OK, MessageBoxImage.Information);

            this.Close();
            Window tmp = new clientWindow();
            tmp.ShowDialog();
        }
    }
}
