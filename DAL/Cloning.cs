using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
namespace DAL
{
    public static class Cloning
    {
        /// <summary>
        /// function that return the copy of host 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Host clone(this Host source)
        {
            Host helper = new Host();
            helper.HostKey= source.HostKey;
            helper.code = source.code;

            helper.PrivateName= source.PrivateName;
            helper.FamilyName= source.FamilyName;
            helper.FhoneNumber= source.FhoneNumber;
            helper.MailAddress= source.MailAddress;
            helper.BankAccountNumber= source.BankAccountNumber;
            helper.BankBranchDetails= source.BankBranchDetails.clone();
            helper.CollectionClearance= source.CollectionClearance;
            return helper;
        }
        /// <summary>
        /// function that return the copy of order
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static Order clone(this Order source)
        {
            Order helper = new Order();
            helper.totalPrice = source.totalPrice;
       //   helper.feeOrder = source.feeOrder;
            helper.HostingUnitKey = source.HostingUnitKey;
            helper.GuestRequestKey = source.GuestRequestKey;
            helper.OrderKey = source.OrderKey;
            helper.Status = source.Status;
            helper.CreateDate = source.CreateDate;
            helper.OrderDate = source.OrderDate;
            return helper;
        }
        /// <summary>
        /// function that return the copy of HostingUnit
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static HostingUnit clone(this HostingUnit source)
        {

            HostingUnit helper = new HostingUnit();
            helper.medsatisfication = source.medsatisfication;
            helper.numawful1 = source.numawful1;
            helper.numbad2 = source.numbad2;
            helper.numfine3 = source.numfine3;
            helper.numgood4 = source.numgood4;
            helper.numperfect5 = source.numperfect5;
            helper.feeAll = source.feeAll;
            helper.HostingUnitKey = source.HostingUnitKey;
            helper.Owner = source.Owner.clone();
            helper.HostingUnitName = source.HostingUnitName;
            helper.Diary = source.Diary;
            helper.SubArea = source.SubArea;
            helper.maxGuest = source.maxGuest;
            helper.priceAdults = source.priceAdults;
            helper.priceChildren = source.priceChildren;
            helper.Uris = source.Uris;
                return helper;
        }
        /// <summary>
        /// function that return the copy of BankBranch
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static BankBranch clone(this BankBranch source)
        {
            BankBranch helper = new BankBranch();
            helper.BankNumber = source.BankNumber;
            helper.BankName = source.BankName;
            helper.BranchNumber = source.BranchNumber;
            helper.BranchAddress = source.BranchAddress;
            helper.BranchCity = source.BranchCity;
            return helper;
        }
        /// <summary>
        /// function that return the copy of GuestRequest
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static GuestRequest clone(this GuestRequest source)
        {

            GuestRequest helper = new GuestRequest();
            helper.ID = source.ID;
            helper.GuestRequestKey = source.GuestRequestKey;
            helper.PrivateName = source.PrivateName;
            helper.FamilyName = source.FamilyName;
            helper.Status = source.Status;
            helper.MailAddress = source.MailAddress;
            helper.EntryDate = source.EntryDate;
            helper.ReleaseDate = source.ReleaseDate;
            helper.RegistrationDate = source.RegistrationDate;
            helper.Area = source.Area;
            helper.SubArea = source.SubArea;
            helper.Type = source.Type;
            helper.Adults = source.Adults;
            helper.Children = source.Children;
            helper.Pool = source.Pool;
            helper.Jacuzzi = source.Jacuzzi;
            helper.Garden = source.Garden;
            helper.ChildrensAttractions = source.ChildrensAttractions;
            helper.baby = source.baby;


            return helper;
        }
    }
}
