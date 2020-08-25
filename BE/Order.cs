using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Order
    {    
        public double totalPrice { get; set; }//the price for one day 

        //public double feeOrder { get; set; }//fee for order for all days
        public int HostingUnitKey { get; set; }//the key of hosting unit
        public int GuestRequestKey { get; set; }//the key of guest request
        public int OrderKey { get; set; }//the key of order
        public orderStatus Status { get; set; }//status of order
        public DateTime CreateDate { get; set; }//date of creation of order
        public DateTime OrderDate { get; set; }//date that an email was sent to the client

        public override string ToString()//tostring
        {//+ "\nfeeOrder:"+ feeOrde
            return "HostingUnitKey: " + HostingUnitKey + "\nGuestRequestKey: " + GuestRequestKey + "\nOrderKey: " + OrderKey + "\nStatus: " + Status+ "\nCreateDate: " + CreateDate+ "\nOrderDate: " + OrderDate+ "\ntotalPrice:" + totalPrice ;
        }                                                                                                                                                                                         
        
    }
}
