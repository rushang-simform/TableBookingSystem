using AutoMapper;
using MediatR;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.User;
using TableBookingSystem.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.Features.User.Queries
{
    public class GetUserInfoQuery : IRequest<GenericResponse<BasicUserInfoDto>>
    {
        public string UserId { get; set; }
    }
    public class GetUserInfoQueryHandler : IRequestHandler<GetUserInfoQuery, GenericResponse<BasicUserInfoDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public GetUserInfoQueryHandler(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        public async Task<GenericResponse<BasicUserInfoDto>> Handle(GetUserInfoQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<BasicUserInfoDto>() { IsSuccess = false };

            if (string.IsNullOrEmpty(request.UserId))
            {
                response.Message = "Invalid User Id";
                return response;
            }
            var userId = Guid.Parse(request.UserId);

            var userInfo = await _userRepository.GetByIdAsync(userId);

            if (userInfo == null)
            {
                response.IsSuccess = false;
                response.Message = "User not found";
            }
            else
            {
                response.IsSuccess = true;
                response.Data = _mapper.Map<BasicUserInfoDto>(userInfo);
            }
            return response;
        }
    }
}

