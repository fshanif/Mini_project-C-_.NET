using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
   public class Configuration
    {
        public static double fee = 10;//fee
        public static int ExpireRequest = 20;//time for request till it gets expired
        public static int HostingUnitKeyCode = 10000000;//static variable that contains the hosting unit code
        public static int GuestRequestKeyCode = 10000000;//static variable that contains the guest request code
        public static int OrderKeyCode = 10000000;//static variable that contains the order code

    }
}
