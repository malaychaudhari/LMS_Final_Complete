using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.Services.IServices
{
    public interface IManagerService
    {
        #region Manage Inventory Category
        Task<List<InventoryCategoryDTO>?> GetInventoryCategories();
        Task<InventoryCategoryDTO?> GetInventoryCategory(int id);
        Task<int?> AddInventoryCategory(InventoryCategoryDTO category);
        Task<int> RemoveInventoryCategory(int id);

        #endregion


        #region Manage Inventory
        Task<List<InventoryDTO>?> GetInventories();
        Task<InventoryDTO?> GetInventory(int id);
        Task<int> AddInventory(InventoryDTO inventory);
        Task<int> RemoveInventory(int id);
        Task<int> UpdateInventory(InventoryDTO inventory);
        public Task<int> PutInventory(InventoryDTO inventory);
        #endregion


        #region Manage Vehicles Type
        public Task<List<VehicleTypeDTO>?> GetVehicleType();
        #endregion


        #region Manage Vehicles
        public Task<List<VehicleDTO>?> GetVehicles();
        public Task<int> AddVehicle(VehicleDTO vehicle);
        public Task<int> RemoveVehicle(int id);
        #endregion


        #region Statistics
        public Task<ManagerSummaryStatisticsDTO?> GetManagerStatistics();
        #endregion


        #region Resource Mapping
        public Task<int> AssignOrder(ResourceMappingDTO assignment);
        public Task<List<ResourceMappingDTO>> getAssignedOrders();
        #endregion

        #region Manage Order 
        public Task<List<OrderDTO>?> getOrders();
        #endregion
    }
}
