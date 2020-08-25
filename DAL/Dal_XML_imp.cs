using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using BE;
using System.ComponentModel;
using System.Threading;
using System.Net;
namespace DAL
{
  public class Dal_XML_imp : IDAL
    {

        //static Dal_XML_imp instance = new Dal_XML_imp();
       
        XElement grRoot;
        XElement orderRoot;
        XElement huRoot;
        XElement BankRoot;
        //XElement hostRoot;
        //XElement atmsRoot;
        XElement ConfigRoot;
        //const string hostPath = @"HostXml.xml";
        const string huPath = @"HostinUnitXml.xml";
        const string grPath = @"GuestRequestXML.xml";
        const string orderPath = @"oredrXML.xml";
      //  const string atmsPath = @"atmsXml.xml";
        const string ConfigPath = @"configurationXml.xml";
        const string xmlLocalPath = @"atmXML.xml";

        List<HostingUnit> hostingUnits = new List<HostingUnit>();
        BackgroundWorker worker = new BackgroundWorker();
        bool IsDownloadFileComplete = false;

        public Dal_XML_imp()//we dont do it private since we cant operate it with the dal factory
        {
            try
            {
                worker.DoWork += Worker_DoWork;
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                worker.RunWorkerAsync();
                if (!File.Exists(ConfigPath))
                {
                    ConfigRoot = new XElement("allConfig", new XElement("GuestRequestKeyCode", 10000000), new XElement("HostingUnitKeyCode", 10000000),
                                  new XElement("ExpireRequest", 35), new XElement("OrderKeyCode", 10000000), new XElement("fee", 10));
                    ConfigRoot.Save(ConfigPath);
                }
                else ConfigRoot = XElement.Load(ConfigPath);

                if (!File.Exists(grPath))
                {
                     grRoot = new XElement("GuestRequests");
                     grRoot.Save(grPath);
                     }
                 else grRoot=XElement.Load(grPath);
                if (!File.Exists(huPath))
                {
                    huRoot = new XElement("HostingUnits");
                    huRoot.Save(huPath);
                    }
                else
                {
                    try { huRoot = XElement.Load(huPath); }
                    catch { Console.WriteLine("File upload problem"); }
                }

                if (!File.Exists(orderPath))
                {
                    orderRoot = new XElement("Orders");
                     orderRoot.Save(orderPath);
                   }
                else orderRoot=XElement.Load(orderPath);
            }
            catch (Exception e)
            {
                throw e;
            }

        }
        #region BackgroundWorker
        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (IsDownloadFileComplete == false)
            {
                WebClient wc = new WebClient();
                try
                {
                    string xmlServerPath =
                    @"https://www.boi.org.il/he/BankingSupervision/BanksAndBranchLocations/Lists/BoiBankBranchesDocs/atm.xml";
                    wc.DownloadFile(xmlServerPath, xmlLocalPath);
                    IsDownloadFileComplete = true;
                }
                catch (Exception)
                {
                    try
                    {
                        string xmlServerPath = @"http://www.jct.ac.il/~coshri/atm.xml";
                        wc.DownloadFile(xmlServerPath, xmlLocalPath);
                        IsDownloadFileComplete = true;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(2000);
                    }
                }
                finally
                {
                    wc.Dispose();
                }
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BankRoot = XElement.Load(xmlLocalPath);
            var allBranchByAtm = from item in BankRoot.Elements()
                                 select new XElement("BankBranch", new XElement("bankNumber", item.Element("קוד_בנק").Value),
                                 new XElement("bankName", item.Element("שם_בנק").Value), new XElement("branchNumber", item.Element("קוד_סניף").Value),
                                 new XElement("branchAddress", item.Element("כתובת_ה-ATM").Value), new XElement("branchCity", item.Element("ישוב").Value));
            BankRoot = new XElement("allBranch");
            foreach (var item in allBranchByAtm)
            {
                BankRoot.Add(item);
            }
            BankRoot.Save(xmlLocalPath);
        }

        public static void SaveToXML<T>(T source, string path)
        {
            FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write);
            XmlSerializer xmlSer = new XmlSerializer(source.GetType());
            xmlSer.Serialize(file, source);
            file.Close();
        }
        public static T LoadFromXML<T>(string path)
        {
            FileStream file = new FileStream(path, FileMode.Open, FileAccess.Read);
            XmlSerializer xmlSer = new XmlSerializer(typeof(T));
            T result = (T)xmlSer.Deserialize(file);
            file.Close();
            return result;
        }
        #endregion

        #region HostingUnit :)
        XElement ConverHostingUnitToXElement(HostingUnit hu)
        {
            XElement convered = new XElement("HostingUnit");
            convered.Add(new XElement("HostingUnitKey", hu.HostingUnitKey));
            convered.Add(new XElement("HostingUnitName", hu.HostingUnitName));
            XElement helperuri = new XElement("Uris");

            foreach (Uri item in hu.Uris)
            {
                helperuri.Add(new XElement("Uri1", item));
            }
            convered.Add(helperuri);

            convered.Add(new XElement("feeAll", hu.feeAll));
            convered.Add(new XElement("numawful1", hu.numawful1));
            convered.Add(new XElement("numbad2", hu.numbad2));
            convered.Add(new XElement("numfine3", hu.numfine3));
            convered.Add(new XElement("numgood4", hu.numgood4));
            convered.Add(new XElement("numperfect5", hu.numperfect5));
            convered.Add(new XElement("medsatisfication", hu.medsatisfication));
            convered.Add(new XElement("maxGuest", hu.maxGuest));
            convered.Add(new XElement("priceAdults", hu.priceAdults));
            convered.Add(new XElement("priceChildren", hu.priceChildren)); 
            convered.Add(new XElement("SubArea", hu.SubArea));
             convered.Add(hu.Diaryxml);
            XElement hostXelement = new XElement("Owner");
            XElement BankBranchXelement = new XElement("BankBranchDetails");
            BankBranchXelement.Add(new XElement("BankNumber", hu.Owner.BankBranchDetails.BankNumber));
            BankBranchXelement.Add(new XElement("BankName", hu.Owner.BankBranchDetails.BankName));
            BankBranchXelement.Add(new XElement("BranchNumber", hu.Owner.BankBranchDetails.BranchNumber));
            BankBranchXelement.Add(new XElement("BranchAddress", hu.Owner.BankBranchDetails.BranchAddress));
            BankBranchXelement.Add(new XElement("BranchCity", hu.Owner.BankBranchDetails.BranchCity));

            hostXelement.Add(new XElement("code", hu.Owner.code));
            hostXelement.Add(new XElement("HostKey", hu.Owner.HostKey));
            hostXelement.Add(new XElement("PrivateName", hu.Owner.PrivateName));
            hostXelement.Add(new XElement("FamilyName", hu.Owner.FamilyName));
            hostXelement.Add(new XElement("FhoneNumber", hu.Owner.FhoneNumber));
            hostXelement.Add(new XElement("MailAddress", hu.Owner.MailAddress));
            hostXelement.Add(BankBranchXelement);
            hostXelement.Add(new XElement("BankAccountNumber", hu.Owner.BankAccountNumber));
            hostXelement.Add(new XElement("CollectionClearance", hu.Owner.CollectionClearance));
            convered.Add(hostXelement);

            return convered;
        }
       
        public void addHostingUnit(HostingUnit hu)
        {
            try
            {
                XElement HostingUnitElement = (from p in huRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == hu.HostingUnitKey
                                               select p).FirstOrDefault();
                if (HostingUnitElement != null)
                    throw new Exception("DAL: HostingUnit with the same HostingUnitKey already exists...");
                else
                {
                    hu.HostingUnitKey = int.Parse(ConfigRoot.Element("HostingUnitKeyCode").Value)+1;//
                    ConfigRoot.Element("HostingUnitKeyCode").Value = (hu.HostingUnitKey).ToString();
                    ConfigRoot.Save(ConfigPath);

                    hu.Diary = new bool[13, 32];
                    for (int i = 1; i < 13; i++)
                    {
                        for (int j = 1; j < 32; j++)
                        {
                            hu.Diary[i, j] = false;
                        }
                    }
                    
                    huRoot.Add(ConverHostingUnitToXElement(hu));
                    huRoot.Save(huPath);
                  //  throw new Exception("DAL:Add HostingUnit was successed");
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void deleteHostingUnit(HostingUnit h)
        {
            try
            {
                XElement HostingUnitElement = (from p in huRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == h.HostingUnitKey
                                               select p).FirstOrDefault();
                if (HostingUnitElement == null)
                    throw new Exception("DAL: HostingUnit has not found !!!");
                HostingUnitElement.Remove();
                huRoot.Save(huPath);
            }
            catch (Exception)
            {
                throw;
            }
            //bool exists = hostingUnits.Exists(item => item.HostingUnitKey == h.HostingUnitKey);
            //if (exists == false)
            //    throw new Exception("This object isn't exist, Cannot delete");
            //hostingUnits.Remove(hostingUnits.Find(item => h.HostingUnitKey == item.HostingUnitKey));
            //SaveToXML(hostingUnits, huPath);

        }
        public bool updateHostingUnit(HostingUnit h)
        {
            try
            {
                XElement HostingUnitElement = (from p in huRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == h.HostingUnitKey
                                               select p).FirstOrDefault();
                if (HostingUnitElement == null)
                    throw new Exception("this element is not exist");
                HostingUnitElement.Remove();
                huRoot.Add(ConverHostingUnitToXElement(h));
                huRoot.Save(huPath);
            }
            catch (Exception)
            {

                throw;
            }


            return true;
        }
        public void updateDiary(HostingUnit h)
        {
            try
            {
                XElement HostingUnitElement = (from p in huRoot.Elements()
                                               where Convert.ToInt32(p.Element("HostingUnitKey").Value) == h.HostingUnitKey
                                               select p).FirstOrDefault();
                if (HostingUnitElement == null)
                    throw new Exception("this element is not exist");
                HostingUnitElement.Remove();
                huRoot.Add(ConverHostingUnitToXElement(h));
                huRoot.Save(huPath);
            }
            catch (Exception)
            {

                throw;
            }

        }

        #endregion

        #region GuestRequest
        //.ToString("dd/mm/yyyy")
        public void addGuestRequest(GuestRequest r)
        {
            r.GuestRequestKey = int.Parse(ConfigRoot.Element("GuestRequestKeyCode").Value) + 1;
            ConfigRoot.Element("GuestRequestKeyCode").Value = (r.GuestRequestKey).ToString();
            ConfigRoot.Save(ConfigPath);
            grRoot.Add(new XElement("GuestRequest", new XElement("GuestRequestKey", r.GuestRequestKey),
                new XElement("ID", r.ID), new XElement("PrivateName", r.PrivateName), new XElement("FamilyName", r.FamilyName)
              , new XElement("MailAddress", r.MailAddress), new XElement("Status", r.Status), new XElement("Area", r.Area), new XElement("SubArea", r.SubArea)
              , new XElement("Type", r.Type), new XElement("Adults", r.Adults), new XElement("Children", r.Children), new XElement("baby", r.baby)
              , new XElement("Pool", r.Pool), new XElement("RegistrationDate", r.RegistrationDate.Date), new XElement("EntryDate", r.EntryDate.Date), new XElement("ReleaseDate", r.ReleaseDate.Date)
              , new XElement("Jacuzzi", r.Jacuzzi), new XElement("Garden", r.Garden), new XElement("ChildrensAttractions", r.ChildrensAttractions)));
            grRoot.Save(grPath);
        }
        public bool updateGuestRequestStatus(GuestRequest r, grStatus s) {
            
                XElement myguest = grRoot.Elements().FirstOrDefault(item => Convert.ToInt32(item.Element("GuestRequestKey").Value) == r.GuestRequestKey);
                if (myguest == null)
                    throw new Exception(" updateGuestRequestStatus:dal:This object isn't exist, can't make a change");
                myguest.Element("status").Value = s.ToString();
                grRoot.Save(grPath);
            
            return true;
        }

        public GuestRequest getOnegr(int key)
        {
            XElement myguest = grRoot.Elements().FirstOrDefault(item => Convert.ToInt32(item.Element("GuestRequestKey").Value) == key);
            if (myguest == null)
                throw new Exception("getOnegr :This object isn't exist");
            GuestRequest g = new GuestRequest();
            g.ID = myguest.Element("ID").Value;
            g.GuestRequestKey = Convert.ToInt32(myguest.Element("GuestRequestKey").Value);
            g.PrivateName = myguest.Element("PrivateName").Value;
            g.FamilyName = myguest.Element("FamilyName").Value;
            g.MailAddress = myguest.Element("MailAddress").Value;
            g.Status = (grStatus)Enum.Parse(typeof(grStatus), myguest.Element("Status").Value);
            g.RegistrationDate = Convert.ToDateTime(myguest.Element("RegistrationDate").Value).Date;
            g.EntryDate = Convert.ToDateTime(myguest.Element("EntryDate").Value).Date;
            g.ReleaseDate = Convert.ToDateTime(myguest.Element("ReleaseDate").Value).Date;
            g.Area = (areaHosting)Enum.Parse(typeof(areaHosting), myguest.Element("Area").Value);
            g.SubArea = (subareaHosting)Enum.Parse(typeof(subareaHosting), myguest.Element("SubArea").Value);
            g.Type = (typeHosting)Enum.Parse(typeof(typeHosting), myguest.Element("Type").Value);
            g.Adults = Convert.ToInt32(myguest.Element("Adults").Value);
            g.Children = Convert.ToInt32(myguest.Element("Children").Value);
            g.Pool = (options)Enum.Parse(typeof(options), myguest.Element("Pool").Value);
            g.Jacuzzi = (options)Enum.Parse(typeof(options), myguest.Element("Jacuzzi").Value);
            g.Garden = (options)Enum.Parse(typeof(options), myguest.Element("Garden").Value);
            g.ChildrensAttractions = (options)Enum.Parse(typeof(options), myguest.Element("ChildrensAttractions").Value);
            return g.clone();
        }





        #endregion

        #region Order:)
     
        public void addOrder(Order o)
        {
            o.OrderKey= int.Parse(ConfigRoot.Element("OrderKeyCode").Value) + 1;
            ConfigRoot.Element("OrderKeyCode").Value = (o.OrderKey).ToString();
            ConfigRoot.Save(ConfigPath);
            orderRoot.Add(new XElement("Order", new XElement("totalPrice", o.totalPrice),
                new XElement("HostingUnitKey", o.HostingUnitKey), new XElement("GuestRequestKey", o.GuestRequestKey), new XElement("OrderKey", o.OrderKey)
              , new XElement("Status", o.Status), new XElement("CreateDate", o.CreateDate), new XElement("OrderDate", o.OrderDate)));
            orderRoot.Save(orderPath);
        }
        public void updateOrderStatus(Order o, orderStatus so)
        {

            XElement myorder = orderRoot.Elements().FirstOrDefault(item => int.Parse(item.Element("OrderKey").Value) == o.OrderKey);

            if (myorder == null)
                throw new Exception("dalXml:updateOrderStatus  : cant update order since it is not exists");

            myorder.Element("OrderDate").Value = o.OrderDate.ToString();
            myorder.Element("Status").Value = so.ToString();
            orderRoot.Save(orderPath);   
        }
        

        public Order getOneOrder(int key)
        {
            XElement myorder = orderRoot.Elements().FirstOrDefault(item => int.Parse(item.Element("OrderKey").Value) == key);
            if (myorder == null)
                throw new Exception("This object isn't exist");
            Order order = new Order();
            order.GuestRequestKey = int.Parse(myorder.Element("GuestRequestKey").Value);
            order.HostingUnitKey = int.Parse(myorder.Element("HostingUnitKey").Value);
            order.OrderKey = int.Parse(myorder.Element("OrderKey").Value);
            order.Status = (orderStatus)Enum.Parse(typeof(orderStatus), myorder.Element("Status").Value);
            order.CreateDate = Convert.ToDateTime(myorder.Element("CreateDate").Value);
            order.OrderDate = Convert.ToDateTime(myorder.Element("OrderDate").Value);
            order.totalPrice = int.Parse(myorder.Element("totalPrice").Value);
            return order.clone();
        }
        #endregion
        #region lists
        public List<GuestRequest> GetGuestRequestList()
        {
            var v = from item in grRoot.Elements()
                    let Guester = getOnegr(Convert.ToInt32(item.Element("GuestRequestKey").Value))
                    select Guester.clone();
            return v.ToList();
        }
        public List<HostingUnit> GetHostingUnitsList()
        {
          var v= (from p in huRoot.Elements()
                     
                    select new HostingUnit()
                    {

                        HostingUnitKey = int.Parse(p.Element("HostingUnitKey").Value),
                        feeAll = double.Parse(p.Element("feeAll").Value),
                        numawful1 = int.Parse(p.Element("numawful1").Value),
                        numbad2 = int.Parse(p.Element("numbad2").Value),
                        numfine3 = int.Parse(p.Element("numfine3").Value),
                        numgood4 = int.Parse(p.Element("numgood4").Value),
                        numperfect5 = int.Parse(p.Element("numperfect5").Value),
                        medsatisfication = double.Parse(p.Element("medsatisfication").Value),
                        maxGuest = int.Parse(p.Element("maxGuest").Value),
                        priceAdults = double.Parse(p.Element("priceAdults").Value),
                        priceChildren = double.Parse(p.Element("priceChildren").Value),
                        SubArea = (areaHosting)Enum.Parse(typeof(areaHosting), p.Element("SubArea").Value),
                        HostingUnitName = (p.Element("HostingUnitName").Value),
                        Diaryxml = p.Element("diary"),
                       
                        Uris= new List<Uri>(),
                        Owner = new Host()
                        {
                            code = Convert.ToString(p.Element("Owner").Element("code").Value),
                            HostKey = int.Parse(p.Element("Owner").Element("HostKey").Value),
                            PrivateName = (p.Element("Owner").Element("PrivateName").Value),
                            FamilyName = (p.Element("Owner").Element("FamilyName").Value),
                            FhoneNumber = p.Element("Owner").Element("FhoneNumber").Value,
                            MailAddress = (p.Element("Owner").Element("MailAddress").Value),
                            BankAccountNumber = int.Parse(p.Element("Owner").Element("BankAccountNumber").Value),
                            CollectionClearance = bool.Parse(p.Element("Owner").Element("CollectionClearance").Value),
                            BankBranchDetails = new BankBranch()
                            {
                                BankNumber = int.Parse(p.Element("Owner").Element("BankBranchDetails").Element("BankNumber").Value),
                                BankName = (p.Element("Owner").Element("BankBranchDetails").Element("BankName").Value),
                                BranchNumber = int.Parse(p.Element("Owner").Element("BankBranchDetails").Element("BranchNumber").Value),
                                BranchAddress = (p.Element("Owner").Element("BankBranchDetails").Element("BranchAddress").Value),
                                BranchCity = (p.Element("Owner").Element("BankBranchDetails").Element("BranchCity").Value),

                            }
                        }
                    }).ToList<HostingUnit>();
            //var v2 = (from p2 in huRoot.Elements()
            //          from helper in p2.Elements()
            //          where helper== p2.Element("Uris") 
            //          select new List<Uri>() {
            //              new Uri(Convert.ToString( helper.Element("Uris").Value))

            //          }).ToList();//<List<Uri>>
            List<Uri> l = new List<Uri>();
            foreach (var v3 in v)
            {
                foreach (var p2 in huRoot.Elements())
                {
                    if (v3.HostingUnitKey == Convert.ToInt32(p2.Element("HostingUnitKey").Value))
                    {                 

                        foreach (var helper in p2.Element("Uris").Elements())
                        {
                            
                            v3.Uris.Add(new Uri(helper.Value));
                        
                        }
                    }
                };
            };

           
            return v;

            //var v = from item in hostingUnits
            //        select item.clone();
            //return v.ToList();
        }
        public List<Order> GetOrderList()
        {
            var v = from item in orderRoot.Elements()
                    let Orderr = getOneOrder(int.Parse(item.Element("OrderKey").Value))
                    select Orderr.clone();
            return v.ToList();
        }
        public List<BankBranch> GetBankBranchList() {

            IEnumerable<BankBranch> list = new List<BankBranch>();
            try
            {
                list = (from temp in BankRoot.Elements()
                        select new BankBranch()
                        {
                            BankNumber = Int32.Parse(temp.Element("bankNumber").Value),
                            BankName = temp.Element("bankName").Value,
                            BranchNumber = Int32.Parse(temp.Element("branchNumber").Value),
                            BranchAddress = temp.Element("branchAddress").Value,
                            BranchCity = temp.Element("branchCity").Value
                        }
                       );

                return list.ToList();
            }
            catch (Exception x)
            {
                throw new Exception("קובץ סניפי הבנק לא ירד עדיין");
            }
            
        }
        #endregion

    }
}
