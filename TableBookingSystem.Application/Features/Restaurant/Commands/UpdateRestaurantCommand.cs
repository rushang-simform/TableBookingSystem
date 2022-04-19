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

namespace TableBookingSystem.Application.Features.Restaurant.Commands
{
    public class UpdateRestaurantCommand : IRequest<GenericResponse<RestaurantDto>>
    {
        public Guid RestaurantId { get; set; }
        public string RestaurantName { get; set; }
        public Guid RestaurantCompanyId { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string StreetAddress { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public TimeSpan OpeningTime { get; set; }
        public TimeSpan ClosingTime { get; set; }
        public Decimal? Latitude { get; set; }
        public Decimal? Longitude { get; set; }
        public Guid UpdatedBy { get; set; }
    }

    public class UpdateRestaurantCommandHandler : IRequestHandler<UpdateRestaurantCommand, GenericResponse<RestaurantDto>>
    {
        private readonly ILogger<UpdateRestaurantCommandHandler> _logger;
        private readonly IMapper _mapper;
        private readonly IRestaurantRepository _restaurantRepository;

        public UpdateRestaurantCommandHandler
        (
            ILogger<UpdateRestaurantCommandHandler> logger,
            IMapper mapper,
            IRestaurantRepository restaurantRepository
        )
        {
            _logger = logger;
            _mapper = mapper;
            _restaurantRepository = restaurantRepository;
        }
        public async Task<GenericResponse<RestaurantDto>> Handle(UpdateRestaurantCommand request, CancellationToken cancellationToken)
        {
            var response = new GenericResponse<RestaurantDto>() { IsSuccess = true };
            try
            {
                var restaurant = await _restaurantRepository.UpdateAsync(new Domain.Entities.Restaurant.Restaurant()
                {
                    RestaurantId = request.RestaurantId,
                    RestaurantCompanyId=request.RestaurantCompanyId,
                    RestaurantName = request.RestaurantName,
                    Description = request.Description,
                    Phone = request.Phone,
                    Website = request.Website,
                    StreetAddress = request.StreetAddress,
                    State = request.State,
                    Country = request.Country,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    IsDeleted = false,
                    OpeningTime = request.OpeningTime,
                    ClosingTime = request.ClosingTime,
                    UpdatedBy = request.UpdatedBy,
                    UpdatedDate = DateTime.UtcNow
                });

                response.Data = _mapper.Map<RestaurantDto>(restaurant);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Failed to update Restaurant";
                _logger.LogError(ex, "Error in creating records");
            }
            return response;
        }
    }
}
