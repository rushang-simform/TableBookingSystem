using MediatR;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.User;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.Features.User.Queries
{
    public class GetAuthUserInfoQuery : IRequest<GenericResponse<AuthUserInfoDto>>
    {
        public string EmailId { get; set; }
    }
    public class GetAuthUserInfoQueryHandler : IRequestHandler<GetAuthUserInfoQuery, GenericResponse<AuthUserInfoDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public GetAuthUserInfoQueryHandler(IUserRepository userRepository, IRestaurantRepository restaurantRepository)
        {
            _userRepository = userRepository;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<GenericResponse<AuthUserInfoDto>> Handle(GetAuthUserInfoQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<AuthUserInfoDto>
            {
                IsSuccess = true,
            };
            if (string.IsNullOrEmpty(request.EmailId))
            {
                throw new ArgumentException("Invalid Email Id");
            }

            var userInfo = await _userRepository.GetByEmailId(request.EmailId);

            if (userInfo != null)
            {
                Guid? resturantId = null;

                if (userInfo.UserRoleId == (int)UserRole.RestaurantAdmin)
                {
                    resturantId = _restaurantRepository.GetRestaurantIdFromUserId(userInfo.UserId);
                }

                response.Data = new AuthUserInfoDto()
                {
                    UserId = userInfo.UserId,
                    EmailId = userInfo.EmailId,
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    ResturantId = resturantId,
                    IsActive = userInfo.IsActive,
                    PasswordHash = userInfo.PasswordHash,
                    PasswordSalt = userInfo.PasswordSalt
                };
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Invalid user";
            }

            return response;

        }
    }
}
