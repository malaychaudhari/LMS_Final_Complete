using LogisticsManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository.IRepository
{
    public interface IManagerRepository
    {

        #region Manage Inventory Category 
        public Task<List<InventoryCategory>?> GetInventoryCategories();
        public Task<InventoryCategory?> GetInventoryCategory(int id);
        public Task<int?> AddInventoryCategory(InventoryCategory category);
        public Task<int> RemoveInventoryCategory(int id);
        #endregion


        #region Manage Inventory 
        public Task<List<Inventory>?> GetInventories();
        public Task<Inventory?> GetInventory(int id);
        public Task<int> AddInventory(Inventory inventory);
        public Task<int> RemoveInventory(int id);
        public Task<int> PatchInvetory(Inventory inventory);
        public Task<int> PutInventory(Inventory inventory);
        #endregion


        #region Manage Vehicles Type
        public Task<List<VehicleType>?> GetVehicleType();
        #endregion


        #region Manage Vehicles
        public Task<List<Vehicle>?> GetVehicles();
        public Task<int> AddVehicle(Vehicle vehicle);
        public Task<int> RemoveVehicle(int id);
        #endregion


        #region Statistics
        public Task<decimal?> GetInvenoryCount();
        public Task<int?> GetInvetoryCategoryCount();
        public Task<int?> GetVehicleCount();
        public Task<int?> GetAvailableVehicleCount();
        public Task<int?> GetVehicleTypeCount();
        public Task<int?> GetDriverCount();
        public Task<int?> GetAvailableDriverCount();
        public Task<int?> GetOrderCount();
        public Task<int?> GetPendingOrderCount();
        #endregion

        #region Resource Mapping
        public Task<int> AssignOrder(ResourceMapping assignment);
        public Task<List<ResourceMapping>> getAssignedOrders();
        #endregion

        #region Manage Orders
        public Task<List<Order>?> getOrders();
        #endregion
    }
}
