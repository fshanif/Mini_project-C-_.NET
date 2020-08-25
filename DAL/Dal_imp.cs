using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS;
using BE;


namespace DAL
{
    /// <summary>
    /// this class implement the function fron IDAL
    /// </summary>
    public class Dal_imp : IDAL
    {
        public void updateDiary(HostingUnit h) {
        
        
        }


        #region GuestRequest


        public void addGuestRequest(GuestRequest r)
        {

            if (DataSource.guestrequestlist.Exists(item => item.GuestRequestKey == r.GuestRequestKey) == true)
                throw new Exception("this guest request is allready  exist");

            else
            {
                r.Status = grStatus.active;
                r.GuestRequestKey = Configuration.GuestRequestKeyCode++;
               // DataSource.guestRequestKeys.Add(r.GuestRequestKey);

                DataSource.guestrequestlist.Add(r.clone());
            }
        }
       //***********************************************
    
        public bool updateGuestRequestStatus(GuestRequest r,grStatus s)
        {       
            bool ris = false;

            if (DataSource.guestrequestlist.Exists(item => item.GuestRequestKey == r.GuestRequestKey) == false)
            {

                r.Status = s;
                DataSource.guestrequestlist.Add(r.clone());
                throw new Exception("dal:updateGuestRequestStatus:this guestrequest doesnt exist");
            }
            else
            {
                DataSource.guestrequestlist.ForEach(item =>
                {
                    if (item.GuestRequestKey == r.GuestRequestKey)
                    {
                        item.Status = s;//
                        ris = true;
                    }
                });
                //DataSource.guestrequestlist.Remove(r);
                //r.Status = s;
                //addGuestRequest(r);
                //ris = true;
            }
            if (ris == true)
                return true;
            else
                return false;
        }
       
        #endregion

        #region HostingUnit
    
        public void addHostingUnit(HostingUnit h)
        {
            if (DataSource.hostingunitlist.Exists(item => item.HostingUnitKey == h.HostingUnitKey) == true)
            {
                throw new Exception("ERROR: this hosting unit is allready exists");

            }
            else
            {
                for (int i = 0; i < 12; i++)
                {
                    for (int j = 0; j < 31; j++)
                    {
                        h.Diary[i, j] = false;
                    }
                }
                //  h.Uris = new List<Uri>();
                h.feeAll = 0;
                h.numawful1 = 0;
                h.numbad2 = 0;
                h.numfine3 = 0;
                h.numgood4 = 0;
                h.numperfect5 = 0;
                h.medsatisfication = 0;
                h.HostingUnitKey = Configuration.HostingUnitKeyCode++;
               // DataSource.hostingunitkeys.Add(h.HostingUnitKey);
                DataSource.hostingunitlist.Add(h.clone());
            }
        }

        //***********************************************

        public void deleteHostingUnit(HostingUnit h)
        {
            if (DataSource.hostingunitlist.Exists(item => item.HostingUnitKey == h.HostingUnitKey) == true) {
                DataSource.hostingunitlist.Remove(DataSource.hostingunitlist.Find(item => item.HostingUnitKey == h.HostingUnitKey));
              //  DataSource.hostingunitkeys.Remove(h.HostingUnitKey);

            }
            else {
                throw new Exception("ERROR: this host unit isnt exists");
            }
        }
        //***********************************************

        public bool updateHostingUnit(HostingUnit h)
        {
            HostingUnit helper = DataSource.hostingunitlist.Find(item => item.HostingUnitKey == h.HostingUnitKey);
            deleteHostingUnit(helper);
            //addHostingUnit(h.clone());
          //  DataSource.hostingunitkeys.Add(h.HostingUnitKey);
            DataSource.hostingunitlist.Add(h.clone());
            return true;
           
        }

        //***********************************************

        #endregion

        #region Order

        public void addOrder(Order o)
        {
            if (DataSource.orderlist.Exists(item => item.OrderKey == o.OrderKey) == true)
            {

                //DataSource.orderlist.Remove(DataSource.orderlist.Find(item => item.OrderKey == o.OrderKey));
                //DataSource.orderlist.Add(o);
                throw new Exception("dal:addOrder:this order is allready exist");
            }
            else
            {
                                                  //##################################
                o.OrderKey = Configuration.OrderKeyCode++;
                DataSource.orderlist.Add(o.clone());
            }
        }
        //***********************************************
        public void updateOrderStatus(Order o, orderStatus so)
        {
            if (DataSource.orderlist.Exists(item => item.OrderKey == o.OrderKey))
            {
                DataSource.orderlist.Find(item => item.OrderKey == o.OrderKey).Status = so;
                if(so==orderStatus.emailSent)
                { DataSource.orderlist.Find(item => item.OrderKey == o.OrderKey).OrderDate = o.OrderDate; }
                //DataSource.orderlist.Remove(DataSource.orderlist.Find(item => item.OrderKey == o.OrderKey));
                //DataSource.orderlist.Add(o.clone());
            }
            else
            {
                o.Status = so;
                addOrder(o.clone());
                throw new Exception("ERROR: the order was not exists");
            }
        }

        #endregion

        #region list 
        //public List<int> Gethostingunitkeys()
        //{
        //    List<int> list = new List<int>();
        //    foreach (var item in DataSource.hostingunitkeys)
        //    {
        //        list.Add(item);
        //    }
        //    return list;
        //}
        //public List<int> GetguestRequestkeys()
        //{
        //    List<int> list = new List<int>();
        //    foreach (var item in DataSource.guestRequestKeys)
        //    {
        //        list.Add(item);
        //    }
        //    return list;
        //}
        public List<GuestRequest> GetGuestRequestList()
        {
            List<GuestRequest> list = new List<GuestRequest>();
            foreach (var item in DataSource.guestrequestlist)
            {
                list.Add(item.clone());
            }
            return list;
            
        }
        //***********************************************

        public List<HostingUnit> GetHostingUnitsList()
        {
            List<HostingUnit> list = new List<HostingUnit>();
            foreach (var item in DataSource.hostingunitlist)
            {
                list.Add(item.clone());
            }
            return list;
            
        }
        //***********************************************
        public List<Order> GetOrderList()
        {
            List<Order> list = new List<Order>();
            foreach (var item in DataSource.orderlist)
            {
                list.Add(item.clone());
            }
            return list;
        }

        //***********************************************
        public List<BankBranch> GetBankBranchList()
        {
            BankBranch b1 = new BankBranch();
            b1.BankName= "diskont";
            b1.BankNumber= 19;
            b1.BranchAddress= "yafo 220";
            b1.BranchCity= "jerusalem";
            b1.BranchNumber= 45;
            BankBranch b2= new BankBranch();
            b2.BankName= "leumy";
            b2.BankNumber= 25;
            b2.BranchAddress= "daid 22";
            b2.BranchCity = "ramat gan";
            b2.BranchNumber = 18;
            BankBranch b3 = new BankBranch();
            b3.BankName = "habinleumy";
            b3.BankNumber = 87;
            b3.BranchAddress = "meir 20";
            b3.BranchCity = "jerusalem";
            b3.BranchNumber = 56;
            BankBranch b4 = new BankBranch();
            b4.BankName = "diskont";
            b4.BankNumber = 9;
            b4.BranchAddress = "marom nave 220";
            b4.BranchCity = "ramat gan";
            b4.BranchNumber = 31;
            BankBranch b5 = new BankBranch();
            b5.BankName = "yahav";
            b5.BankNumber = 16;
            b5.BranchAddress = "grozman 9";
            b5.BranchCity = "yafo";
            b5.BranchNumber = 41;
            List<BankBranch> BankBranchList = new List<BankBranch>() { b1, b2, b3, b4, b5 };
            return BankBranchList;
        }
        #endregion

        #region helpers
        
        public void updateOrder(Order o)
        {
            if (DataSource.orderlist.Exists(item => item.OrderKey == o.OrderKey)==false)
            {
                addOrder(o.clone());            
                throw new Exception("ERROR: the order was not exists");

            }
            else
            {
                Order helper = DataSource.orderlist.Find(item => item.OrderKey == o.OrderKey);
                DataSource.orderlist.Remove(helper);
                    addOrder(o.clone());
            }
        }
       
        
        
       
        
      

        #endregion
    }
}
