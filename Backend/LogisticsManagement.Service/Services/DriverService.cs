using AutoMapper;
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository;
using LogisticsManagement.DataAccess.Repository.IRepository;
using LogisticsManagement.Service.Convertors;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Enums;
using LogisticsManagement.Service.Services.IServices;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssignmentStatus = LogisticsManagement.Service.Enums.AssignmentStatus;
using OrderStatus = LogisticsManagement.DataAccess.Models.OrderStatus;

namespace LogisticsManagement.Service.Services
{
    public class DriverService : IDriverService
    {
        private readonly IDriverRepository _driverRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;

        public DriverService(IDriverRepository driverRepository, IAdminRepository adminRepository, IMapper mapper)
        {
            _driverRepository = driverRepository;
            _adminRepository = adminRepository;
            _mapper = mapper;
        }
        public async Task<List<ResourceMappingDTO>?> GetAllAssignedOrdersAsync(int driverId)
        {
            try
            {
                List<ResourceMapping>? assignedOrders = await _driverRepository.ViewAssignedOrders(driverId);
                

                if (assignedOrders is null || assignedOrders.Count == 0)
                    return null;

                return _mapper.Map<List<ResourceMappingDTO>>(assignedOrders);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while fetching the assigned orders. " + ex.Message);
                return null;
            }
        }

        public async Task<List<ResourceMappingDTO>?> GetAllCompletedOrdersAsync(int driverId)
        {
            try
            {
                List<ResourceMapping>? completedOrders = await _driverRepository.ViewCompletedOrders(driverId);

                if (completedOrders is null || completedOrders.Count == 0)
                    return null;

                return _mapper.Map<List<ResourceMappingDTO>>(completedOrders);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while fetching the completed orders. " + ex.Message);
                return null;
            }
        }

        public async Task<int>? UpdateStatusAsync(UpdateStatusDTO updateStatus)
        {
            try
            {
                return await _driverRepository.UpdateStatus(updateStatus.orderId, updateStatus.orderStatusId);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating user" + ex.Message);
                return -1;
            }
        }

        public async Task<List<OrderStatusDTO>> GetOrderStatusAsync()
        {
            try
            {
                List<OrderStatus> orderStatuses = await _driverRepository.GetOrderStatuses();
                return _mapper.Map<List<OrderStatusDTO>>(orderStatuses);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while updating user" + ex.Message);
                return [];
            }
        }
    }
}
