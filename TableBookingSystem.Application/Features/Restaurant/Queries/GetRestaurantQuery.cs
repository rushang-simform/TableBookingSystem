using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.Restaurant;
using TableBookingSystem.Application.Repository;

namespace TableBookingSystem.Application.Features.Restaurant.Queries
{
    public class GetRestaurantQuery : IRequest<GenericQueryResponse<RestaurantDto>>
    {
        public Guid RestaurantId { get; set; }
    }

    public class GetRestaurantQueryHandler : IRequestHandler<GetRestaurantQuery, GenericQueryResponse<RestaurantDto>>
    {
        private readonly ILogger<GetRestaurantQueryHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;

        public GetRestaurantQueryHandler(ILogger<GetRestaurantQueryHandler> logger, IMapper mapper, IRestaurantRepository restaurantRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<GenericQueryResponse<RestaurantDto>> Handle(GetRestaurantQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericQueryResponse<RestaurantDto>() { IsSuccess = true };
            try
            {
                var restaurant = await _restaurantRepository.GetByIdAsync(request.RestaurantId);

                if (restaurant == null)
                {
                    response.Status = QueryReponseStatus.NotFound;
                }
                else
                {
                    response.Status = QueryReponseStatus.Found;
                    response.Data = _mapper.Map<RestaurantDto>(restaurant);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in query Restaurant");
                response.IsSuccess = false;
                response.Message = "Failed to get Restaurant";
            }
            return response;
        }
    }

}

