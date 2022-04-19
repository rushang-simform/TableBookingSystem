using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using TableBookingSystem.Application.Repository;

namespace TableBookingSystem.Application.Features.RestaurantCompany.Commands
{
    public class DeleteResturantCompanyCommand : IRequest
    {
        public Guid RestaurantCompanyId { get; set; }
    }

    public class DeleteResturantCompanyCommandHandler : IRequestHandler<DeleteResturantCompanyCommand>
    {
        private readonly ILogger<DeleteResturantCompanyCommandHandler> _logger;
        private readonly IRestaurantCompanyRepository _restaurantCompanyRepository;

        public DeleteResturantCompanyCommandHandler(ILogger<DeleteResturantCompanyCommandHandler> logger,IRestaurantCompanyRepository restaurantCompanyRepository)
        {
            _logger = logger;
            _restaurantCompanyRepository = restaurantCompanyRepository;
        }
        public async Task<Unit> Handle(DeleteResturantCompanyCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _restaurantCompanyRepository.DeleteAsync(request.RestaurantCompanyId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete restaurant company");
            }

            return Unit.Value;
        }
    }
}
