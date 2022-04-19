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
using TableBookingSystem.Application.DTOs.RestaurantCompany;
using TableBookingSystem.Application.Repository;

namespace TableBookingSystem.Application.Features.RestaurantCompany.Queries
{
    public class GetRestaurantCompanyQuery : IRequest<GenericQueryResponse<RestaurantCompanyDto>>
    {
        public Guid RestaurantCompanyId { get; set; }
    }

    public class GetRestaurantCompanyHanler : IRequestHandler<GetRestaurantCompanyQuery, GenericQueryResponse<RestaurantCompanyDto>>
    {
        private readonly ILogger<GetRestaurantCompanyHanler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantCompanyRepository _restaurantCompanyRepository;

        public GetRestaurantCompanyHanler(ILogger<GetRestaurantCompanyHanler> logger, IMapper mapper, IRestaurantCompanyRepository restaurantCompanyRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantCompanyRepository = restaurantCompanyRepository;
        }
        public async Task<GenericQueryResponse<RestaurantCompanyDto>> Handle(GetRestaurantCompanyQuery request, CancellationToken cancellationToken)
        {
            var response = new GenericQueryResponse<RestaurantCompanyDto>() { IsSuccess = true };
            try
            {
                var restCompany = await _restaurantCompanyRepository.GetByIdAsync(request.RestaurantCompanyId);
                if (restCompany != null)
                {
                    if (restCompany.IsDeleted)
                    {
                        response.Status = QueryReponseStatus.Deleted;
                    }
                    else
                    {
                        response.Data = _mapper.Map<RestaurantCompanyDto>(restCompany);
                        response.Status = QueryReponseStatus.Found;
                    }
                }
                else
                {
                    response.Status = QueryReponseStatus.NotFound;

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error to get restaurant company");
                response.IsSuccess = false;
                response.Message = "Failed to get resturant company";
            }

            return response;
        }
    }
}

