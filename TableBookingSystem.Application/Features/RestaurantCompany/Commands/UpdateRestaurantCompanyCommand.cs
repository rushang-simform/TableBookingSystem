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

namespace TableBookingSystem.Application.Features.RestaurantCompany.Commands
{
    public class UpdateRestaurantCompanyCommand : IRequest<GenericResponse<RestaurantCompanyDto>>
    {
        public Guid RestaurantCompanyId { get; set; }
        public string RestaurantCompanyName { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public Guid UpdatedBy { get; set; }
    }
    public class UpdateRestaurantCompanyCommandHandler : IRequestHandler<UpdateRestaurantCompanyCommand, GenericResponse<RestaurantCompanyDto>>
    {
        private readonly ILogger<UpdateRestaurantCompanyCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantCompanyRepository _restaurantCompanyRepository;

        public UpdateRestaurantCompanyCommandHandler(ILogger<UpdateRestaurantCompanyCommandHandler> logger, IMapper mapper, IRestaurantCompanyRepository restaurantCompanyRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantCompanyRepository = restaurantCompanyRepository;
        }
        public async Task<GenericResponse<RestaurantCompanyDto>> Handle(UpdateRestaurantCompanyCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<RestaurantCompanyDto>() { IsSuccess = true };

            try
            {
                var restCompany = await _restaurantCompanyRepository.UpdateAsync(new Entities.Domain.RestaurantCompany.RestaurantCompany()
                {
                    RestaurantCompanyId = request.RestaurantCompanyId,
                    RestaurantCompanyName = request.RestaurantCompanyName,
                    Description = request.Description,
                    Phone = request.Phone,
                    Website = request.Website,
                    UpdatedBy = request.UpdatedBy,
                    UpdatedDate = DateTime.UtcNow
                });
                response.Data = _mapper.Map<RestaurantCompanyDto>(restCompany);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to update restaurant company");
                response.IsSuccess = false;
            }

            return response;
        }
    }

}
