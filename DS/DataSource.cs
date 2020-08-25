using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DS
{
    public class DataSource
    {
       // public static List<HostingUnit> hostingunitlist = new List<HostingUnit> {
       // new HostingUnit{
       //     HostingUnitKey=10000008,
       //     Diary=new bool[31,12],
       //     HostingUnitName="live life",
       //     maxGuest=7,
       //     priceAdults=90,
       //     priceChildren=75,
       //     SubArea=areaHosting.all,
       //     Owner=new Host{
       //               HostKey = 11111111,
       //               PrivateName = "Elina",
       //               FamilyName = "tzofiya",
       //               FhoneNumber = "0548466550",
       //               MailAddress = "elina154@gmail.com",
       //               BankBranchDetails = new BankBranch{ BankName = "leumi",
       //                                                   BankNumber = 19,
       //                                                   BranchNumber = 18,
       //                                                   BranchAddress = "yafo 54 ",
       //                                                   BranchCity = "yerushalaim"},
       //               BankAccountNumber = 188751
       //               }
       //},
       // new HostingUnit{
       //     HostingUnitKey=10000009,
       //     Diary=new bool[31,12],
       //     HostingUnitName="BLUE EYES",
       //     maxGuest=5,
       //     priceAdults=85.99,
       //     priceChildren=60,
       //     SubArea=areaHosting.center,
       //     Owner=new Host{
       //                     CollectionClearance=false,
       //                     HostKey = 33333333,
       //                     PrivateName = "Shani",
       //                     FamilyName = "Fluk",
       //                     FhoneNumber = "0548589563",
       //                     MailAddress = "shani587@gmail.com",
       //                     BankBranchDetails = new BankBranch
       //                                          {
       //                                          BankName = "yahav",
       //                                          BankNumber = 45,
       //                                          BranchNumber = 9,
       //                                          BranchAddress = "yerushlaim 59 ",
       //                                          BranchCity = "yafo",
       //                                        },
       //      BankAccountNumber = 852169,


       //     }
       // },
       // new HostingUnit{
       //     HostingUnitKey=10000010,
       //     Diary=new bool[31,12],
       //     HostingUnitName="Luxury Suite",
       //     maxGuest=10,
       //     priceAdults=120,
       //     priceChildren=50,
       //     SubArea=areaHosting.south,
       //     Owner=new Host{
       //               CollectionClearance=true,
       //               HostKey = 222222222,
       //               PrivateName = "Shani",
       //               FamilyName = "Fluk",
       //               FhoneNumber = "0548589563",
       //               MailAddress = "shani587@gmail.com",
       //               BankBranchDetails = new BankBranch{
       //                                        BankName = "yahav",
       //                                        BankNumber = 45,
       //                                        BranchNumber = 9,
       //                                        BranchAddress = "yerushlaim 59 ",
       //                                        BranchCity = "yafo" },
       //     BankAccountNumber = 852169


       //     } }
       // };
       // public static List<GuestRequest> guestrequestlist = new List<GuestRequest> {
       // new GuestRequest{GuestRequestKey=1000008,
       //     PrivateName = "daniel",
       //     FamilyName = "cohen",
       //     MailAddress = "danielco1@gmail.com",
       //     Status = grStatus.active,
       //     RegistrationDate = new DateTime(2020, 02, 15),
       //     EntryDate = new DateTime(2020, 08, 15),
       //     ReleaseDate = new DateTime(2020, 08, 20),
       //     Area = areaHosting.all,
       //     SubArea = subareaHosting.haifa,
       //     Type = typeHosting.hotel,
       //     Adults = 2,
       //     Children = 1,
       //     baby = 1,
       //     Pool = options.nessary,
       //     Jacuzzi = options.notIntrested,
       //     Garden = options.possible,
       //     ChildrensAttractions = options.possible
       // },
       // new GuestRequest{
       //     GuestRequestKey=1000009,
       //     PrivateName = "yael",
       //     FamilyName = "katz",
       //     MailAddress = "yaelk2@gmail.com",
       //     Status = grStatus.active,
       //     RegistrationDate = new DateTime(2020, 01, 30),
       //     EntryDate = new DateTime(2020, 07, 25),
       //     ReleaseDate = new DateTime(2020, 08, 01),
       //     Area = areaHosting.north,
       //     SubArea = subareaHosting.jerusalem,
       //     Type = typeHosting.tzimer,
       //     Adults = 2,
       //     Children = 2,
       //     baby = 0,
       //     Pool = options.possible,
       //     Jacuzzi = options.possible,
       //     Garden = options.possible,
       //     ChildrensAttractions = options.possible
       // },
       // new GuestRequest{
       //     GuestRequestKey=1000010,
       // PrivateName = "michal",
       // FamilyName = "shine",
       // MailAddress = "michalsh3@gmail.com",
       // Status = grStatus.active,
       // RegistrationDate = new DateTime(2020, 03, 01),
       // EntryDate = new DateTime(2020, 09, 25),
       // ReleaseDate = new DateTime(2020, 10, 15),
       // Area = areaHosting.center,
       // SubArea = subareaHosting.tel_aviv,
       // Type = typeHosting.hotel,
       // Adults = 4,
       // Children = 2,
       // baby = 0,
       // Pool = options.possible,
       // Jacuzzi = options.possible,
       // Garden = options.possible,
       // ChildrensAttractions = options.possible,
       // },
       // new GuestRequest{
       //     GuestRequestKey=100001,
       // PrivateName = "david",
       // FamilyName = "zilber",
       // MailAddress = "david15@gmail.com",
       // Status = grStatus.active,
       // RegistrationDate = new DateTime(2020, 08, 01),
       // EntryDate = new DateTime(2020, 10, 25),
       // ReleaseDate = new DateTime(2020, 11, 01),
       // Area = areaHosting.south,
       // SubArea = subareaHosting.jerusalem,
       // Type = typeHosting.tzimer,
       // Adults = 2,
       // Children = 0,
       // baby = 0,
       // Pool = options.nessary,
       // Jacuzzi = options.nessary,
       // Garden = options.possible,
       // ChildrensAttractions = options.notIntrested,
       // }
       // };
       public static List<Order> orderlist=new List<Order>();//list of order
       public static List<HostingUnit> hostingunitlist = new List<HostingUnit>();//list of hosting unit
       public static List<GuestRequest> guestrequestlist = new List<GuestRequest>();//list of guest request
      //public static List<int> hostingunitkeys = new List<int>();
       //public static List<int> guestRequestKeys = new List<int>();

    }
}
