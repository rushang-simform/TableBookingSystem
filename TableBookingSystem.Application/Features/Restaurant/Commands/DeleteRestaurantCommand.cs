using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.Repository;

namespace TableBookingSystem.Application.Features.Restaurant.Commands
{
    public class DeleteRestaurantCommand : IRequest
    {
        public Guid RestaurantId { get; set; }
    }

    public class DeleteRestaurantCommandHandler : IRequestHandler<DeleteRestaurantCommand>
    {
        private readonly ILogger<DeleteRestaurantCommandHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;

        public DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommandHandler> logger,IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<Unit> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _restaurantRepository.DeleteAsync(request.RestaurantId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete restaurant");
            }
            return Unit.Value;
        }
    }
}
