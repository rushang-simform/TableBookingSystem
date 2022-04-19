using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TableBookingSystem.Application.DTOs.Response;
using TableBookingSystem.Application.DTOs.RestaurantCompany;
using TableBookingSystem.Application.Repository;
using TableBookingSystem.Entities.Domain.RestaurantCompany;

namespace TableBookingSystem.Application.Features.RestaurantCompany.Queries
{
    public class GetAllRestaurantCompanies : IRequest<GenericQueryResponse<List<RestaurantCompanyDto>>>
    {
    }
    public class GetAllRestaurantCompaniesHandler : IRequestHandler<GetAllRestaurantCompanies, GenericQueryResponse<List<RestaurantCompanyDto>>>
    {
        private readonly ILogger<GetAllRestaurantCompaniesHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantCompanyRepository _restaurantCompanyRepository;

        public GetAllRestaurantCompaniesHandler(ILogger<GetAllRestaurantCompaniesHandler> logger, IMapper mapper, IRestaurantCompanyRepository restaurantCompanyRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantCompanyRepository = restaurantCompanyRepository;
        }
        public async Task<GenericQueryResponse<List<RestaurantCompanyDto>>> Handle(GetAllRestaurantCompanies request, CancellationToken cancellationToken)
        {
            var response = new GenericQueryResponse<List<RestaurantCompanyDto>>() { IsSuccess = true };

            try
            {
                var result = await _restaurantCompanyRepository.GetAll();
                response.Data = _mapper.Map<List<RestaurantCompanyDto>>(result);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
            }

            return response;
        }
    }
}
