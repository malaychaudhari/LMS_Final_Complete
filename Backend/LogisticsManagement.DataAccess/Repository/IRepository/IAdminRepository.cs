using LogisticsManagement.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository.IRepository
{
    public interface IAdminRepository
    {
        // Methods for user's related operations
        #region Users related operations

        Task<User?> GetUserById(int userId);//Get user details by id
        Task<List<User>> GetUsersByRoleId(int userRoleId); // Get all users by role id
        Task<List<User>> GetPendingUsersByRoleId(int userRoleId); // Get all pending users by role id
        Task<int> UpdateSignUpRequest(int userIdToUpdate, int updatedState); // Update user's signup request status to approved or rejected
        Task<int> DeleteUserById(int userIdToDelete); // Delete user by id

        Task<int> UnBlockUserById(int userId); // Delete user by id

        Task<int> AssignManagerToWarehouse(int managerId, int warehouseId); // Assign manager to warehouse

        Task<int> GetTotalUsersCount(int userRoleId); // Get total users count by role

        #endregion

        // Methods for warehouse table operations
        #region Warehouse Table Operations

        Task<List<Warehouse>?> GetAllWarehouses(); // Get all warehouses
        Task<Warehouse?> GetWarehouseById(int warehouseId); // Get warehouse by id
        Task<int> AddWarehouse(Warehouse warehouse); // Add warehouse
        Task<int> UpdateWarehouse(Warehouse warehouse); // Update warehouse

        Task<int> UpdateWarehousePatch(Warehouse warehouse); // Update warehouse using patch
        Task<int> RemoveWarehouse(int warehouseId); // Remove warehouse

        Task<int> GetTotalWarehousesCount(); // Get total warehouses count

        #endregion

        Task<int> GetTotalOrdersCount();
        Task<decimal> GetTotalSalesAmount();

    }
}
