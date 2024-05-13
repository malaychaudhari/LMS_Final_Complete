using LogisticsManagement.Service.DTOs;
using System.Net;

namespace LogisticsManagement.WebAPI.Utilities
{
    public static class ApiResponseHelper
    {
        public static APIResponseDTO Response(bool? status, HttpStatusCode? statusCode, dynamic? data = null, string error = "")
        {
            return new APIResponseDTO
            {
                Success = status,
                StatusCode = statusCode,
                Data = data,
                Error = error
            };
        }
    }
}
