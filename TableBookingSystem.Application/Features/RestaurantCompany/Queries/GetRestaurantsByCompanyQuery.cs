using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.Restaurant;
using TableBookingSystem.Application.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.Features.RestaurantCompany.Queries
{
    public class GetRestaurantsByCompanyQuery : IRequest<GenericQueryResponse<List<RestaurantDto>>>
    {
        public Guid RestaurantCompanyId { get; set; }
        public GetRestaurantsByCompanyQuery(Guid restaurantCompanyId)
        {
            this.RestaurantCompanyId = restaurantCompanyId;
        }
    }
    public class GetRestaurantsByCompanyQueryHandler : IRequestHandler<GetRestaurantsByCompanyQuery, GenericQueryResponse<List<RestaurantDto>>>
    {
        private readonly IMapper _mapper;
        private readonly ILogger<GetRestaurantsByCompanyQueryHandler> _logger;
        private readonly IRestaurantRepository _restaurantRepository;

        public GetRestaurantsByCompanyQueryHandler(IMapper mapper, ILogger<GetRestaurantsByCompanyQueryHandler> logger, IRestaurantRepository restaurantRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<GenericQueryResponse<List<RestaurantDto>>> Handle(GetRestaurantsByCompanyQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericQueryResponse<List<RestaurantDto>>();
            try
            {
                var dbResponse = await _restaurantRepository.GetRestaurantsByCompany(request.RestaurantCompanyId);
                response.Data = _mapper.Map<List<RestaurantDto>>(dbResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in retriving restaurants");
                response.IsSuccess = false;
                response.Message = "Error in retriving restaurants";
            }

            return response;
        }
    }
}
