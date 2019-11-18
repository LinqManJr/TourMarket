using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TourMarket.Dto;
using TourMarket.Models;
using TourMarket.Services;

namespace TourMarket.Helpers
{
    public static class Extensions
    {
        public static Manager ToManager(this ManagerDto managerDto)
        {
            return new Manager { Id = managerDto.Id, Login = managerDto.Login, Name = managerDto.Name };
        }

        public static Order ToOrder(this OrderDto orderDto)
        {
            return new Order { Id = orderDto.Id, 
                                Date = orderDto.Date, 
                                Tour = orderDto.Tour, 
                                OrderManagers = new List<OrderManager> { new OrderManager { OrderId = orderDto.Id, Manager = orderDto.Manager } },
                                OrderTourists = new List<OrderTourist> { new OrderTourist { OrderId = orderDto.Id, Tourist = orderDto.Tourist} }
            };
        }

        public static void AddMyAuthentication(this IServiceCollection services, byte[] key)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {                        
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();                                             
                        
                        var manager = context.Principal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier);
                        if (manager == null)
                            context.Fail("Unauthorized");

                        var user = userService.GetById(int.Parse(manager.Value));
                        if (user == null)
                        {
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    },
                    OnAuthenticationFailed = context =>
                    {
                        context.Fail("Unauthorized");
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
