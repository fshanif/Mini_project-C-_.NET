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
using BL;
//shoshana fluk 211471800
//elina hen chulpaev 322622317
namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static IBL bl = BL.BLFactory.getBL();


        //constructor
        public MainWindow()
        {

            InitializeComponent();
         BE.GuestRequest gr = new GuestRequest();
            #region helper
            ////helpers
            //GuestRequest gr1 = new GuestRequest
            //{
            //    ID = "666666666",
            //    PrivateName = "elina",
            //    FamilyName = "chulpaev",
            //    MailAddress = "elinach322@gmail.com",
            //    Status = grStatus.active,
            //    RegistrationDate = new DateTime(2020, 02, 15),
            //    EntryDate = new DateTime(2020, 08, 15),
            //    ReleaseDate = new DateTime(2020, 08, 20),
            //    Area = areaHosting.all,
            //    SubArea = subareaHosting.jerusalem,
            //    Type = typeHosting.hotel,
            //    Adults = 2,
            //    Children = 1,
            //    baby = 1,
            //    Pool = options.nessary,
            //    Jacuzzi = options.notIntrested,
            //    Garden = options.possible,
            //    ChildrensAttractions = options.possible
            //};//#####
            //GuestRequest gr2 = new GuestRequest
            //{
            //    ID = "555555555",
            //    PrivateName = "yael",
            //    FamilyName = "katz",
            //    MailAddress = "yaelk2@gmail.com",
            //    Status = grStatus.active,
            //    RegistrationDate = new DateTime(2020, 01, 30),
            //    EntryDate = new DateTime(2020, 07, 25),
            //    ReleaseDate = new DateTime(2020, 08, 01),
            //    Area = areaHosting.north,
            //    SubArea = subareaHosting.tel_aviv,
            //    Type = typeHosting.tzimer,
            //    Adults = 2,
            //    Children = 2,
            //    baby = 0,
            //    Pool = options.possible,
            //    Jacuzzi = options.possible,
            //    Garden = options.possible,
            //    ChildrensAttractions = options.possible
            //};//#####
            //GuestRequest gr3 = new GuestRequest
            //{
            //    ID = "333333333",
            //    PrivateName = "michal",
            //    FamilyName = "shine",
            //    MailAddress = "michalsh3@gmail.com",
            //    Status = grStatus.active,
            //    RegistrationDate = new DateTime(2020, 03, 01),
            //    EntryDate = new DateTime(2020, 09, 25),
            //    ReleaseDate = new DateTime(2020, 10, 15),
            //    Area = areaHosting.center,
            //    SubArea = subareaHosting.tel_aviv,
            //    Type = typeHosting.hotel,
            //    Adults = 4,
            //    Children = 2,
            //    baby = 0,
            //    Pool = options.possible,
            //    Jacuzzi = options.possible,
            //    Garden = options.possible,
            //    ChildrensAttractions = options.possible,
            //};//#####
            //GuestRequest gr4 = new GuestRequest
            //{
            //    ID = "444444444",
            //    PrivateName = "racli",
            //    FamilyName = "paz",
            //    MailAddress = "pazpaz@gmail.com",
            //    Status = grStatus.active,
            //    RegistrationDate = new DateTime(2020, 03, 11),
            //    EntryDate = new DateTime(2020, 09, 29),
            //    ReleaseDate = new DateTime(2020, 10, 15),
            //    Area = areaHosting.all,
            //    SubArea = subareaHosting.tel_aviv,
            //    Type = typeHosting.hotel,
            //    Adults = 4,
            //    Children = 2,
            //    baby = 3,
            //    Pool = options.notIntrested,
            //    Jacuzzi = options.nessary,
            //    Garden = options.possible,
            //    ChildrensAttractions = options.possible,
            //};//#####


            //MainWindow.bl.addGuestRequest(gr1);//#####
            //MainWindow.bl.addGuestRequest(gr2);//#####
            //MainWindow.bl.addGuestRequest(gr3);//#####
            //MainWindow.bl.addGuestRequest(gr4);//#####

            //HostingUnit hu1 = new HostingUnit
            //{
            //    feeAll = 0,
            //    medsatisfication = 0,
            //    numawful1 = 0,
            //    numbad2 = 0,
            //    numfine3 = 0,
            //    numgood4 = 0,
            //    numperfect5 = 0,

            //    Uris = new List<Uri>()
            //                {new Uri("https://merkaz-harahit.co.il/wp-content/uploads/2018/10/%D7%97%D7%93%D7%A8-%D7%A9%D7%99%D7%A0%D7%94-%D7%A4%D7%99%D7%96%D7%94.jpg"),
            //                 new Uri("https://touch.maariv.co.il/wp-content/uploads/kitchen-living-room-4043091_1920-802x386.jpg"),
            //                 new Uri("https://www.zometrahitim.co.il/wp-content/uploads/2018/01/%D7%97%D7%93%D7%A8-%D7%A6%D7%91%D7%A8-%D7%9B%D7%95%D7%9C%D7%9C-%D7%90%D7%A8%D7%95%D7%9F-%D7%90%D7%AA%D7%A8-1.jpg"),
            //                },
            //    Diary = new bool[12, 31],
            //    HostingUnitName = "live life",
            //    maxGuest = 15,
            //    priceAdults = 90,
            //    priceChildren = 75,
            //    SubArea = areaHosting.north,
            //    Owner = new Host
            //    {
            //        HostKey = 111111111,
            //        PrivateName = "shani",
            //        FamilyName = "fluk",
            //        FhoneNumber = "0548466550",
            //        MailAddress = "shanifluk@gmail.com",
            //        code = "1111",
            //        BankBranchDetails = new BankBranch
            //        {
            //            BankName = "leumi",
            //            BankNumber = 19,
            //            BranchNumber = 18,
            //            BranchAddress = "yafo 54 ",
            //            BranchCity = "yerushalaim"
            //        },
            //        BankAccountNumber = 188751,
            //        CollectionClearance = true

            //    }
            //};//#####
            //HostingUnit hu2 = new HostingUnit
            //{
            //    feeAll = 0,
            //    medsatisfication = 0,
            //    numawful1 = 0,
            //    numbad2 = 0,
            //    numfine3 = 0,
            //    numgood4 = 0,
            //    numperfect5 = 0,

            //    Uris = new List<Uri>()
            //                     {new Uri("https://www.vila4me.co.il/images/gallery/vila_169_60781_FQyfrj3.jpg"),
            //                     new Uri("https://www.sharondesign.co.il/wp-content/uploads/2018/02/%D7%A2%D7%99%D7%A6%D7%95%D7%91-%D7%A4%D7%A0%D7%99%D7%9D-%D7%93%D7%99%D7%A8%D7%AA-%D7%A7%D7%91%D7%9C%D7%9F.jpg"),
            //                     new Uri("https://t-ec.bstatic.com/images/hotel/max1024x768/188/188483899.jpg")
            //                     },
            //    Diary = new bool[12, 31],
            //    HostingUnitName = "BLUE EYES",
            //    maxGuest = 5,
            //    priceAdults = 85.99,
            //    priceChildren = 60,
            //    SubArea = areaHosting.center,
            //    Owner = new Host
            //    {
            //        HostKey = 333333333,
            //        PrivateName = "Shania",
            //        FamilyName = "Fluka",
            //        FhoneNumber = "0548589563",
            //        MailAddress = "shani587@gmail.com",
            //        code = "2222",
            //        BankBranchDetails = new BankBranch
            //        {
            //            BankName = "yahav",
            //            BankNumber = 45,
            //            BranchNumber = 9,
            //            BranchAddress = "yerushlaim 59 ",
            //            BranchCity = "yafo",
            //        },
            //        BankAccountNumber = 852169,
            //        CollectionClearance = true
            //    }
            //};//#####

            //MainWindow.bl.addHostingUnit(hu1);//#####
            //MainWindow.bl.addHostingUnit(hu2);//#####
            #endregion

        }
        //button
        private void ButtonSTART_Click(object sender, RoutedEventArgs e)
        {
            Window tmp = new openWindow();
            tmp.ShowDialog();
           MainWindow.bl.closethreadbyMainWindow();

            this.Close();
        }

        private void closebutten_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

