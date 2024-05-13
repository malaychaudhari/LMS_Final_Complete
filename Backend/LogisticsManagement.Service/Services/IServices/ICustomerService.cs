using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.Services.IServices
{
    public interface ICustomerService
    {
        #region Manage Address
        Task<List<AddressDTO>?> GetAllAddressAsync(); // Get all Addresses
        Task<AddressDTO?> GetAddressByIdAsync(int addressId); // Get Address by id
        Task<int> AddAddressAsync(AddressDTO address); // Add Address

        Task<int> UpdateAddressAsync(AddressDTO address); // Update Address
        Task<int> RemoveAddressAsync(int addressId); // Remove Address

        Task<List<CityDTO>?> ViewAllCitiesAsync(); // Get all cities
        Task<List<StateDTO>?> ViewAllStatesAsync(); // Get all States
        Task<List<CountryDTO>?> ViewAllCountriesAsync(); // Get all Countries
        #endregion

        #region Manage Orders
        Task<List<OrderDTO>?> ViewOrderDetails(int orderId); // View Order Details
        Task<List<OrderDTO>?> ViewAllOrdersAsync(int customerId);  // View All Orders
        Task<int> AddOrderAsync(OrderDTO order);
        #endregion
    }
}
