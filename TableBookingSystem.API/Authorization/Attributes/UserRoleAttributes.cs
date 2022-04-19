using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using TableBookingSystem.API.Util.Enums;
using MediatR;
using TableBookingSystem.Application.Features.User.Queries;
using System;
using Microsoft.AspNetCore.Mvc;

namespace TableBookingSystem.API.Authorization.Attributes
{
    public class SuperAdminRole : AuthorizeAttribute
    {
        public SuperAdminRole()
        {
            Policy = "UserRolePolicy";
            Roles = "SuperAdmin";
        }
    }
    public class AdminRole : AuthorizeAttribute
    {
        public AdminRole()
        {
            Policy = "UserRolePolicy";
            Roles = "Admin";
        }
    }

    public class RestaurantAdmin : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string _param;
        private readonly ParamLocation _paramLocation;
        private readonly bool _validateParmas = false;

        public RestaurantAdmin()
        {
            Policy = "UserRolePolicy";
            Roles = "RestaurantAdmin";
        }
        public RestaurantAdmin(string param, ParamLocation paramLocation)
        {
            _validateParmas = true;
            _param = param;
            _paramLocation = paramLocation;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var mediator = context.HttpContext.RequestServices.GetService(typeof(IMediator)) as IMediator;
            string userId = context.HttpContext.User.FindFirst("UserId")?.Value;
            string restaurantId = context.HttpContext.User.FindFirst("RestaurantId")?.Value;

            if (!string.IsNullOrEmpty(userId) && !string.IsNullOrEmpty(restaurantId))
            {
                var hasAccess = mediator.Send<bool>(new CheckUserHasRestaurantAccess(Guid.Parse(restaurantId), Guid.Parse(userId)))
                    .GetAwaiter().GetResult();

                if (!hasAccess)
                {
                    context.Result = new ForbidResult();
                }
            }
            else
            {
                context.Result = new ForbidResult();
            }
        }
    }
}
