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
    public class AdminRepository : IAdminRepository
    {
        private readonly LogisticsManagementContext _dbContext;
        public AdminRepository(LogisticsManagementContext context)
        {

            _dbContext = context;

        }
        //Get user details by id
        public async Task<User?> GetUserById(int userId)
        {
            try
            {
                // Admin
                return await _dbContext.Users.Include(u => u.Role)
                                             .Include(u => u.UserDetails)
                                             .ThenInclude(u => u.Address)
                                             .ThenInclude(u => u.City)
                                             .ThenInclude(u => u.State)
                                             .ThenInclude(u => u.Country)
                                             .Where(u => u.Id == userId).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching user user id \n" + e.Message);
                return null;
            }
        }

        //Get user details by role id
        public async Task<List<User>> GetUsersByRoleId(int userRoleId)
        {
            try
            {

                return await _dbContext.Users.Include(u => u.Role)
                                       .Include(u => u.UserDetails)
                                       .Where(e => e.RoleId == userRoleId)
                                       .ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching users by role id \n" + e.Message);
                return new List<User>();
            }
        }

        //Get pending users details by role id
        public async Task<List<User>> GetPendingUsersByRoleId(int userRoleId)
        {
            try
            {
                return await _dbContext.Users.Include(u => u.Role)
                    .Include(u => u.UserDetails)
                    .Where(e => e.RoleId == userRoleId && e.UserDetails.Any(ud => ud.IsApproved == 0))
                    .ToListAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching pending users by role id \n" + e.Message);
                return [];
            }
        }

        //Update sign up request status to approved or rejected
        public async Task<int> UpdateSignUpRequest(int userIdToUpdate, int updatedState)
        {
            try
            {
                UserDetail? userDetail = await _dbContext.UserDetails.FirstOrDefaultAsync(ud => ud.UserId == userIdToUpdate);


                if (userDetail != null)
                {
                    userDetail.UpdatedAt = DateTime.Now;
                    userDetail.IsApproved = updatedState;
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }

            catch (Exception)
            {
                Console.WriteLine("An Error occurred while updating user");
                return -1;
            }
        }

        // Soft delete user by making isactive = false
        public async Task<int> DeleteUserById(int userIdToDelete)
        {
            try
            {
                User? user = await _dbContext.Users.Include(u => u.UserDetails)
                    .FirstOrDefaultAsync(u => u.Id == userIdToDelete && u.IsActive == true);
                if (user != null)
                {
                    user.IsActive = false;
                    user.UpdatedAt = DateTime.Now;

                    UserDetail? userDetail = user?.UserDetails.FirstOrDefault();
                    userDetail.UpdatedAt = DateTime.Now;
                    userDetail.IsActive = false;

                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occurred while deleting user\n" + e.Message);
                return -1;
            }
        }

        public async Task<int> UnBlockUserById(int userId)
        {
            try
            {
                User? user = await _dbContext.Users.Include(u => u.UserDetails)
                    .FirstOrDefaultAsync(u => u.Id == userId && u.IsActive == false);
                if (user != null)
                {
                    user.IsActive = true;
                    user.UpdatedAt = DateTime.Now;

                    UserDetail? userDetail = user?.UserDetails.FirstOrDefault();
                    userDetail.UpdatedAt = DateTime.Now;
                    userDetail.IsActive = true;

                    return await _dbContext.SaveChangesAsync();
                }
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occurred while unblocking user\n" + e.Message);
                return -1;
            }
        }


        // Assign manager to warehouse 
        public async Task<int> AssignManagerToWarehouse(int managerId, int warehouseId)
        {
            try
            {
                UserDetail? userDetail = await _dbContext.UserDetails.FirstOrDefaultAsync(ud => ud.UserId == managerId);
                if (userDetail is not null)
                {
                    userDetail.WareHouseId = warehouseId;
                    return await _dbContext.SaveChangesAsync();
                }
                return 0;

            }
            catch (Exception e)
            {
                Console.WriteLine("An Error occurred while assigning manager to warehouse\n" + e.Message);
                return -1;
            }
        }


        public async Task<int> GetTotalUsersCount(int userRoleId)
        {
            try
            {
                return await _dbContext.Users.Where(u => u.RoleId == userRoleId).CountAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching total users count\n" + e.Message);
                return -1;
            }
        }

        #region ----------------- Wareouse Operations -----------------

        //Get all warehouses
        public async Task<List<Warehouse>?> GetAllWarehouses()
        {
            try
            {
                return await _dbContext.Warehouses.Include(w => w.City)
                                                  .ThenInclude(w => w.State)
                                                  .ThenInclude(w => w.Country)
                                                  .ToListAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching all warehouses\n" + e.Message);
                return null;
            }
        }

        //Get warehouse by id
        public async Task<Warehouse?> GetWarehouseById(int warehouseId)
        {
            try
            {

                if (warehouseId > 0)
                {
                    Warehouse? warehouse = await _dbContext.Warehouses.Include(w => w.City)
                                          .ThenInclude(w => w.State)
                                         .ThenInclude(w => w.Country)
                                         .FirstOrDefaultAsync(w => w.Id == warehouseId);

                    if (warehouse is not null)
                    {
                        return warehouse;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching warehouse by id\n" + e.Message);
            }
            return null;
        }

        //Add warehouse
        public async Task<int> AddWarehouse(Warehouse warehouse)
        {
            try
            {
                await _dbContext.Warehouses.AddAsync(warehouse);
                int result = await _dbContext.SaveChangesAsync();
                return result > 0 ? warehouse.Id : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding warehouse: " + ex.Message);
                return -1;
            }
        }

        //Update warehouse
        public async Task<int> UpdateWarehouse(Warehouse warehouse)
        {
            try
            {
                if (warehouse is null)
                {
                    return 0;
                }


                Warehouse? warehouseDetails = await _dbContext.Warehouses.Where(w => w.Id == warehouse.Id).FirstOrDefaultAsync();

                if (warehouseDetails is null)
                {
                    return 0;
                }
                //_dbContext.Entry(warehouseDetails).State = EntityState.Detached;

                warehouseDetails.Name = warehouse.Name;
                warehouseDetails.Address = warehouse.Address;
                warehouseDetails.CityId = warehouse.CityId;
                warehouseDetails.UpdatedAt = DateTime.Now;
                //_dbContext.Warehouses.Update(warehouse);
                //_dbContext.Warehouses.Attach(warehouse);
                //_dbContext.Entry(warehouse).State = EntityState.Modified;
                int result = await _dbContext.SaveChangesAsync();
                return result > 0 ? warehouse.Id : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating warehouse: " + ex.Message);
                return -1;
            }
        }

        //Update warehouse using Patch
        public async Task<int> UpdateWarehousePatch(Warehouse warehouse)
        {
            try
            {
                if (warehouse is null)
                {
                    return 0;
                }


                Warehouse? warehouseDetails = await _dbContext.Warehouses.Where(w => w.Id == warehouse.Id).FirstOrDefaultAsync();

                if (warehouseDetails is null)
                {
                    return 0;
                }

                warehouseDetails.UpdatedAt = DateTime.Now;
                _dbContext.Entry(warehouse).State = EntityState.Modified;
                int result = await _dbContext.SaveChangesAsync();
                return result > 0 ? warehouse.Id : 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating warehouse: " + ex.Message);
                return -1;
            }
        }

        //soft delete warehouse by making isactive = false
        public async Task<int> RemoveWarehouse(int warehouseId)
        {
            try
            {
                var warehouse = await _dbContext.Warehouses.FirstOrDefaultAsync(w => w.Id == warehouseId && w.IsActive == true);
                if (warehouse != null)
                {
                    warehouse.IsActive = false;
                    warehouse.UpdatedAt = DateTime.Now;
                    int result = await _dbContext.SaveChangesAsync();
                    return result > 0 ? warehouse.Id : 0;

                }
                return 0;

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while removing warehouse: " + ex.Message);
                return -1;
            }
        }


        public async Task<int> GetTotalWarehousesCount()
        {

            try
            {
                return await _dbContext.Warehouses.CountAsync();

            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching total warehouse count \n" + e.Message);
                return -1;
            }
        }

        #endregion



        #region------------- Admin Statistics -------------

        public async Task<int> GetTotalOrdersCount()
        {
            return await _dbContext.Orders.CountAsync();
        }

        public async Task<decimal> GetTotalSalesAmount()
        {
            decimal totalSalesAmount = await _dbContext.Orders
                .Where(o => o.OrderDetails.All(od => od.OrderStatus.Status == "Delivered"))
                .SumAsync(od => od.TotalAmount);

            return totalSalesAmount;
        }

        #endregion
    }
}
