using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Domain.Exceptions.Common;

namespace TableBookingSystem.Application.Features.Restaurant.Commands
{
    public class AssignUsersCommand : IRequest<PureResponse>
    {
        public Guid RestaurantId { get; set; }
        public List<Guid> UserIds { get; set; }
        public Guid AssignedBy { get; set; }
        public AssignUsersCommand() { }
        public AssignUsersCommand(Guid restaurantId, List<Guid> userIds, Guid assignedBy)
        {
            this.RestaurantId = restaurantId;
            this.UserIds = userIds;
            this.AssignedBy = assignedBy;
        }
    }

    public class AssignUsersCommandHandler : IRequestHandler<AssignUsersCommand, PureResponse>
    {
        private readonly ILogger<AssignUsersCommandHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;

        public AssignUsersCommandHandler(ILogger<AssignUsersCommandHandler> logger, IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
        }

        public async Task<PureResponse> Handle(AssignUsersCommand request, CancellationToken cancellationToken)
        {
            var response = new PureResponse() { IsSuccess = true };

            if (_restaurantRepository.CheckRestaurantExists(request.RestaurantId))
            {
                try
                {
                    await _restaurantRepository.AssignUsersAsync(request.RestaurantId, request.UserIds, request.AssignedBy);
                }
                catch (Exception ex) when (ex.Message.Contains(ExceptionMessages.USER_ALREADY_ASSIGNED))
                {
                    response.IsSuccess = false;
                    response.Message = "One or more users in the list is already assigned to resturant";
                }
                catch (Exception ex) when (ex.Message.Contains(ExceptionMessages.INVALID_USERID_IN_STRING))
                {
                    response.IsSuccess = false;
                    response.Message = "One or more users in the list is invalid";
                }
                catch (Exception ex)
                {
                    response.IsSuccess = false;
                    response.Message = "Failed to assign users";
                    _logger.LogError(ex, "Failed to assign users");
                }
            }
            else
            {
                response.IsSuccess = false;
                response.Message = "Restaurant doesn't exists";
            }

            return response;
        }
    }
}
