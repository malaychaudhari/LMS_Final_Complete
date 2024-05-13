using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.Services.IServices
{
    public interface IDriverService
    {
        Task<List<ResourceMappingDTO>?> GetAllAssignedOrdersAsync(int driverId);
        Task<List<ResourceMappingDTO>?> GetAllCompletedOrdersAsync(int driverId);
        Task<int>? UpdateStatusAsync(UpdateStatusDTO updateStatus);

        Task<List<OrderStatusDTO>> GetOrderStatusAsync();
    }
}
