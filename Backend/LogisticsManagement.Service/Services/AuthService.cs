using AutoMapper;
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository.IRepository;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsManagement.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public AuthService(IAuthRepository authRepository, IAdminRepository adminRepository, IMapper mapper, IConfiguration configuration)
        {
            _authRepository = authRepository;
            _adminRepository = adminRepository;
            _mapper = mapper;
            _configuration = configuration;
        }


        // Sign Up
        public async Task<int> SignUpAsync(UserDTO user)
        {
            try
            {
                // check if user already exists
                if (await _authRepository.GetUserByEmailId(user.Email) is not null)
                {
                    return -1;
                }

                // Generate password hash
                string hashedPassword = GenerateHashPassword(user.Password);


                user.Email = user.Email.ToLower(); // convert email to lowecase
                user.Password = hashedPassword;  // set hashed password as password


                User newUser = _mapper.Map<User>(user);
                UserDetail newUserDetail = _mapper.Map<UserDetail>(user);
                newUserDetail.User = newUser;

                // Add user to Database
                return await _authRepository.AddUser(newUser, newUserDetail);

            }
            catch (Exception ex)
            {
                //Log Error
                Console.WriteLine("Error occurred while adding user" + ex.Message);

            }
            return 0;
        }


        //Login User
        public async Task<LoginResponseDTO> LoginAsync(LoginDTO login)
        {
            // get user by email id 
            User? user = await _authRepository.GetUserByEmailId(login.EmailId.ToLower());

            string hashedPassword = GenerateHashPassword(login.Password);
            // check if user exists and password is same as hashed password
            if (user is not null && user.Password == hashedPassword && user.IsActive== true)
            {
                int isApproved = (int)user.UserDetails.FirstOrDefault().IsApproved;

                // check if user is approved
                if (isApproved == 1)
                {
                    // generate token if user is approved
                    string token = GenerateToken(user);
                    return new LoginResponseDTO { Token = token, Message="Login Successful" };
                }
                else if (isApproved == -1)
                {
                    return new LoginResponseDTO {Message="Your sign up request has been rejected" };

                }
                else if (isApproved == 0)
                {
                    return new LoginResponseDTO { Message = "Your sign up approval request is Pending" };

                }
            }
            return new LoginResponseDTO { Message = "Invalid credentials" };
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            User? user = await _adminRepository.GetUserById(id);

            if(user is not null)
            {
                return _mapper.Map<UserDTO>(user);
            }
            return null;
        }


        //Generate Token
        public string GenerateToken(User user)
        {
            try
            {
                // create token handler instance
                var tokenHandler = new JwtSecurityTokenHandler();

                // get secret key from configuration
                var secretKey = Encoding.UTF8.GetBytes(_configuration.GetValue<string>("JWTSecret"));


                // create claims for token
                var claims = new List<Claim>()
                {
                    new Claim("Id",user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Name),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.Name)
                };

                // create token descriptor
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Issuer= _configuration["JWTIssuer"],
                    Audience=_configuration["JWTAudience"],
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha512Signature)
                };

                // create token from token descriptor
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // return token
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while generating token" + ex.Message);
            }
            return null;
        }


        //Generate Hash of password 
        public string GenerateHashPassword(string password)
        {
            try
            {
                using (SHA512 sha512 = SHA512.Create())
                {
                    //compute hash from password and return it as byte array
                    byte[] hashedBytes = sha512.ComputeHash(Encoding.UTF8.GetBytes(password));
                    StringBuilder builder = new StringBuilder();
                    
                    //convert byte array to string
                    foreach (byte b in hashedBytes)
                    {
                        builder.Append(b.ToString("x2"));
                    }

                    //return hashed password
                    return builder.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while Generating hash" + ex.Message);
                return null;
            }
        }


    }
}
