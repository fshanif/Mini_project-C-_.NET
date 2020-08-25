using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;
using System.Net.Mail;
using System.Threading;

namespace BL
{

    public class BL_imp : IBL
    {
        //delegate:)
        // public delegate bool conditionDelegate(GuestRequest guestRequests);
        //dal->bl:)


        DAL.IDAL dal = DAL.DalFactory.getDal();
        bool stopthread = true;
        Thread updateOrderThread;
       
        #region thread
        public BL_imp()
        {
            updateOrderThread = new Thread(updateOrder);//פעם ביום מעדכן הזמנות רלוונטיות
            updateOrderThread.Start();
        }
        public void closethreadbyMainWindow()
        {
            stopthread = false;
            updateOrderThread.Abort();//we do it since we dont want that the computer to keep working after the program close and go crazy
        }

        public void updateOrder()//
        {
             while (stopthread)
              {
            foreach (Order order in dal.GetOrderList())
            {
                try
                {

                    if ((daysBetween(order.CreateDate) > 31 && order.Status != orderStatus.closedNoResponse && order.Status != orderStatus.closedResponse) || (getGuestRequestByKey(order.GuestRequestKey).EntryDate <= DateTime.Today))//אני בממתינה
                        closeOrderbysite(order);
                }
                catch (Exception) { }
            }

            Thread.Sleep(86400000);//a day
            }  
        }

        public void closeOrderbysite(Order o)
        {
             updateOrderStatus(o, orderStatus.closedNoResponse);
           
        }
        #endregion

        #region GuestRequest

        public void addGuestRequest(GuestRequest r)
        {
            var v = from item in dal.GetGuestRequestList()
                    where item.ID == r.ID && (item.PrivateName != r.PrivateName || item.FamilyName != r.FamilyName)
                    select item;
            foreach (GuestRequest item in v) { }
            if (v != null)
            {
                if (r.EntryDate.Date >= r.ReleaseDate.Date)
                {
                    throw new Exception("entry date must be smaller than the release day at least one day");
                }
                else
                {
                    try
                    {
                        if (r.MailAddress.EndsWith("@gmail.com"))
                        {
                            r.RegistrationDate = DateTime.Now;
                            dal.addGuestRequest(r);
                        }
                        else
                            throw new Exception("bl:addGuestRequest:the mail address is not correct");
                    }
                    catch (Exception x)
                    {
                        throw new Exception(x.Message);
                    }
                }
            }
            else
                throw new Exception("bl:addGuestRequest:the id  is not taken allready");
        }
        //****************************************
        public void updateGuestRequestStatus(GuestRequest r, grStatus s) {
            try { dal.updateGuestRequestStatus(r, s); }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        #endregion

        #region HostingUnit
        //****************************************
        public void addHostingUnit(HostingUnit h) {
            try
            {
                if (h.Owner.MailAddress.EndsWith("@gmail.com"))
                {
                    dal.addHostingUnit(h);
                }
                else
                    throw new Exception("bl:addHostingUnit:the mail address is not correct");
            }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        //****************************************
        public void deleteHostingUnit(HostingUnit h) {
            List<Order> lisrorders = dal.GetOrderList();
            var v = lisrorders.Where(item => item.HostingUnitKey == h.HostingUnitKey);
            foreach (Order item in v)
            {
                if (item.Status == orderStatus.NotYetAddressed)
                {
                    throw new Exception("cant delete hostingunit while the suggest is open");
                }
            }
            try { dal.deleteHostingUnit(h); }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        //****************************************
        public void updateHostingUnit(HostingUnit h) {
            if (h.Owner.CollectionClearance == false)
            {
                List<Order> lst = dal.GetOrderList();
                var v = from n in lst
                        where (n.HostingUnitKey == h.HostingUnitKey)
                        select n;
                List<Order> lst2 = new List<Order>();
                foreach (var item in v)
                {
                    lst2.Add(item);
                }
                foreach (var item in lst2)
                {
                    var v1 = from n in dal.GetGuestRequestList()
                             where (n.GuestRequestKey == item.GuestRequestKey && n.Status == grStatus.active)
                             select n;
                    if (v != null)
                        throw new Exception("There are open requests");
                }

            }
            try { dal.updateHostingUnit(h); }
            catch (Exception x)
            {
                Console.WriteLine(x);
            }
        }
        #endregion

        #region Order
        //****************************************
        public void addOrder(Order o)
        {
            int help_GuestRequestKey = o.GuestRequestKey;
            int help_HostingUnitKey = o.HostingUnitKey;
            HostingUnit h = getHostingUnitByKey(help_HostingUnitKey);
            GuestRequest gr = getGuestRequestByKey(help_GuestRequestKey);
            if (gr.Adults + gr.Children + gr.baby > h.maxGuest)
            {
                throw new Exception("BL:addOrder:there are to many of guests");
            }
            
            double y = priceForAnOrder(o);//for one day
            int x = daysBetween(gr.EntryDate, gr.ReleaseDate);
            o.totalPrice = y * x;
            h.feeAll = h.feeAll + Configuration.fee * x;
            updateHostingUnit(h);
            if (ApproveRequest(gr.EntryDate, x, h) == true)
            { dal.addOrder(o); }
            else
            {
                throw new Exception("BL:addOrder:The hostingunit is unavailable on these dates");
            }
        }

        //****************************************
        public void updateOrderStatus(Order o, orderStatus s)
        {

            HostingUnit hhelp = getHostingUnitByKey(o.HostingUnitKey);
           GuestRequest gr=getGuestRequestByKey(o.GuestRequestKey);
          

            if (o.Status == orderStatus.closedResponse || o.Status == orderStatus.closedNoResponse)
            {
                throw new Exception("BL:updateOrderStatus: The order has been closed");
            }
            //if (daysBetween(o.CreateDate) >= Configuration.ExpireRequest)
            //{
            //    dal.updateOrderStatus(o, orderStatus.closedNoResponse);
            //} thread :)


            if (s == orderStatus.emailSent)
            {
                o.OrderDate = DateTime.Now.Date;
            //  sendAnEmail( o);  //the mail will be sent when we will create the wpf
           
                
                if (dal.GetHostingUnitsList().Find(item => o.HostingUnitKey == item.HostingUnitKey).Owner.CollectionClearance == false)
                {
                    throw new Exception("ERROR:updateOrderStatus: the guest didnt sign for Direct debit authorization");
                }
                else
                {
                    o.Status = s;
                    o.OrderDate = DateTime.Now.Date;
                    dal.updateOrderStatus(o, s);
                    dal.GetOrderList().Find(item1 => o.HostingUnitKey == item1.HostingUnitKey).OrderDate = DateTime.Now;
                    Console.WriteLine("mail sent");
                }
            }
            if (s == orderStatus.closedResponse)//closedNoResponse
            {
                orderclose(o, s);
            }
            if (s == orderStatus.closedNoResponse)//
            {
                dal.updateOrderStatus(o, orderStatus.closedNoResponse);
            }
        }
        #endregion

        #region list
        public List<HostingUnit> GetHostingUnitsList() {
            if (dal.GetHostingUnitsList() == null)
            {
                throw new Exception("bl:GetHostingUnitsList: the list in the data source is empty");
            }
            return dal.GetHostingUnitsList();
        }
        //****************************************
        public List<int> Gethostingunitkeys()
        {
            var v = from item in GetHostingUnitsList()
                    select item.HostingUnitKey;
            foreach (int item in v) { }
            if (v == null)
            {
                throw new Exception("bl:Gethostingunitnames: the list in the data source is empty");
            }
            return v.ToList();
        }
        public List<int> GetguestRequestkeys()
        {      
            var v = from item in GetGuestRequestList()
                    select item.GuestRequestKey;
            foreach(int item in v) { }

            if (v == null)
            {
                throw new Exception("bl:Gethostingunitnames: the list in the data source is empty");
            }
            return v.ToList();
        }

        public List<GuestRequest> GetGuestRequestList() {
            if (dal.GetGuestRequestList() == null)
            {
                throw new Exception("bl:GetHostingUnitsList: the list in the data source is empty");
            }
            return dal.GetGuestRequestList();
        }
        //****************************************
        public List<Order> GetOrderList() {
            if (dal.GetOrderList() == null)
            {
                throw new Exception("bl:GetHostingUnitsList: the list in the data source is empty");
            }
            return dal.GetOrderList();
        }
        //****************************************
        public List<BankBranch> GetBankBranchList() {
            List<BankBranch> a = dal.GetBankBranchList();
            if (a == null)
            {
                throw new Exception("bl:GetHostingUnitsList: the list in the data source is empty");
            }
            return a;
        }
        #endregion

        #region helper
        public List<int> getordersKeyByTz(int _id)
        {
            List<int> help = new List<int>();

            var v = from item in GetOrderList()
                    where getHostingUnitByKey(item.HostingUnitKey).Owner.HostKey  == _id
                    select item.OrderKey;
            foreach (int item in v)
            {
                help.Add(item);
            };
            return help;
        }

        public List<int> gethostingUnitKeyByTz(int _id)
        {
            List<int> help = new List<int>();
            var v = from item in GetHostingUnitsList()
                    where item.Owner.HostKey == _id
                    select item.HostingUnitKey;
            foreach(int item in v) {
                help.Add(item);
                    };
            return help;
        }

        public List<int> listMyRequest(string id)
        {
            try
            {
                List<int> x=new List<int>();
                List<GuestRequest> h = new List<GuestRequest>();
                foreach (GuestRequest item in GetGuestRequestList())
                {
                    if (item.ID == id)
                        h.Add(item);
                }

                foreach (GuestRequest item in h)
                {
                    x.Add(item.GuestRequestKey);
                }
               return x;
                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public Double priceForAnOrder(Order o)
        {
            GuestRequest gr = getGuestRequestByKey(o.GuestRequestKey);
            HostingUnit h = getHostingUnitByKey(o.HostingUnitKey);
            if (gr == null || h == null)
                throw new Exception("bl:priceForAnOrder:someone is null");
            if (gr.Adults + gr.Children > h.maxGuest)
            {
                throw new Exception("BL:priceForAnOrder:there are to many of guests");
            }
            else
            {
                 gr = getGuestRequestByKey(o.GuestRequestKey);
                 h = getHostingUnitByKey(o.HostingUnitKey);
                return gr.Adults * h.priceAdults + gr.Children * h.priceChildren + gr.baby * h.priceChildren;

            }
        }
        public double feeForAllDays()
        {
            List<HostingUnit> list = GetHostingUnitsList();
            if (list == null)
            {
                throw new Exception("there are no hosts so the fee cant be calculate");
            }
           return list.Sum(item => item.feeAll);

        }
        //****************************************
        public void sendAnEmail(Order o)
        {   MailMessage mail = new MailMessage();
            GuestRequest gr = getGuestRequestByKey(o.GuestRequestKey);
            HostingUnit h = getHostingUnitByKey(o.HostingUnitKey);
            string subj = "suggesion for a dream vocation:";
            string body = ("Dear " +  gr.FamilyName + " " + gr.PrivateName + ",<br>" + " We want you to have a great vocation with us,<br>" +
                "The wanted dates are available, <br>" +
                "And we will be more than happy to entertain you:),<br>" +
                "The information about our hosting unit: *" +   h.HostingUnitName + "* is: <br>" + "This hosting unit belong to " +
                h.Owner.FamilyName + " " + h.Owner.PrivateName + " " + h.Owner.MailAddress + "<br> the hotel is located in: " + h.SubArea + " <br>" +
                "The maximum guest that are welcome to our hotel is:  " + h.maxGuest + " <br>" +
             "The price for an adult per night is: " + h.priceAdults + ",<br>" + "and for a child or baby is: " + h.priceChildren + ", <br>" +
             "We will be happy to hear from you back " + "<br>For more information please send an email to: " + h.Owner.MailAddress + "<br>" +"<b>"+ h.Owner.PrivateName + " <b>" + h.Owner.FamilyName);
            
            mail.To.Add(gr.MailAddress);
            mail.From = new MailAddress(h.Owner.MailAddress);
            mail.Subject =subj;
            //mail.Body =body;
            mail.Body = " <p style='font-size:30px' dir=LTR>" + body;//<b>
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("vacationwithe.s@gmail.com", "elinashani");
            smtp.EnableSsl = true;
            try
            {
               // שליחת ההודעה 
              smtp.Send(mail);
            }
            catch (Exception ex)
            {
                // טיפול בשגיאות ותפיסת חריגות 
                throw ex;
            }

        }
        //****************************************
        public void orderclose(Order o, orderStatus s)
        {
            GuestRequest grhelp = dal.GetGuestRequestList().Find(item => item.GuestRequestKey == o.GuestRequestKey);
            HostingUnit hhelp = getHostingUnitByKey(o.HostingUnitKey);
            
            int x = daysBetween(grhelp.EntryDate, grhelp.ReleaseDate);
            if (ApproveRequest(grhelp.EntryDate, x, hhelp) == true)
            {
                updateGuestRequestStatus(grhelp, grStatus.closedBySite);
                dal.updateOrderStatus(o, s);
                GuestRequest gr = getGuestRequestByKey(o.GuestRequestKey);
                diarymaker(o);
                var v = from item in dal.GetGuestRequestList()
                        where item.ID == gr.ID/////#################
                        select item;
                // where (grhelp.FamilyName == item.FamilyName) && (grhelp.PrivateName == item.PrivateName)
                //where item.GuestRequestKey == o.GuestRequestKey/////#################

                foreach (var item in v)
                {
                    item.Status = grStatus.closedBySite;
                }
                //fee
                int numberDay = daysBetween(gr.EntryDate, gr.ReleaseDate);
                hhelp.feeAll = hhelp.feeAll+Configuration.fee * numberDay;
            }
            else
                throw new Exception("bl:orderclose: cant order a hosting unit while its not available");
        }
        //****************************************
        public HostingUnit getHostingUnitByKey(int _hostingUnitKey)
        {
          
            List<HostingUnit> a = GetHostingUnitsList();
            return a.Find(item => item.HostingUnitKey == _hostingUnitKey);

        }
        //****************************************
        public GuestRequest getGuestRequestByKey(int _GuestRequestKey)
        {
            List<GuestRequest> a = GetGuestRequestList();
            return a.Find(item => item.GuestRequestKey == _GuestRequestKey);

        }
        //****************************************
        public void diarymaker(Order o)
        {
            //function that recieves the entry date and release date and checks if the request can be approved if yes change these dates it the diary to true
            List<GuestRequest> listgr = dal.GetGuestRequestList();
            List<HostingUnit> listhu = dal.GetHostingUnitsList();
            GuestRequest guestReq = getGuestRequestByKey(o.GuestRequestKey);
            HostingUnit hostingU = getHostingUnitByKey(o.HostingUnitKey);
            //GuestRequest guestReq = listgr.Find(item => item.GuestRequestKey == o.GuestRequestKey);
            //HostingUnit hostingU = listhu.Find(item => item.HostingUnitKey == o.HostingUnitKey);

            DateTime in1 = new DateTime(guestReq.EntryDate.Year, guestReq.EntryDate.Month, guestReq.EntryDate.Day);//entry date
            DateTime out1 = new DateTime(guestReq.ReleaseDate.Year, guestReq.ReleaseDate.Month, guestReq.ReleaseDate.Day);//release date
            DateTime helper = new DateTime(guestReq.EntryDate.Year, guestReq.EntryDate.Month, guestReq.EntryDate.Day);//helper variable that contains entrydate 
            while (helper != out1)
            {
                if (hostingU.Diary[helper.Month - 1, helper.Day - 1] != false) {
                    throw new Exception("BL: DiaryMaker: this hosting unit is busy in those days");
                }
                helper = helper.AddDays(1);
            }
            DateTime helper2 = new DateTime(guestReq.EntryDate.Year, guestReq.EntryDate.Month, guestReq.EntryDate.Day);//helper variable that contains entrydate 
            while (helper2 != out1)
            {
                hostingU.Diary[helper2.Month - 1, helper2.Day - 1] = true;
                helper2 = helper2.AddDays(1);
            }
            dal.updateDiary(hostingU);            
            //dal.updateDiary( HostingUnit h);

        }
        //****************************************
        public bool ApproveRequest(DateTime date, int days, HostingUnit h)
        {//function that recieves the entry date and release date and checks if the request can be approved if yes change these dates it the diary to true
            DateTime in1 = new DateTime(date.Year, date.Month, date.Day);//entry date
            DateTime out1 = new DateTime(date.Year, date.Month, date.Day);//release date
            out1=out1.AddDays(days - 1);
            DateTime helper = new DateTime(date.Year, date.Month, date.Day);//helper variable that contains entrydate 
            while (helper.Date != out1.Date)
            {
                if (h.Diary[helper.Month - 1, helper.Day - 1] != false) { return false; }
                helper = helper.AddDays(1);
            }
            return true;
        }

        //***********************************************
        public void medgrade(HostingUnit h)//מחזיר ממוצע
        {
            if (dal.GetHostingUnitsList().Exists(item => item.HostingUnitKey == h.HostingUnitKey) == true)
            {
                double med=0;
                int up=0;
                int down=0;
                up = h.numawful1 * 1 + h.numbad2 * 2 + h.numfine3 * 3 + h.numgood4 * 4 + h.numperfect5 * 5;
                down = h.numawful1 + h.numbad2 + h.numfine3 + h.numgood4 + h.numperfect5;
                med = up / down;
                h.medsatisfication = med;
                updateHostingUnit(h);
            }
            else
                throw new Exception("bl:bestGrade:no hosting unit is exist");
           
        }
        //***********************************************

        public List<HostingUnit> recomandendHostingUnit()//חייב להיות יותר מאפס בתים
        {
            if (dal.GetHostingUnitsList() == null)
            {
                throw new Exception("bl:bestGrade:no hosting unit is exist");
            }
            else
            {
                List<HostingUnit> hlist = GetHostingUnitsList();
                List<HostingUnit> Threebests = new List<HostingUnit>();
                HostingUnit helper = new HostingUnit();
                if (hlist.Count > 3)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        helper = bestGrade(hlist);
                        Threebests.Add(helper);
                        hlist.Remove(helper);
                    }
                }
                else
                {
                    Threebests = hlist;
                }
                return Threebests;

            }

        }//##############################################

        public HostingUnit bestGrade()//למי יש הכי הרבה פעמים הציון הכי טוב
        {

            if (dal.GetHostingUnitsList() == null)
            {
                throw new Exception("bl:bestGrade:no hosting unit is exist");
            }
            else
            {
                HostingUnit max = new HostingUnit();
            double howmany = 0;
            foreach (HostingUnit item in GetHostingUnitsList())
            {
                if (item.medsatisfication > howmany)
                {
                    howmany = item.medsatisfication;
                    max = item.clone();
                }
            }
            return GetHostingUnitsList().Find(item => item.HostingUnitKey == max.HostingUnitKey);
            }

            
        }
        //***********************************************

        public HostingUnit bestGrade(List<HostingUnit> lst)//למי יש הכי הרבה פעמים הציון הכי טוב
        {
            if (dal.GetHostingUnitsList() == null)
            {
                throw new Exception("bl:bestGrade:no hosting unit is exist");
            }
            else
            {
                HostingUnit max = new HostingUnit();
                double howmany = 0;
                foreach (HostingUnit item in lst)
                {
                    if (item.medsatisfication > howmany)
                    {
                        howmany = item.medsatisfication;
                        max = item.clone();
                    }
                }
                return GetHostingUnitsList().Find(item => item.HostingUnitKey == max.HostingUnitKey).clone();
            }
         }


       

        #endregion

        #region other:

        public List<HostingUnit> availableHostingUnits(DateTime date, int days)//+++++++++++++++++################
        {
            //IEnumerable<HostingUnit> list = from HostingUnit item in GetHostaingUnitsList()
            //                                where ApproveRequest(date, days, item);
            var list1 = GetHostingUnitsList().Where(item => ApproveRequest(date, days, item)).ToList();
            return list1;
        }
        //****************************************
        public int daysBetween(params DateTime[] date)
        {
            if (date.Length > 2 || date.Length < 1)
            {
                throw new Exception("ERROR the days between the dates must be one or two days");
            }
            int count = 0;
            if (date.Length == 1)
            {

                DateTime today1 = DateTime.Now;
                DateTime to, from;
                if (date[0].Date > today1.Date)
                {
                    to = date[0];
                    from = today1;
                }
                else
                {
                    if(today1.Date == date[0].Date)
                    {
                        return 0;
                    }
                    if (today1 != date[0])
                    {
                        to = today1.Date;
                        from = date[0].Date;
                    }
                }
                while (today1.Date != date[0].Date)
                {
                    count++;
                    date[0].AddDays(1);
                }
                return count;
            }
            else
            {
                if (date.Length == 2)
                {
                    if (date[0].Date > date[1].Date)
                    {
                        DateTime smalldate = date[1];
                        DateTime big = date[0];

                        while (smalldate.Date != big.Date)
                        {
                            count++;
                            smalldate=smalldate.AddDays(1);
                        }
                    }
                    else
                    {
                        DateTime smalldate = date[0];
                        DateTime big = date[1];
                        while (smalldate.Date != big.Date)
                        {
                            count++;
                           smalldate= smalldate.AddDays(1);
                            
                        }
                    }
                    return count;
                }
                return count;
            }
        }
        //****************************************
        public IEnumerable<Order> isDaysBigOrEqual(int days)
        {
            List<Order> ol = dal.GetOrderList();
            var v = from item in ol
                    where (days <= daysBetween(item.CreateDate)) || (days <= daysBetween(item.OrderDate))
                    select item;
            foreach (var item in v) { }
            return v;
        }
        //****************************************

        public IEnumerable<GuestRequest> getNewList(Predicate<GuestRequest> cond)
        {

            List<GuestRequest> x = dal.GetGuestRequestList();
            var v = from item in x
                    where cond(item)
                    select item;
            List<GuestRequest> list = new List<GuestRequest>();
            foreach (GuestRequest item in v)
            {
                list.Add(item);
            }
            return list;
        }
          
        public int numOfOrder(GuestRequest _guestRequest)
        {
            List<Order> listo = dal.GetOrderList();
            int count = 0;
            int grkey = _guestRequest.GuestRequestKey;
            foreach (Order item in GetOrderList())
                if (item.GuestRequestKey == grkey)
                    count++;
            return count;
        }


        //****************************************
        public int numOf(HostingUnit hostingUnit)//return the number of order that have been send or the number of order that closed successfuly by site
        {
            List<Order> listo = dal.GetOrderList();
            int count = 0;
            int hukey = hostingUnit.HostingUnitKey;
            var a = from item in listo
                    where item.HostingUnitKey == hukey
                    select item;
            foreach(var item1 in a)
            {
                if (item1.Status == orderStatus.closedResponse || item1.Status == orderStatus.emailSent)
                    count++;
            } 
            return count;

            
        }

        //****************************************
        public IEnumerable<IGrouping<areaHosting, GuestRequest>> groupingGRbyarea() {
            List<GuestRequest> grl= dal.GetGuestRequestList();
         var v = from item in grl
                 group item by item.Area into a
                 select  a ;
        foreach(var item2 in v) { }
            return v;
        }

        //****************************************
        public IEnumerable<IGrouping<int, GuestRequest>> groupingGRbynumofpeople()
        {
            List<GuestRequest> grl = dal.GetGuestRequestList();
            var v = from item in grl
                    let x=item.Adults + item.Children+item.baby
                    group item by x into a
                    select a;
            foreach (var item2 in v) { }
            return v;
        }
        public IEnumerable<IGrouping<int, GuestRequest>> groupingGRbymaxOfPeople(int y)
        {
            List<GuestRequest> grl = dal.GetGuestRequestList();
            var v = from item in grl
                    let x = item.Adults + item.Children + item.baby
                    where x < y
                    group  item by x into a
                    select a;
            foreach (var item2 in v) { }
            return v;
        }
        //****************************************
        /*ublic IEnumerable<IGrouping<int, Host>> GetAllHostGroupByNumberHostingUnit(bool toSort = false)
        {
            if (toSort) return from item in d.getHostingUnitsList()
                               group item.Owner by helpGetAllHostGroupByNumberHostingUnit(item.Owner) into f1
                               orderby f1.Key
                               select f1;

            else return from item in d.getHostingUnitsList()
                        group item.Owner by helpGetAllHostGroupByNumberHostingUnit(item.Owner) into f1
                        select f1;
        */
        public int helpnumberHUforThisHost(Host h)
        {
            int counter = 0;
            foreach(HostingUnit item in dal.GetHostingUnitsList())
            {
                if (item.Owner.HostKey == h.HostKey)
                {
                    counter++;
                }
            }
            return counter;
        }
        public IEnumerable<IGrouping<int, Host>> groupinghostbynumofHU()
        {
            var v = from item in dal.GetHostingUnitsList()
                    group item.Owner by helpnumberHUforThisHost(item.Owner) into a
                    select   a ;//select new
            foreach (var item2 in v) { }
            return v;
        }
        //****************************************
        public IEnumerable<IGrouping<areaHosting, HostingUnit>> groupingHUbyarea()
        {
            List<HostingUnit> HUlist = dal.GetHostingUnitsList();
            var v = from item in HUlist
                    group item by item.SubArea into a
                    select a;
            
            foreach (var item2 in v) { }
            return v;
        }
        











        #endregion

    }
}

/**/
