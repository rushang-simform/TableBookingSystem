using TableBookingSystem.Entities.Domain.RestaurantCompany;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TableBookingSystem.Application.Repository
{
    public interface IRestaurantCompanyRepository : IAsyncEntityRepository<RestaurantCompany, Guid>
    {
        Task<IEnumerable<RestaurantCompany>> GetAll();
    }
}
