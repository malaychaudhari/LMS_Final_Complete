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
    public class DriverRepository : IDriverRepository
    {
        private readonly LogisticsManagementContext _dbContext;
        public DriverRepository(LogisticsManagementContext context) 
        {
            _dbContext = context;
        }

        #region ---------------- Order Operations ------------------------------
        public async Task<List<ResourceMapping>?> ViewAssignedOrders(int driverId)
        {
            try
            {
                List<ResourceMapping> resourceMappings = await _dbContext.ResourceMappings
                    .Include(u => u.Manager)
                    .Include(a => a.AssignmentStatus)
                    .Include(v => v.Vehicle)
                    .Include(o => o.Order)
                    .ThenInclude(u => u.User)
                    .ThenInclude(u => u.UserDetails)
                    .ThenInclude(a => a.Address)
                    .Where(r => r.DriverId == driverId && r.AssignmentStatusId == 1).ToListAsync();

                return resourceMappings;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching Assigned Orders \n" + e.Message);
                return null;
            }
        }

        public async Task<List<ResourceMapping>> ViewCompletedOrders(int driverId)
        {
            try
            {
                List<ResourceMapping> resourceMappings = await _dbContext.ResourceMappings
                    .Include(u => u.Manager)
                    .Include(a => a.AssignmentStatus)
                    .Include(v => v.Vehicle)
                    .Include(o => o.Order)
                    .ThenInclude(u => u.User)
                    .ThenInclude(u => u.UserDetails)
                    .ThenInclude(a => a.Address)
                    .Where(r => r.DriverId == driverId && r.AssignmentStatusId == 2).ToListAsync();

                return resourceMappings;
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while fetching Completed Orders \n" + e.Message);
                return null;
            }
        }
        #endregion

        #region -------------------- Status Operations ----------------------
        public async Task<int> UpdateStatus(int orderId, int orderStatusId)
        {
            try
            {   
                // change order status
                OrderDetail orderDetail = await _dbContext.OrderDetails.FirstOrDefaultAsync(o => o.OrderId == orderId);
                orderDetail.OrderStatusId = orderStatusId;
                orderDetail.ActualArrivalTime= DateTime.Now;
                orderDetail.UpdatedAt = DateTime.Now;

                _dbContext.OrderDetails.Update(orderDetail);

                // change assignment status
                ResourceMapping resource = await _dbContext.ResourceMappings.Where(u => u.OrderId == orderId).FirstOrDefaultAsync();
                resource.AssignmentStatusId = 2;
                int driverId = resource.DriverId;
                _dbContext.ResourceMappings.Update(resource);

                // change user availability status
                UserDetail user = await _dbContext.UserDetails.Where(u => u.UserId == driverId).FirstOrDefaultAsync();
                user.IsAvailable = true;
                _dbContext.UserDetails.Update(user);

                return _dbContext.SaveChanges();
            }

            catch (Exception)
            {
                Console.WriteLine("An Error occurred while updating status");
                return -1;
            }
        }

        public async Task<List<OrderStatus>?> GetOrderStatuses()
        {
            try
            {
                return await _dbContext.OrderStatuses.ToListAsync();
            }

            catch (Exception)
            {
                Console.WriteLine("An Error occurred while updating user");
                return [];
            }
        }
        #endregion
    }
}
