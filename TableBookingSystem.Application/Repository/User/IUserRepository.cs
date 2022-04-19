using TableBookingSystem.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableBookingSystem.Application.Repository;

namespace TableBookingSystem.Application.Repository
{
    public interface IUserRepository : IAsyncEntityRepository<UserInfo, Guid>
    {
        Guid GetSystemUserId();
        Task<UserInfo> GetByEmailId(string emailId);
        bool CheckUserExists(string emailId);
        int GetUserRoleById(string userId);
    }
}
