using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.DTOs;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.User;
using TableBookingSystem.Application.Intefaces.Cryptography;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Domain.Entities.User;
using TableBookingSystem.Domain.Enums;

namespace TableBookingSystem.Application.Features.User.Commands
{
    public class CreateUserCommand : IRequest<GenericResponse<BasicUserInfoDto>>
    {
        public string EmailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; } = (int)Domain.Enums.UserRole.Customer;
        public string CreatedOrModifiedBy { get; set; }
    }
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, GenericResponse<BasicUserInfoDto>>
    {
        private readonly ILogger<CreateUserHandler> _logger;
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;
        private readonly IMapper _mapper;

        public CreateUserHandler
        (
            ILogger<CreateUserHandler> logger,
            IUserRepository userRepository,
            ICryptographyService cryptographyService,
            IMapper mapper
        )
        {
            _logger = logger;
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
            _mapper = mapper;
        }
        public async Task<GenericResponse<BasicUserInfoDto>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<BasicUserInfoDto>() { IsSuccess = false };

            try
            {
                Guid createdOrModifiedBy = string.IsNullOrEmpty(request.CreatedOrModifiedBy) 
                                                    ? _userRepository.GetSystemUserId() 
                                                    : Guid.Parse(request.CreatedOrModifiedBy);

                (string encryptedPassword, string salt) = _cryptographyService.EncryptPassword(request.Password);

                var userInfo = new UserInfo()
                {
                    EmailId = request.EmailId,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    IsActive = true,
                    PasswordHash = encryptedPassword,
                    PasswordSalt = salt,
                    UserRoleId = request.UserType,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = createdOrModifiedBy,
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = createdOrModifiedBy
                };

                UserInfo user = await _userRepository.InsertAsync(userInfo);
                var retVal = _mapper.Map<BasicUserInfoDto>(user);

                response.IsSuccess = true;
                response.Data = retVal;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "User creation error");
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
