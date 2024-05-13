using LogisticsManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository.IRepository
{
    public interface ICustomerRepository
    {
        #region Manage Address
        Task<List<Address>?> GetAllAddresses(); // Get all Addresss
        Task<Address?> GetAddressById(int AddressId); // Get Address by id
        Task<int> AddAddress(Address address); // Add Address
        Task<int> UpdateAddress(Address address); // Update Address
        Task<int> RemoveAddress(int addressId); // Remove Address
        Task<List<City>> GetAllCities(); // Get all cities
        Task<List<State>> GetAllStates(); // Get all states
        Task<List<Country>> GetAllCountries(); // Get all countries
        #endregion

        #region Manage Order
        Task<List<OrderDetail>> GetAllOrderDetails(int orderId);
        Task<int> AddOrder(OrderDetail orderDetail, Order order);
        Task<List<OrderDetail>?> GetAllOrders(int customerId); // Get all orders of a particular customer
        #endregion
    }
}
