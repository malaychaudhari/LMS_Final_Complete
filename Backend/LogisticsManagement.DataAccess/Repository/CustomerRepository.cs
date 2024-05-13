using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly LogisticsManagementContext _dbContext;
        public CustomerRepository(LogisticsManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> AddAddress(Address address)
        {
            try
            {
                UserDetail? userDetail = _dbContext.UserDetails.FirstOrDefault(u => u.UserId == address.UserId);

                if (userDetail == null)
                {
                    return -2;
                }

                await _dbContext.Addresses.AddAsync(address);

                int result = await _dbContext.SaveChangesAsync();

                if (result == 0)
                {
                    return 0;
                }
                userDetail.Address = address;
                int userUpdated = await _dbContext.SaveChangesAsync();

                return userUpdated > 0 ? address.Id : 0;
                //return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding address: " + ex.Message);
                return -1;
            }
        }

        public async Task<Address?> GetAddressById(int AddressId)
        {
            try
            {

                if (AddressId > 0)
                {
                    Address? address = await _dbContext.Addresses.Include(w => w.City)
                                          .ThenInclude(w => w.State)
                                         .ThenInclude(w => w.Country)
                                         .FirstOrDefaultAsync(w => w.Id == AddressId);

                    if (address is not null)
                    {
                        return address;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching address by id\n" + e.Message);
            }
            return null;
        }
        public async Task<List<Address>?> GetAllAddresses()
        {
            try
            {
                return await _dbContext.Addresses.Include(w => w.City)
                                                  .ThenInclude(w => w.State)
                                                  .ThenInclude(w => w.Country)
                                                  .ToListAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching all addresses\n" + e.Message);
                return null;
            }
        }
        public async Task<int> UpdateAddress(Address address)
        {
            try
            {
                if (address is null)
                {
                    return 0;
                }


                Address? addressDetails = await _dbContext.Addresses.Where(w => w.Id == address.Id).FirstOrDefaultAsync();

                if (addressDetails is null)
                {
                    return 0;
                }



                _dbContext.Entry(addressDetails).CurrentValues.SetValues(address);
                _dbContext.Entry(addressDetails).Property(x => x.UserId).IsModified = false;
                _dbContext.Entry(addressDetails).Property(x => x.IsActive).IsModified = false;
                _dbContext.Entry(addressDetails).Property(x => x.CreatedAt).IsModified = false;

                int result = await _dbContext.SaveChangesAsync();
                return result > 0 ? address.Id : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating address: " + ex.Message);
                return -1;
            }
        }
        public async Task<int> RemoveAddress(int addressId)
        {
            try
            {
                Address? address = await _dbContext.Addresses.FirstOrDefaultAsync(w => w.Id == addressId && w.IsActive == true);

                if (address != null)
                {
                    address.IsActive = false;
                    address.UpdatedAt = DateTime.Now;

                    UserDetail? userDetail = _dbContext.UserDetails.FirstOrDefault(u => u.UserId == address.UserId);

                    if (userDetail == null)
                    {
                        return -2;
                    }
                    userDetail.AddressId = null;
                    int result = await _dbContext.SaveChangesAsync();

                    //if (result > 0)
                    //{
                    //    UserDetail? userDetail = _dbContext.UserDetails.FirstOrDefault(u => u.UserId == address.UserId);

                    //    if (userDetail == null)
                    //    {
                    //        return -2;
                    //    }
                    //    userDetail.AddressId = null;
                    //}
                    return result > 0 ? address.Id : 0;

                }
                return 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while deleting address: " + ex.Message);
                return -1;
            }
        }
        public async Task<List<OrderDetail>> GetAllOrderDetails(int orderId)
        {
            try
            {
                List<OrderDetail> orderDetails = await _dbContext.OrderDetails
                    .Include(i => i.Inventory)
                    .Include(s => s.OrderStatus)
                    .Include(o => o.Order)
                    .ThenInclude(u => u.User)
                    .ThenInclude(u => u.UserDetails)
                    .ThenInclude(a => a.Address)
                    .ThenInclude(c => c.City)
                    .ThenInclude(s => s.State)
                    .ThenInclude(c => c.Country)
                    .Where(o => o.OrderId == orderId).ToListAsync();

                return orderDetails;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while fetching order details. " + ex.Message);
                return null;
            }
        }
        public async Task<List<City>> GetAllCities()
        {
            try
            {
                return await _dbContext.Cities.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while fetching Cities" + ex.Message);
                return null;
            }
        }
        public async Task<List<State>> GetAllStates()
        {
            try
            {
                return await _dbContext.States.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while fetching States" + ex.Message);
                return null;
            }
        }
        public async Task<List<Country>> GetAllCountries()
        {
            try
            {
                return await _dbContext.Countries.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while fetching Cities" + ex.Message);
                return null;
            }
        }

        public async Task<int> AddOrder(OrderDetail orderDetail, Order order)
        {
            try
            {
                await _dbContext.Orders.AddAsync(order);
                orderDetail.Order = order;
                orderDetail.ExpectedArrivalTime = DateTime.Now.AddDays(2);
                await _dbContext.OrderDetails.AddAsync(orderDetail);

                Inventory? inventory = _dbContext.Inventories.FirstOrDefault(i => i.Id == orderDetail.InventoryId);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    inventory.Stock -= orderDetail.Quantity;

                    if (await _dbContext.SaveChangesAsync() > 0)
                    {
                        return order.Id;
                    }
                }

                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding order: " + ex.Message);
                return -2;
            }
        }

        public async Task<List<OrderDetail>> GetAllOrders(int customerId)
        {
            try
            {
                return await _dbContext.OrderDetails.Include(o => o.Order).Include(i => i.Inventory).Where(u => u.Order.UserId == customerId).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while fetching orders." + ex.Message);
                return null;
            }
        }
    }
}
