using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using DAL;

namespace DAL
{
    public interface IDAL
    {

        // List<int> Gethostingunitkeys();

        //List<int> GetguestRequestkeys();

       void  updateDiary(HostingUnit h);


        //GuestRequest(:
        /// <summary>
        /// function that recieves a guestRequest an checks if exist 
        /// if yes it throw an error
        /// if not it adds the GuestRequest
        /// </summary>
        /// <param name="r">GuestRequest to add</param>
        void addGuestRequest(GuestRequest r);
       
        /// <summary>
        /// function that recieves GuestRequest and status to update
        /// if it exist it update the status
        /// if not it creates a new GuestRequest and add to the guestRequestlist
        /// </summary>
        /// <param name="r">GuestRequest that we want to update its status</param>
        /// <param name="s">the status to update in GuestRequest</param>
        /// <returns>true if the update was succeeded</returns>
        bool updateGuestRequestStatus(GuestRequest r, grStatus s);

        //HostingUnit(:
        /// <summary>
        /// function that recieves a HostingUnit and checks if exist
        /// if yes it throw an error and if not it adds the HostingUnit
        /// </summary>
        /// <param name="h">the HostingUnit to add</param>
        void addHostingUnit(HostingUnit h);
        /// <summary>
        /// function that recieves a HostingUnit and checks if exist
        /// if yes it deletes the HostingUnit if not it throw an error
        /// </summary>
        /// <param name="h">the HostingUnit to delete</param>
        void deleteHostingUnit(HostingUnit h);
        /// <summary>
        /// function that recieves a HostingUnit and checks if there is an order for this HostingUnit
        /// if yes and the CollectionClearance of the hosts is not equal it throw and error
        /// since the order is not closed yet
        /// if the CollectionClearance is equal it update the HostingUnit
        /// </summary>
        /// <param name="h">the HostingUnit to update</param>
        /// <returns>true if the update was succeeded</returns>
        bool updateHostingUnit(HostingUnit h);

        //Order(:
        /// <summary>
        /// function that recieves an Order and checks if exist if yes it deletes the order
        /// and adds the recieved order and if its not exist it adds the received order
        /// </summary>
        /// <param name="o">the order to add</param>
        void addOrder(Order o);
        /// <summary>
        /// function that recieve an order and a status to update if the order exist it update its status 
        /// and if its not exist it creates ane otder and update the status and throw an error
        /// </summary>
        /// <param name="o">order ,to update its status </param>
        /// <param name="so">the status to update</param>
        void updateOrderStatus(Order o, orderStatus so);


        //list(:
        /// <summary>
        /// function that returns the HostingUnitsList from dataSource
        /// </summary>
        /// <returns>the HostingUnits list from dateSource</returns>
        List<HostingUnit> GetHostingUnitsList();
        /// <summary>
        /// function that returns the GuestRequestList from dataSource
        /// </summary>
        /// <returns>the GuestRequest list from dateSource</returns>
        List<GuestRequest> GetGuestRequestList();
        /// <summary>
        /// function that returns OrderList from dataSource
        /// </summary>
        /// <returns>the Order list from dateSource</returns>
        List<Order> GetOrderList();
        /// <summary>
        /// function that creates 5 BankBranch and returns the list that was created by them
        /// </summary>
        /// <returns>the BankBranch list from dateSource</returns>
        List<BankBranch> GetBankBranchList();

    }
}
