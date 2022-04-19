using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Domain.Exceptions.DB;

namespace TableBookingSystem.Application.Features.User.Queries
{
    public class CheckUserHasRestaurantAccess : IRequest<bool>
    {
        public Guid RestaurantId { get; set; }
        public Guid UserId { get; set; }
        public CheckUserHasRestaurantAccess(Guid restaurantId, Guid userId)
        {
            this.RestaurantId = restaurantId;
            this.UserId = userId;
        }
    }
    public class CheckUserHasRestaurantAccessHandler : IRequestHandler<CheckUserHasRestaurantAccess, bool>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public CheckUserHasRestaurantAccessHandler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }
        public async Task<bool> Handle(CheckUserHasRestaurantAccess request, CancellationToken cancellationToken)
        {
            try
            {
                if (_restaurantRepository.CheckRestaurantExists(request.RestaurantId))
                {
                    var result = await _restaurantRepository.CheckUserHasAccessOfRestaurant(request.RestaurantId, request.UserId);
                    return result;
                }
                else
                {
                    throw new ResourceNotFoundException("Restaurant not found");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
