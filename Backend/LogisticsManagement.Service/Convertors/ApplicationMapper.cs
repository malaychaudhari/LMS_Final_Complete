using AutoMapper;
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LogisticsManagement.Service.Convertors
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            try
            {

                CreateMap<User, UserDTO>()
                    .ForMember(dest => dest.AddressId, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().AddressId))
                    .ForMember(dest => dest.LicenseNumber, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().LicenseNumber))
                    .ForMember(dest => dest.WareHouseId, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().WareHouseId))
                    .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().IsAvailable))
                    .ForMember(dest => dest.IsApproved, opt => opt.MapFrom(src => src.UserDetails.FirstOrDefault().IsApproved))
                    .ReverseMap();
                CreateMap<UserDetail, UserDTO>().ReverseMap();

                CreateMap<Warehouse, WarehouseDTO>()
                     .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                     .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.City.State.Name))
                     .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.State.Country.Name));

                CreateMap<City, CityDTO>()
                  .ForMember(dest => dest.CityName, opt => opt.MapFrom(src => src.Name))
                  .ReverseMap();

                CreateMap<State, StateDTO>()
                    .ForMember(dest => dest.StateName, opt => opt.MapFrom(src => src.Name))
                    .ReverseMap();

                CreateMap<Country, CountryDTO>()
                    .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Name))
                    .ReverseMap();

                CreateMap<WarehouseDTO, Warehouse>();

                CreateMap<OrderStatusDTO, OrderStatus>().ReverseMap();

                CreateMap<Address, AddressDTO>()
                     .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address1))
                     .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City.Name))
                     .ForMember(dest => dest.State, opt => opt.MapFrom(src => src.City.State.Name))
                     .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.City.State.Country.Name))
                    .ReverseMap();

                CreateMap<AddressDTO, Address>()
                     .ForMember(dest => dest.Address1, opt => opt.MapFrom(src => src.Address));

                CreateMap<InventoryCategory, InventoryCategoryDTO>().ReverseMap();

                CreateMap<Inventory, InventoryDTO>()
                      .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
                
                CreateMap<InventoryDTO, Inventory>();

                CreateMap<VehicleType, VehicleTypeDTO>();

                CreateMap<OrderDetail, OrderDTO>()
                 //.ForMember(dest => dest.OrderDetailId, opt => opt.MapFrom(src => src.Id))
                 //.ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Order.Id))
                 .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
                 .ForMember(dest => dest.InventoryName, opt => opt.MapFrom(src => src.Inventory.Name))
                 .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                 .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.OrderStatusId))
                 //.ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderStatus.Status))
                 .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.Order.UserId))
                 .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal))
                 .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.Order.TotalAmount))
                 .ForMember(dest => dest.ExpectedArrivalTime, opt => opt.MapFrom(src => src.ExpectedArrivalTime)).ReverseMap();



                //CreateMap<OrderDTO, OrderDetail>();

                CreateMap<OrderDTO, Order>()
               .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.CustomerId))
               .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
               .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.SubTotal))
               .ReverseMap();

                CreateMap<OrderDTO, OrderDetail>()
                   .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.InventoryId))
                   .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                   .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.SubTotal))
                   .ForMember(dest => dest.ExpectedArrivalTime, opt => opt.MapFrom(src => src.ExpectedArrivalTime))
                   .ReverseMap();

                CreateMap<Order, OrderDTO>()
                    .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.UserId))
                    .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name))
                    .ForMember(dest => dest.OrderDetailId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().Id))
                    .ForMember(dest => dest.InventoryId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().InventoryId))
                    .ForMember(dest => dest.InventoryName, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().Inventory.Name))
                    .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().Quantity))
                    .ForMember(dest => dest.SubTotal, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().SubTotal))
                    .ForMember(dest => dest.OrderStatusId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().OrderStatusId))
                    .ForMember(dest => dest.OriginId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().OriginId))
                    .ForMember(dest => dest.DestinationId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().DestinationId))
                    .ForMember(dest => dest.ExpectedArrivalTime, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().ExpectedArrivalTime))
                    .ForMember(dest => dest.ActualArrivalTime, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().ActualArrivalTime))
                    .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().OrderStatusId))
                    .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.OrderDetails.FirstOrDefault().OrderStatus.Status));

                //CreateMap<OrderDTO, Order>();



               CreateMap<ResourceMapping, ResourceMappingDTO>()
                  .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.Order.User.Name))
                  .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.Order.User.Phone))
                  .ForMember(dest => dest.CustomerAddress, opt => opt.MapFrom(src => src.Order.User.UserDetails.FirstOrDefault().Address.Address1))
                  .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Manager.Name))
                  .ForMember(dest => dest.VehicleType, opt => opt.MapFrom(src => src.Vehicle.VehicleType.Type))
                  .ForMember(dest => dest.AssignmentStatus, opt => opt.MapFrom(src => src.AssignmentStatus.Status))
                  .ForMember(dest => dest.VehicleNumber, opt => opt.MapFrom(src => src.Vehicle.VehicleNumber))
                  .ReverseMap();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static VehicleDTO MapVehicleToVehicleDTO(Vehicle vehicle)
        {
            if (vehicle == null)
            {
                throw new ArgumentNullException(nameof(vehicle));
            }

            return new VehicleDTO
            {
                Id = vehicle.Id,
                VehicleTypeId = vehicle.VehicleTypeId,
                VehicleType = vehicle.VehicleType?.Type,
                VehicleNumber = vehicle.VehicleNumber,
                WareHouseId = vehicle.WareHouseId,
                IsAvailable = vehicle.IsAvailable,
                IsActive = vehicle.IsActive,
                CreatedAt = vehicle.CreatedAt,
                UpdatedAt = vehicle.UpdatedAt
            };
        }
        public static Vehicle MapVehicleDTOToVehicle(VehicleDTO vehicleDTO)
        {
            if (vehicleDTO == null)
            {
                throw new ArgumentNullException(nameof(vehicleDTO));
            }

            return new Vehicle
            {
                Id = vehicleDTO.Id,
                VehicleTypeId = vehicleDTO.VehicleTypeId,
                VehicleNumber = vehicleDTO.VehicleNumber,
                WareHouseId = vehicleDTO.WareHouseId,
                IsAvailable = vehicleDTO.IsAvailable,
                IsActive = vehicleDTO.IsActive,
                CreatedAt = vehicleDTO.CreatedAt,
                UpdatedAt = vehicleDTO.UpdatedAt
            };
        }



        public static ResourceMapping MapResourceMappingDTOtoResouceMapping(ResourceMappingDTO assignment)
        {
            if (assignment == null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }
            return new ResourceMapping
            {
                Id = assignment.Id,
                ManagerId = assignment.ManagerId,
                VehicleId = assignment.VehicleId,
                DriverId = assignment.DriverId,
                OrderId = assignment.OrderId,
                AssignedDate = assignment.AssignedDate,
                AssignmentStatusId = assignment.AssignmentStatusId
            };
        }

        public static ResourceMappingDTO MapResourceMappingtoResouceMappingDTO(ResourceMapping assignment)
        {
            if (assignment == null)
            {
                throw new ArgumentNullException(nameof(assignment));
            }
            return new ResourceMappingDTO
            {
                Id = assignment.Id,
                ManagerId = assignment.ManagerId,
                VehicleId = assignment.VehicleId,
                DriverId = assignment.DriverId,
                OrderId = assignment.OrderId,
                AssignedDate = assignment.AssignedDate,
                AssignmentStatusId = assignment.AssignmentStatusId
            };
        }

    }
}
