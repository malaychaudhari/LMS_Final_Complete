using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class InventoryDTO
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        [MaxLength(50)]
        public string Name { get; set; } 

        public string? Image { get; set; }

        public string? Description { get; set; }

        [Required]
        [Range(0, 5000,ErrorMessage = "stock should be between 0 to 500")]
        public int Stock { get; set; }

        [Required]
        [Range(0,int.MaxValue)]
        public decimal Price { get; set; }

        public int CategoryId { get; set; }

   
        public string? CategoryName { get; set; }

        public int WarehouseId { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

    }
}
