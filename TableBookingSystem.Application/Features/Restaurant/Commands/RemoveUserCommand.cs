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

namespace TableBookingSystem.Application.Features.Restaurant.Commands
{
    public class RemoveUserCommand : IRequest<PureResponse>
    {
        public Guid RestaurantId { get; set; }
        public Guid UserId { get; set; }
        public RemoveUserCommand()
        {

        }
        public RemoveUserCommand(Guid restaurantId, Guid userId)
        {
            RestaurantId = restaurantId;
            UserId = userId;
        }
    }

    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, PureResponse>
    {
        private readonly ILogger<RemoveUserCommandHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;

        public RemoveUserCommandHandler(ILogger<RemoveUserCommandHandler> logger, IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<PureResponse> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {

            var response = new PureResponse() { IsSuccess = true };
            try
            {
                await Task.Run(() => _restaurantRepository.RemoveUsers(request.RestaurantId, request.UserId));
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
