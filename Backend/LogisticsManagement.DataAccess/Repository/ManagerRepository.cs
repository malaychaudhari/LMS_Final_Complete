using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace LogisticsManagement.DataAccess.Repository
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly LogisticsManagementContext _db;
        public ManagerRepository(LogisticsManagementContext db)
        {
            _db = db;
        }



        // Manage Inventory Category
        #region Manage Inventory Category
        // Fetch All Inventory Categories
        public async Task<List<InventoryCategory>?> GetInventoryCategories()
        {
            try
            {
                return await _db.InventoryCategories.Where(c => c.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventory Categories.");
                return null;
            }
        }

        // Fetch Inventory Category by Id
        public async Task<InventoryCategory?> GetInventoryCategory(int id)
        {
            try
            {
                return await _db.InventoryCategories.Where(c => c.IsActive == true).FirstOrDefaultAsync(inv => inv.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventory category by id.");
                return null;
            }
        }

        // Add Inventory Category
        public async Task<int?> AddInventoryCategory(InventoryCategory category)
        {
            try
            {
                InventoryCategory? existingCategory = await _db.InventoryCategories.FirstOrDefaultAsync(c => c.Name == category.Name);
                if (existingCategory == null)
                {
                    var entry = _db.InventoryCategories.Add(category);
                    if (await _db.SaveChangesAsync() > 0)
                    {
                        Console.WriteLine("id : " + entry.Entity.Id);
                        return entry.Entity.Id;
                    }
                    return 0;
                }
                else
                {
                    if (existingCategory.IsActive == true)
                    {
                        return -2;
                    }
                    else
                    {
                        existingCategory.IsActive = true;
                        _db.InventoryCategories.Update(existingCategory);
                        if (await _db.SaveChangesAsync() > 0)
                        {
                            return existingCategory.Id;
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Adding Category.");
                return -1;
            }
        }


        // Remove Inventory Category
        public async Task<int> RemoveInventoryCategory(int id)
        {
            try
            {
                var category = await _db.InventoryCategories.FindAsync(id);
                if (category != null)
                {
                    category.IsActive = false;
                    _db.InventoryCategories.Update(category);
                    return await _db.SaveChangesAsync();
                }
                return -2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Removing Category.");
                return -1;
            }
        }
        #endregion



        // Manage Inventory
        #region Manage Inventory
        // Fetch All Inventories
        public async Task<List<Inventory>?> GetInventories()
        {
            try
            {
                return await _db.Inventories.Include(c=>c.Category).Where(inv => inv.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventories.");
                return null;
            };
        }

        // Fetch Inventory by Id
        public async Task<Inventory?> GetInventory(int id)
        {
            try
            {
                return await _db.Inventories.Include(i => i.Category)
                    .Where(inv => inv.IsActive == true).FirstOrDefaultAsync(inv => inv.Id == id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventory.");
                return null;
            }
        }

        // Add Inventory
        public async Task<int> AddInventory(Inventory inventory)
        {
            try
            {
                Inventory? existingInventory = await _db.Inventories.FirstOrDefaultAsync(inv => inv.Name == inventory.Name);
                if (existingInventory == null)
                {
                    var inv = _db.Inventories.Add(inventory);

                    if (await _db.SaveChangesAsync() > 0)
                    {
                        return inv.Entity.Id;
                    }
                    return 0;
                }
                else
                {
                    if (existingInventory.IsActive == true)
                    {
                        return -2;
                    }
                    else
                    {
                        existingInventory.IsActive = true;
                        _db.Inventories.Update(inventory);
                        if (await _db.SaveChangesAsync() > 0)
                        {
                            return existingInventory.Id;
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Adding Inventory.");
                return -1;
            }
        }

        // Remove Inventory
        public async Task<int> RemoveInventory(int id)
        {
            try
            {
                var inventory = await _db.Inventories.FindAsync(id);
                if (inventory != null)
                {
                    inventory.IsActive = false;
                    _db.Inventories.Update(inventory);
                    return await _db.SaveChangesAsync();
                }
                return -2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Removing Inventory.");
                return -1;
            }
        }

        // Update Inventory
        public async Task<int> PatchInvetory(Inventory inventory)
        {
            try
            {
                var existingInventory = _db.Inventories.Local.FirstOrDefault(inv => inv.Id == inventory.Id);
                if (existingInventory != null)
                {
                    _db.Entry(existingInventory).State = EntityState.Detached;
                }

                _db.Entry(inventory).State = EntityState.Modified;
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Updating Inventory.");
                return -1;
            }
        }

        // Update Inventory
        public async Task<int> PutInventory(Inventory inventory)
        {
            try
            {
                if (!_db.Inventories.Local.Any(inv => inv.Id == inventory.Id))
                {
                    _db.Inventories.Attach(inventory);
                }
                _db.Entry(inventory).State = EntityState.Modified;
                return await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Updating Inventory.");
                return -1;
            }
        }
        #endregion



        // Manage Vehicle Type 
        #region Manage Vehicle Type
        // Fetch Vechicle Type
        public async Task<List<VehicleType>?> GetVehicleType()
        {
            try
            {
                return await _db.VehicleTypes.Where(v => v.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Vehicle Type.");
                return null;
            }
        }
        #endregion



        // Manage Vehicle
        #region Manage Vehicles
        // Fetch Vehicles
        public async Task<List<Vehicle>?> GetVehicles()
        {
            try
            {
                return await _db.Vehicles.Include(v => v.VehicleType).Where(v => v.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Vehicles.");
                return null;
            };
        }

        // Add vehicle
        public async Task<int> AddVehicle(Vehicle vehicle)
        {
            try
            {
                Vehicle? existingVehicle = await _db.Vehicles.FirstOrDefaultAsync(v => v.VehicleNumber == vehicle.VehicleNumber);
                if (existingVehicle == null)
                {
                    _db.Vehicles.Add(vehicle);
                    return await _db.SaveChangesAsync();
                }
                else
                {
                    if (existingVehicle.IsActive == true)
                    {
                        return -2;
                    }
                    else
                    {
                        existingVehicle.IsActive = true;
                        _db.Vehicles.Update(vehicle);
                        return await _db.SaveChangesAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Adding Vehicle.");
                return -1;
            }
        }

        // Remove Vehicle
        public async Task<int> RemoveVehicle(int id)
        {
            try
            {
                var vehicle = await _db.Vehicles.FindAsync(id);
                if (vehicle != null)
                {
                    vehicle.IsActive = false;
                    _db.Vehicles.Update(vehicle);
                    return await _db.SaveChangesAsync();
                }
                return -2;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Removing Vehicle.");
                return -1;
            }
        }
        #endregion


        // Statistics
        #region Statistics
        public async Task<decimal?> GetInvenoryCount()
        {
            try
            {
                decimal count =  _db.Inventories.Sum(i => i.Stock);
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventory Count.");
                return -1;
            }
        }
        public async Task<int?> GetInvetoryCategoryCount()
        {
            try
            {
                int count = await _db.InventoryCategories.Where(i => i.IsActive == true).CountAsync();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventory Category Count.");
                return -1;
            }
        }
        public async Task<int?> GetVehicleCount()
        {
            try
            {
                int count = await _db.Vehicles.Where(i => i.IsActive == true).CountAsync();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Vehicle Count.");
                return -1;
            }
        }
        public async Task<int?> GetAvailableVehicleCount()
        {
            try
            {
                int count = await _db.Vehicles.Where(i => i.IsActive == true).CountAsync();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Vehicle Count.");
                return -1;
            }
        }
        public async Task<int?> GetVehicleTypeCount()
        {
            try
            {
                int count = await _db.Vehicles.Where(u => u.IsActive == true && u.IsAvailable == true).CountAsync();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Vehicle Type Count.");
                return -1;
            }
        }
        public async Task<int?> GetDriverCount()
        {
            try
            {
                int count = await _db.Users.Where(u => u.IsActive == true  && u.RoleId == 3).CountAsync();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Drivers Count.");
                return -1;
            }
        }
        public async Task<int?> GetAvailableDriverCount()
        {
            try
            {
                int count = await _db.Users.Where(u => u.IsActive == true && u.RoleId == 3 && u.UserDetails.All(d=>d.IsAvailable == true)).CountAsync();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Drivers Count.");
                return -1;
            }
        }
        public async Task<int?> GetOrderCount()
        {
            try
            {
                int count = await _db.Orders.CountAsync();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventory Count.");
                return -1;
            }
        }
        public async Task<int?> GetPendingOrderCount()
        {
            try
            {
                int count = await _db.Orders.Where(o => o.OrderDetails.All(od => od.OrderStatus.Status == "Pending")).CountAsync();
                return count;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventory Count.");
                return -1;
            }
        }
        #endregion


        #region Resource Mapping
        public async Task<int> AssignOrder(ResourceMapping assignment)
        {
            try
            {
                // driver isAvailable = false
                UserDetail? userDetail = await _db.UserDetails.FirstOrDefaultAsync(d => d.UserId == assignment.DriverId);
                userDetail.IsAvailable = false;
                _db.UserDetails.Update(userDetail);

                // change order status
                OrderDetail orderDetail = await _db.OrderDetails.FirstOrDefaultAsync(o => o.OrderId == assignment.OrderId);
                orderDetail.OrderStatusId = 2;
                _db.OrderDetails.Update(orderDetail);

                // vehicle isAvailable false
                Vehicle vehicle = await _db.Vehicles.FirstOrDefaultAsync(v => v.Id == assignment.VehicleId);
                vehicle.IsAvailable = false;
                _db.Vehicles.Update(vehicle);

                _db.ResourceMappings.Add(assignment);
                return _db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return -1;
            }
        }
        public async Task<List<ResourceMapping>> getAssignedOrders()
        {
            try
            {
                return await _db.ResourceMappings.ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventory Categories.");
                return null;
            }
        }
        #endregion


        #region Manage Orders
        public async Task<List<Order>?> getOrders()
        {
            try
            {
                return await _db.Orders.Include(u=>u.User)
                                       .Include(o => o.OrderDetails)
                                       .ThenInclude(i=>i.Inventory)
                                       //.ThenInclude(d => d.OrderStatus)
                                       .ToListAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("An Error Occurred While Fetching Inventories.");
                return null;
            };
        }
        #endregion
    }
}
