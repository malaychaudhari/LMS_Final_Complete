
using LogisticsManagement.DataAccess.Models;
using LogisticsManagement.DataAccess.Repository;
using LogisticsManagement.DataAccess.Repository.IRepository;
using LogisticsManagement.Service.Convertors;
using LogisticsManagement.Service.DTOs;
using LogisticsManagement.Service.Services;
using LogisticsManagement.Service.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LogisticsManagement.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers().AddNewtonsoftJson();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var configuration = builder.Configuration;

            builder.Services.AddDbContext<LogisticsManagementContext>(option => option.UseSqlServer(configuration.GetConnectionString("localConnectionString")));
           
            builder.Services.AddAutoMapper(typeof(ApplicationMapper));


            builder.Services.AddScoped<IAuthService,AuthService>();
            builder.Services.AddScoped<IAuthRepository, AuthRepository>();

            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
            builder.Services.AddScoped<ICustomerService, CustomerService>();

            builder.Services.AddScoped<IManagerRepository, ManagerRepository>();
            builder.Services.AddScoped<IManagerService, ManagerService>();


            builder.Services.AddScoped<IDriverRepository, DriverRepository>();
            builder.Services.AddScoped<IDriverService, DriverService>();

            //JWT Configuration
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata=false;
                options.SaveToken=true;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetValue<string>("JWTSecret"))),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTIssuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWTAudience"],

                    ClockSkew= TimeSpan.Zero
                };
            });


            builder.Services.AddCors(options =>
            {
                options.AddPolicy("corsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
                });
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("corsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
