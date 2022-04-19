using TableBookingSystem.Domain.Entities.Restaurant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableBookingSystem.Application.Repository
{
    public interface IRestaurantRepository : IAsyncEntityRepository<Restaurant, Guid>
    {
        bool CheckRestaurantExists(Guid restaurantId, bool includeDeleted = false);
        Task<List<Guid>> GetAssignedUsers(Guid restaurantId);
        Task<List<Guid>> AssignUsersAsync(Guid restaurantId, List<Guid> users, Guid assignedBy);
        void RemoveUsers(Guid restaurantId, Guid userId);
        Task<bool> CheckUserHasAccessOfRestaurant(Guid restaurantId, Guid userId);
        Guid? GetRestaurantIdFromUserId(Guid userId);
        Task<List<Restaurant>> GetRestaurantsByCompany(Guid restaurantCompanyId);
    }
}
