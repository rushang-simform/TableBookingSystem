using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TableBookingSystem.Web.Authorization;
using TableBookingSystem.Application.ConfigurationClasses;
using TableBookingSystem.Application.Intefaces.Auth;
using TableBookingSystem.Application.Intefaces.Cryptography;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Application.ServiceExtension;
using TableBookingSystem.Persistence.Implementation;
using TableBookingSystem.Persistence.Interfaces;
using TableBookingSystem.Persistence.Mappings;
using TableBookingSystem.Persistence.Repository;
using TableBookingSystem.Services.Implementation;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Web.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterApplicatonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDatabase(configuration);
            services.RegisterAuthenticationServices(configuration);
            services.RegisterCustomServices(configuration);
            services.AddApplicationServices(configuration);
            
        }
        public static void ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
        {
            //Dapper Coversion
            MappingConfigurator.Initialize();
            var dbConfiguration = configuration.GetSection("DBConfiguration").Get<DBConfiguration>();
            services.AddScoped<IDataProvider>(x => new SQLServerDataProvider(dbConfiguration.GetConnectionString()));


            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRestaurantCompanyRepository, RestaurantCompanyRepository>();
            services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        }

        public static void RegisterAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAuthorizationHandler, UserRoleAuthorizationHandler>();
            var config = configuration.GetSection("JWTConfig").Get<JWTConfig>();
            services.Configure<JWTConfig>(x =>
            {
                x.ExpirationTimeout = config.ExpirationTimeout;
                x.EncryptionKey = config.EncryptionKey;
                x.Issuer = config.Issuer;
                x.Audience = config.Audience;
            });

            services
              .AddAuthentication(x =>
              {
                  x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              })
              .AddJwtBearer(x =>
              {
                  x.RequireHttpsMetadata = false;
                  x.SaveToken = true;
                  x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = config.Issuer,
                      ValidAudience = config.Audience,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.EncryptionKey)),
                  };
                  x.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          context.Token = context.Request.Cookies["AuthToken"];
                          return Task.CompletedTask;
                      },
                      OnTokenValidated = x =>
                      {
                          return Task.CompletedTask;
                      },
                      OnForbidden = x =>
                      {
                          return Task.CompletedTask;
                      },
                      OnAuthenticationFailed = x =>
                      {
                          return Task.CompletedTask;
                      }
                  };
              });

            services.AddAuthorization(config =>
            {
                var userAuthPolicyBuilder = new AuthorizationPolicyBuilder();
                config.DefaultPolicy = userAuthPolicyBuilder
                                    .RequireAuthenticatedUser()
                                    .RequireClaim("UserId")
                                    .Build();

                config.AddPolicy("UserRolePolicy", x => { x.RequireClaim("UserId"); });
                
            });
        }
        public static void RegisterCustomServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<ICryptographyService, CryptographyService>();
            services.AddTransient<IAccountService, AccountService>();
        }
    }
}
