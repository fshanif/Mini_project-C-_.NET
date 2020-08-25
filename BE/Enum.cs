using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    //public enum requestStatus { open,closedBySite,closedOccordingToExpireDate}//status of guest request
    public enum options {doesnt_mind, nessary,possible,notIntrested}//type of interest in extras in the hosting units
    public enum orderStatus { emailSent, NotYetAddressed,closedNoResponse,closedResponse }//status of order
    public enum areaHosting {all ,north,south,center}//the area of hosting unit
    public enum subareaHosting { doesnt_mind, ramatgan, tel_aviv, tzfat, jerusalem, haifa }//the area of hosting unit

    public enum typeHosting { doesnt_mind,hotel, tzimer}//type of hosting unit
    public enum grStatus {active,closedBySite,closedIsExpired }//status of guest request
    public enum satisfication { awful ,bad,fine,good,perfect }//the satisfication for a HostingUnit
    public enum typeUser {hostUser,managerUser,clientUser}
}
