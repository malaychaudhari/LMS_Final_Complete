using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.Services.IServices
{
    public interface IAdminService
    {
        Task<UserDTO> GetUserByIdAsync(int id); // Get user by id
        Task<List<UserDTO>> GetUsersByRoleAsync(int roleId); // Get users by role
        Task<List<UserDTO>> GetPendingUsersByRoleAsync(int roleId); // Get manager or driver whose approval is pending
        Task<int> UpdateUserSignUpRequestAsync(int userId, int updatedStatus); // Approve or reject user sign up request
        Task<int> DeleteUserAsync(int userId); // Delete user
        Task<int> UnBlockUserById(int userId);

        Task<int> AssignManagerToWarehouseAsync(int managerId, int warehouseId); // Assign manager to warehouse


        Task<List<WarehouseDTO>?> GetAllWarehousesAsync(); // Get all warehouses
        Task<WarehouseDTO?> GetWarehouseByIdAsync(int warehouseId); // Get warehouse by id
        Task<int> AddWarehouseAsync(WarehouseDTO warehouse); // Add warehouse
       
        Task<int> UpdateWarehouseAsync(WarehouseDTO warehouse); // Update warehouse
        
        Task<int> UpdateWarehousePatchAsync(WarehouseDTO warehouse); // Update warehouse using patch
        
        Task<int> RemoveWarehouseAsync(int warehouseId); // Remove warehouse


        Task<AdminSummaryStatisticsDTO?> GetAdminSummaryStatisticsAsync();

    }

}
