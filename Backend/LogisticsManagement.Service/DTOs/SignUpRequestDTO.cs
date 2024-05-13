using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class SignUpRequestDTO
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int Status {  get; set; }
    }
}
