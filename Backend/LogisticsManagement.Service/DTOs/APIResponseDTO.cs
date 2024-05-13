using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.DTOs
{
    public class APIResponseDTO
    {
        public bool? Success { get; set; } = true;
        public HttpStatusCode? StatusCode { get; set; }
        public dynamic? Data { get; set; }
        public string? Error { get; set; }

    }
}
