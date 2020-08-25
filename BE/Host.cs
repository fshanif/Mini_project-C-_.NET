using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Host
    {
        public string code { get; set; }
        public int HostKey { get; set; }//the key of the host
        public string PrivateName { get; set; }//the private name of the host
        public string FamilyName { get; set; }//the last name of the host
        public string FhoneNumber { get; set; }//the phone number of the host
        public string MailAddress { get; set; }//the mail address of the host
        public BankBranch BankBranchDetails { get; set; }//the bank branch details
        public int BankAccountNumber { get; set; }//bank account number
        public bool CollectionClearance { get; set; }//collection clearance

        public override string ToString()//tostring
        {
            return "HostKey: " + HostKey + "\nPrivateName: " + PrivateName + "\nFamilyName: " + FamilyName + "\nFhoneNumber: " + FhoneNumber + "\nMailAddress: " + MailAddress + "\nBankBranchDetails: " + BankBranchDetails + "\nBankAccountNumber: " + BankAccountNumber + "\nCollectionClearance: " + CollectionClearance;
        }
        
    }
}
