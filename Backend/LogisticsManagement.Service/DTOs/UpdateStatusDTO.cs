using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class UpdateStatusDTO
    {
        public int orderId {  get; set; }
        public int orderStatusId { get; set; }
    }
}
