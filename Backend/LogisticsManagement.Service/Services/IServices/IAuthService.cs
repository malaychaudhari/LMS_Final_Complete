using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.Service.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.Services.IServices
{
    public interface IAuthService
    {
        Task<int> SignUpAsync(UserDTO user);
        Task<LoginResponseDTO> LoginAsync(LoginDTO login);
        string GenerateHashPassword(string password);
        string GenerateToken(User user);

        Task<UserDTO> GetUserByIdAsync(int id);
    }
}
