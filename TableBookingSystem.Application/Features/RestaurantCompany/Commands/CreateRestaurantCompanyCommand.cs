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
using TableBookingSystem.Entities.Domain.RestaurantCompany;


namespace TableBookingSystem.Application.Features.RestaurantCompany.Commands
{
    public class CreateRestaurantCompanyCommand : IRequest<GenericResponse<RestaurantCompanyDto>>
    {
        public string RestaurantCompanyName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public Guid CreatedBy { get; set; }
    }
    public class CreateRestaurantCompanyCommandHandler : IRequestHandler<CreateRestaurantCompanyCommand, GenericResponse<RestaurantCompanyDto>>
    {
        private readonly ILogger<CreateRestaurantCompanyCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantCompanyRepository _restaurantCompanyRepository;

        public CreateRestaurantCompanyCommandHandler(ILogger<CreateRestaurantCompanyCommandHandler> logger, IMapper mapper, IRestaurantCompanyRepository restaurantCompanyRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantCompanyRepository = restaurantCompanyRepository;
        }
        public async Task<GenericResponse<RestaurantCompanyDto>> Handle(CreateRestaurantCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<RestaurantCompanyDto>() { IsSuccess = true };
            try
            {
                var retVal = await _restaurantCompanyRepository.InsertAsync(new Entities.Domain.RestaurantCompany.RestaurantCompany()
                {
                    RestaurantCompanyName = request.RestaurantCompanyName,
                    Description = request.Description,
                    Website = request.Website,
                    Phone = request.Phone,
                    CreatedBy = request.CreatedBy,
                    CreatedDate = DateTime.UtcNow,
                    UpdatedDate = DateTime.UtcNow,
                    UpdatedBy = request.CreatedBy
                });
                response.Data = _mapper.Map<RestaurantCompanyDto>(retVal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in create Restaurant Company");
                response.IsSuccess = false;
                response.Message = "Failed to create restaurant company";
            }

            return response;
        }
    }
}
