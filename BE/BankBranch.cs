using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class BankBranch
    {
        public int BankNumber { get; set; }//number of bank
        public string BankName { get; set; }//name of bank
        public int BranchNumber { get; set; }//number of branch
        public string BranchAddress { get; set; }//address of branch
        public string BranchCity { get; set; }//city of branch
        public override string ToString()//tostring 
        {
            return "BankNumber: " + BankNumber + "\nBankName: " + BankName + "\nBranchNumber: " + BranchNumber + "\nBranchAddress: " + BranchAddress + "\nBranchCity: " + BranchCity;
        }

        public int CompareTo(BankBranch bankBranch)
        {
            throw new NotImplementedException();
        }
    }
}
