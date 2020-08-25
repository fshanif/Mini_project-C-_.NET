using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class GuestRequest
    {
        public string ID { get; set; }
        public int GuestRequestKey { get; set; }//the key of guest request
        public string PrivateName { get; set; }//the private name of the guest
        public string FamilyName { get; set; }//the family name of the guest
        public string MailAddress { get; set; }//the mail address of the guest
        public grStatus Status { get; set; }//status of request
        public areaHosting Area { get; set; }//wanted area
        public subareaHosting SubArea { get; set; }//wanted sub area
        public typeHosting Type { get; set; }//type of wanted hosting unit
        public int Adults { get; set; }//number of adult
        public int Children { get; set; }//number of children
        public int baby { get; set; }//number of babies
        public options Pool { get; set; }//if the pool is nesecery
        public DateTime RegistrationDate { get; set; }//Registration date
        public DateTime EntryDate { get; set; }//entry date
        public DateTime ReleaseDate { get; set; }//release date
        public options Jacuzzi { get; set; }//if the jacuzzi is nesecery
        public options Garden { get; set; }//if the garden is nesecery
        public options ChildrensAttractions { get; set; }//if the childern atraction is nesecery
        public override string ToString()
        {
            return "GuestRequestKey: " + GuestRequestKey + "\nPrivateName: " + PrivateName + "\nFamilyName: " + FamilyName +"\nID"+ID+"\nMailAddress: " + MailAddress + "\nStatus: " + Status + "\nRegistrationDate: " + RegistrationDate + "\nEntryDate: " + EntryDate + "\nReleaseDate: " + ReleaseDate + "\nArea: " + Area + "\nSubArea: " + SubArea + "\nType: " + Type + "\nAdults: " + Adults + "\nChildren: " + Children + "\nPool: " + Pool + "\nJacuzzi: " + Jacuzzi + "\nGarden: " + Garden + "\nChildrensAttractions: " + ChildrensAttractions;
        }

    }
}
