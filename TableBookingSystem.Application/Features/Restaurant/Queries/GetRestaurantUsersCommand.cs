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

namespace TableBookingSystem.Application.Features.Restaurant.Queries
{
    public class GetRestaurantUsersCommand : IRequest<GenericQueryResponse<List<Guid>>>
    {
        public Guid RestaurantId { get; set; }
        public GetRestaurantUsersCommand()
        {

        }
        public GetRestaurantUsersCommand(Guid restaurantId)
        {
            this.RestaurantId = restaurantId;
        }
    }
    public class GetRestaurantUsersCommandHander : IRequestHandler<GetRestaurantUsersCommand, GenericQueryResponse<List<Guid>>>
    {
        private readonly ILogger<GetRestaurantUsersCommandHander> _logger;
        private readonly IRestaurantRepository _restaurantRepository;

        public GetRestaurantUsersCommandHander(ILogger<GetRestaurantUsersCommandHander> logger, IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<GenericQueryResponse<List<Guid>>> Handle(GetRestaurantUsersCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericQueryResponse<List<Guid>>();
            try
            {
                if (_restaurantRepository.CheckRestaurantExists(request.RestaurantId))
                {
                    response.IsSuccess = true;
                    response.Status = QueryReponseStatus.Found;
                    response.Data = await _restaurantRepository.GetAssignedUsers(request.RestaurantId);
                }
                else
                {
                    response.IsSuccess = true;
                    response.Status = QueryReponseStatus.NotFound;
                    throw new MessageException("Invalid Restaurant Id");
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed to get data";
                _logger.LogError(ex, "Failed to get data");
            }
            return response;
        }
    }
}
