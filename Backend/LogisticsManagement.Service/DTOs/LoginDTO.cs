using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string? EmailId { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
