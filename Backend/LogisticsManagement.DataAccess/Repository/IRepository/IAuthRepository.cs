using LogisticsManagement.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.DataAccess.Repository.IRepository
{
    public interface IAuthRepository
    {
        Task<int> AddUser(User user, UserDetail userDetail); // Adding user to database
         Task<User?> GetUserByEmailId(string emailid);//Get user details by email id

    }
}
