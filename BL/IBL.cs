using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using BL;
using System.Net.Mail;

namespace BL
{
    public interface IBL
     {
        void closethreadbyMainWindow();

        IEnumerable<IGrouping<int, GuestRequest>> groupingGRbymaxOfPeople(int y);
        List<int> getordersKeyByTz(int _id);
    
        List<int> gethostingUnitKeyByTz(int _id);
        List<int> GetguestRequestkeys();
        List<int> listMyRequest(string id);

        List<int> Gethostingunitkeys();

        IEnumerable<GuestRequest> getNewList(Predicate<GuestRequest> cond);
        int helpnumberHUforThisHost(Host h);
        /// <summary>
        /// function that recieves a hosting unit and calculate its avarage grade
        /// </summary>
        /// <param name="h">hosting unit to calculate its avarage grade</param>
        void medgrade(HostingUnit h);
        /// <summary>
        /// function that send email
        /// </summary>
        /// <param name="o">order </param>
        /// <param name="subj">the subject of email</param>
        /// <param name="body">the body of the mail</param>
        void sendAnEmail(Order o);

        /// <summary>
        /// check if the HostingUnit is empty if yes it throw an exeption if not it it send to function to find the list of recomended HostingUnits
        /// </summary>
        /// <returns>the list of recomended HostingUnits </returns>
        List<HostingUnit> recomandendHostingUnit();
        /// <summary>
        /// check if the HostingUnit is empty if yes it throw an exeption if not it it send to function to find the HostingUnit with the best grade
        /// </summary>
        /// <param name="lst">list of HostingUnit  </param>
        /// <returns>the HostingUnit with the best grade</returns>
        HostingUnit bestGrade(List<HostingUnit> lst);


        //helpers(:

        /// <summary>
        /// function that calculate the number of the orders that was suggested to this GuestRequest
        /// </summary>
        /// <param name="_guestRequest">GuestRequest that we want to check how many orders was suggested</param>
        /// <returns>the number of orders was suggested</returns>
        int numOfOrder(GuestRequest _guestRequest);
        /// <summary>
        /// function that return all the available HostingUnit in requested dates
        /// </summary>
        /// <param name="date"> the entry date</param>
        /// <param name="days"> how long the vocation is</param>
        /// <returns></returns>
        List<HostingUnit> availableHostingUnits(DateTime date, int days);
        /// <summary>
        /// calculate the days between two dates and if was given one date it calculate the dates between the date and now
        /// </summary>
        /// <param name="date">dates to calculte the days between</param>
        /// <returns></returns>
        int daysBetween(params DateTime[] date);
        //  IEnumerable<GuestRequest> getNewList(conditionDelegate cond);
        /// <summary>
        ///calculate the number of order that have been send or the number of order that closed successfuly by site
        /// </summary>
        /// <param name="hostingUnit"></param>
        /// <returns>hostingUnit to check the number of orders</returns>
        int numOf(HostingUnit hostingUnit);
        /// <summary>
        /// make a lists of all GuestRequest with the same area requested
        /// </summary>
        /// <returns>the list that the function made</returns>
        IEnumerable<IGrouping<areaHosting, GuestRequest>> groupingGRbyarea();
        /// <summary>
        /// make a lists of all GuestRequest with the same number of people
        /// </summary>
        /// <returns>the list that the function made</returns>
        IEnumerable<IGrouping<int, GuestRequest>> groupingGRbynumofpeople();
        /// <summary>
        /// make a lists of all Host with the same number of HostingUnit
        /// </summary>
        /// <returns>the list that the function made</returns>
        IEnumerable<IGrouping<int, Host>> groupinghostbynumofHU();
        /// <summary>
        /// make a lists of all HostingUnit with the same area of location
        /// </summary>
        /// <returns>the list that the function made</returns>
        IEnumerable<IGrouping<areaHosting, HostingUnit>> groupingHUbyarea();
        /// <summary>
        /// make a list of all orders that the days between the order creation to now or the days from the date that the email was send is smaller then receved days
        /// </summary>
        /// <param name="days">number od days </param>
        /// <returns>the list of HostingUnit that was calculate</returns>
        IEnumerable<Order> isDaysBigOrEqual(int days);
        /// <summary>
        /// function that return the HostingUnit that the HostingUnitKey is equal to the recievd one
        /// </summary>
        /// <param name="_hostingUnitKey">the hostingUnitKey to search the proper HostingUnit  </param>
        /// <returns>the HostingUnit with the same HostingUnitKey</return
        HostingUnit getHostingUnitByKey(int _hostingUnitKey);
        /// <summary>
        /// function that return the GuestRequest that the GuestRequestKey is equal to the recievd one
        /// </summary>
        /// <param name="_GuestRequestKey">the GuestRequestKey to search the proper GuestRequest</param>
        /// <returns>the GuestRequest with the same GuestRequestKey</returns>
        GuestRequest getGuestRequestByKey(int _GuestRequestKey);
        /// <summary>
        /// //function that recieves an order checks if the request can be approved if yes change these dates it the diary to true
        /// </summary>
        /// <param name="o">the order that we want to make the diary for</param>
        void diarymaker(Order o);
        /// <summary>
        /// //function that recieves the entry date and number of days and checks if the request can be approved 
        /// </summary>
        /// <param name="date">the entry date</param>
        /// <param name="days">how long the vocation</param>
        /// <param name="h">the HostingUnit that we wantt to check if the rquest can be approved</param>
        /// <returns></returns>
        bool ApproveRequest(DateTime date, int days, HostingUnit h);
        /// <summary>
        /// function that check if the request can be approved if yes it update the status to close by site and makes the diary and update all the GuestRequest status with the same 
        /// GuestRequestKey to close by site and calculate the fee for all the days
        /// if the request cant be approvwd it throw an exeption
        /// </summary>
        /// <param name="o">the order that we want to close</param>
        /// <param name="s">the status to update</param>
        void orderclose(Order o, orderStatus s);
        /// <summary>
        /// find the GuestRequest and HostingUnit if it cant be found it throw an exeption
        /// check if the amount of people is bigger then the maximum amount of people if yes it throw an exeption
        /// and then calculate the price for an order
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        double priceForAnOrder(Order o);

        //GuestRequest(: 
      
        /// function that recieves GuestRequest and checks whether the EntryDate
        /// is bigger or equal to the ReleaseDate if yes it throw an exeption
        /// and if not it adds the GuestRequest to the GuestRequestList
        /// </summary>
        /// <param name="r">the GuestRequest to add</param> 
        void addGuestRequest(GuestRequest r);//!!!
        /// <summary>
        /// function that receives GuestRequest and status to update 
        /// and update the status
        /// </summary>
        /// <param name="r">GuestRequest to update its status</param>
        /// <param name="s">status to update</param>
        void updateGuestRequestStatus(GuestRequest r, grStatus s);//!!

        //HostingUnit(:
        /// <summary>
        /// function that recieves HostingUnit and adds it to the 
        /// HostingUnitList in dal
        /// </summary>
        /// <param name="h">HostingUnit to add</param>
        void addHostingUnit(HostingUnit h);//!!
        /// <summary>
        /// function that recieves HostingUnit and go over the order list
        /// to check whether the status of the order of this HostingUnit is open if yes it throw an error
        /// if not it dalete this HostingUnit from HostingUnitList
        /// </summary>
        /// <param name="h">HostingUnit to delete</param>
        void deleteHostingUnit(HostingUnit h);//!!8
        /// <summary>
        /// function that recieves HostingUnit and update it if the owner doesnt have CollectionClearance and the status of the 
        /// guestRequest for this HostingUnit is active it throw an exeption if not it update the HostingUnit 
        /// </summary>
        /// <param name="h"></param>
        void updateHostingUnit(HostingUnit h);//9
        //Order(:
        /// <summary>
        /// function that recieves an order and checks if the request can be approved 
        /// if yes it adds the order to the order list if not it throw an error
        /// </summary>
        /// <param name="o">order to add</param>
        void addOrder(Order o);//3
        /// <summary>
        /// function that receives an order and status and check the status
        /// if the order is closed it throw an error
        /// if the status is email sent it sends an email 
        /// </summary>
        /// <param name="o">order to update its status </param>
        /// <param name="s">status to update in given order</param>
        void updateOrderStatus(Order o, orderStatus so);//2!,4!,5,6! ,7,10!

        //list(:
        /// <summary>
        /// check if the HostingUnit list is empty if yes ut throw an exeption if not return the HostingUnitsList
        /// </summary>
        /// <returns>the HostingUnitsList</returns>
        List<HostingUnit> GetHostingUnitsList();
        /// <summary>
        /// check if the GuestRequest list is empty if yes it throw an exeption if not return the GuestRequestList
        /// </summary>
        /// <returns>the GuestRequestList</returns>
        List<GuestRequest> GetGuestRequestList();
        /// <summary>
        /// check if the order list is empty if yes it throw an exeption if not return the OrederList
        /// </summary>
        /// <returns>the OrderList</returns>
        List<Order> GetOrderList();
        /// <summary>
        /// check if the BankBranch list is empty if yes it throw an exeption if not return the BankBranchList
        /// </summary>
        /// <returns>the BankBranchList</returns>
        List<BankBranch> GetBankBranchList();
        double feeForAllDays();


        //IEnumerable<GuestRequest> getNewList(conditionDelegate cond);




        /* public static bool IsHebrewOrEnglishChar(char c)
                {

                    string he = "אבגדהוזחטיכלמנסעפצקרשתםןץףך";
                    string en = "abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM ";
                    if ((he.IndexOf(c) != -1) || (c == '\b') || (c == ' ') || (en.IndexOf(c) != -1))
                    {
                        return true;
                    }
                    else
                        return false;

                }


                public static bool IsHebrewOrEnglishOrNumberChar(char c)
                {
                    string number = "1234567890";
                    string he = "אבגדהוזחטיכלמנסעפצקרשתםןץףך";
                    string en = "abcdefghijklmnopqrstuvwxyzQWERTYUIOPASDFGHJKLZXCVBNM ";
                    if ((he.IndexOf(c) != -1) || (c == '\b') || (c == ' ') || (en.IndexOf(c) != -1) || (number.IndexOf(c) != -1))
                    {
                        return true;
                    }
                    else
                        return false;

                }
                public static bool IsNumber(char c)
                {
                    string number = "1234567890";

                    if ((number.IndexOf(c) != -1) || (c == '\b'))
                    {
                        return true;
                    }
                    else
                        return false;

                }
                public static bool checki(String tz)//מקבלת מס' בעל 9 ספרות ובודקת אם הוא ת.ז
                {
                    int sum = 0, num = 0;
                    for (int i = 0; i < 9; i++)
                    {
                        num = (Convert.ToInt32(tz[i].ToString())) * (i % 2 + 1);
                        num = num / 10 + num % 10;
                        sum += num;
                    }
                    return sum % 10 == 0;



                }

                public static bool IsHebrewOrChar(char c)
                {

                    string he = "אבגדהוזחטיכלמנסעפצקרשתםןץףך";

                    if ((he.IndexOf(c) != -1) || (c == '\b') || (c == ' '))
                    {
                        return true;
                    }
                    else
                        return false;

                }

                /// <summary>
                /// checks weather the name is ligeal or throw exeption
                /// </summary>
                /// <param name="_value"></param>
                public static void isValidName(string _value)
                {
                    if (_value != null)
                    {
                        if (_value.Length > 25) throw new Exception("BL:Too long name!!");
                        char current;
                        for (int i = 0; i < _value.Length; i++)
                        {
                            current = _value[i];

                            if (((current < 65) && (current != 32)) || (current > 122) || ((current > 90) && (current < 96)))
                                throw new Exception("BL:name must contain only letters and spaces!");
                        }
                    }
                }

                // public static void isValidNumberOfPeople(int num)
                //{
                //    if (num < 0) throw new Exception("can't be less than 0");
                //}

                /// <summary>
                /// checks weather the Id is ligeal or throw exeption
                /// </summary>
                /// <param name="_id"></param>
                public static void isValidId(string _id)
                {
                    if (_id != null)
                    {
                        if (_id.Length != 9) throw new Exception("BL:Id number must have 9 digits!");
                        char current;
                        for (int i = 0; i < 9; i++)
                        {
                            current = _id[i];
                            if ((current < 48) || (current > 57)) throw new Exception("BL:Id must contain only numbers!");
                        }
                    }
                }

                /// <summary>
                /// checks weather the cell number is ligeal or throw exeption
                /// </summary>
                /// <param name="_number"></param>
                public static void isValidCell(string _number)
                {
                    if (_number != null)
                    {
                        if (_number.Length != 10) throw new Exception("BL:Cell Phone number must have 10 digits!");
                        char current;
                        if ((_number[0] != '0') || (_number[1] != '5')) throw new Exception("BL:Cell Phone number must begin with '05'!");
                        for (int i = 0; i < 9; i++)
                        {
                            current = _number[i];
                            if ((current < 48) || (current > 57)) throw new Exception("BL:Cell Phone must contain only numbers!");
                        }
                    }
                }

                /// <summary>
                /// checks if the date is not in future
                /// </summary>
                /// <param name="_number"></param>
                public static void isValidBirthDate(DateTime _date)
                {
                    if (_date >= DateTime.Today)
                        throw new Exception("Birth date cannot be later then now!");
                }*/






    }
}
