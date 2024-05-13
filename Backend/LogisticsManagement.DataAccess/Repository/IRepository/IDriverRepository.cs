using LogisticsManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository.IRepository
{
    public interface IDriverRepository
    {
        Task<List<ResourceMapping>?> ViewAssignedOrders(int driverId);
        Task<List<ResourceMapping>> ViewCompletedOrders(int driverId);
        Task<int>? UpdateStatus(int orderId, int orderStatusId);

        Task<List<OrderStatus>?> GetOrderStatuses(); 
    }
}
