using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Domain.Enums;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace TableBookingSystem.API.Authorization
{
    public class UserRoleAuthorizationHandler : AuthorizationHandler<RolesAuthorizationRequirement>, IAuthorizationHandler
    {
        private readonly IUserRepository _userRepository;

        public UserRoleAuthorizationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, RolesAuthorizationRequirement requirement)
        {

            if (context.User == null)
            {
                return;
            }

            bool hasPermission = false;

            var userId = context.User.FindFirst("UserId");

            if (userId != null)
            {
                var userRole = await Task.FromResult(_userRepository.GetUserRoleById(userId.Value));

                var role= (UserRole)userRole;

                hasPermission = requirement.AllowedRoles.Any(x => x.Contains(Enum.GetName(role)));

                if (hasPermission)
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
        }
    }
}
