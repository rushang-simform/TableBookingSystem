using TableBookingSystem.Application.Repository;
using TableBookingSystem.Domain.Entities.User;
using TableBookingSystem.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using TableBookingSystem.Application.Extensions;

namespace TableBookingSystem.Persistence.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataProvider _dataProvider;
        public UserRepository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }


        #region Repository Implementation

        public UserInfo GetById(Guid id)
        {
            string sql = @"SELECT ui.UserId
                               , ui.EmailId
                               , ui.UserRoleId
                               , ui.FirstName
                               , ui.LastName
                               , ui.PasswordHash
                               , ui.PasswordSalt
                               , ui.IsActive
                               , ui.CreatedBy
                               , ui.CreatedDate
                               , ui.UpdatedBy
                               , ui.UpdatedDate
                            FROM UserInfo AS ui
                            WHERE ui.UserId = @UserId;";

            var user = _dataProvider.Connection.QueryFirstOrDefault<UserInfo>(sql, new { UserId = id });
            return user;
        }

        public async Task<UserInfo> GetByIdAsync(Guid id)
        {
            var result = await Task.Run<UserInfo>(() =>
              {
                  return GetById(id);
              });
            return result;
        }

        public IList<UserInfo> GetByIds(IList<Guid> ids)
        {
            string sql = @"SELECT ui.UserId
                               , ui.EmailId
                               , ui.UserRoleId
                               , ui.FirstName
                               , ui.LastName
                               , ui.PasswordHash
                               , ui.PasswordSalt
                               , ui.IsActive
                               , ui.CreatedBy
                               , ui.CreatedDate
                               , ui.UpdatedBy
                               , ui.UpdatedDate
                            FROM UserInfo AS ui
                            WHERE ui.UserId IN (@UserIds)";

            
            var users = _dataProvider.Connection.Query<UserInfo>(sql, new { UserIds = ids.ToCommaSeparatedString(true) });
            return users.ToList();
        }

        public async Task<IList<UserInfo>> GetByIdsAsync(IList<Guid> ids)
        {
            var result = await Task.Run(() =>
               {
                   return GetByIds(ids);
               });
            return result;
        }

        public UserInfo Insert(UserInfo entity)
        {
            return _dataProvider.Connection.QueryFirstOrDefault<UserInfo>("usp_InsertUserInfo", new
            {
                EmailId = entity.EmailId,
                UserRoleId = entity.UserRoleId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PasswordHash = entity.PasswordHash,
                PasswordSalt = entity.PasswordSalt,
                IsActive = entity.IsActive,
                CreatorOrUpdator = entity.UpdatedBy,
                CreatedOrUpdatedDate = entity.CreatedDate,
            }, commandType: System.Data.CommandType.StoredProcedure);

        }

        public async Task<UserInfo> InsertAsync(UserInfo entity)
        {
            var result = await Task.Run<UserInfo>(() =>
              {
                  return Insert(entity);
              });
            return result;
        }


        public UserInfo Update(UserInfo entity)
        {
            return _dataProvider.Connection.QueryFirstOrDefault<UserInfo>("usp_InsertUserInfo", new
            {
                UserId = entity.UserId,
                EmailId = entity.EmailId,
                UserRoleId = entity.UserRoleId,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PasswordHash = entity.PasswordHash,
                PasswordSalt = entity.PasswordSalt,
                IsActive = entity.IsActive,
                CreatorOrUpdator = entity.UpdatedBy,
                CreatedOrUpdatedDate = entity.CreatedDate,
            }, commandType: System.Data.CommandType.StoredProcedure);
        }

        public async Task<UserInfo> UpdateAsync(UserInfo entity)
        {
            var result = await Task.Run<UserInfo>(() =>
            {
                return Update(entity);
            });
            return result;
        }

        public void Delete(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(UserInfo entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        #endregion

        public Guid GetSystemUserId()
        {
            return _dataProvider.Connection.ExecuteScalar<Guid>("SELECT UserId FROM UserInfo WHERE UserRoleId = 1");
        }

        public async Task<UserInfo> GetByEmailId(string emailId)
        {
            string sql = @"SELECT ui.UserId
                               , ui.EmailId
                               , ui.UserRoleId
                               , ui.FirstName
                               , ui.LastName
                               , ui.PasswordHash
                               , ui.PasswordSalt
                               , ui.IsActive
                               , ui.CreatedBy
                               , ui.CreatedDate
                               , ui.UpdatedBy
                               , ui.UpdatedDate
                            FROM UserInfo AS ui
                            WHERE ui.EmailId = @EmailId;";

            var user = await _dataProvider.Connection.QueryFirstOrDefaultAsync<UserInfo>(sql, new { EmailId = emailId });
            return user;
        }

        public bool CheckUserExists(string emailId)
        {
            string sql = @"SELECT CASE WHEN EXISTS (SELECT 1 
                         FROM UserInfo AS ui 
                         WHERE ui.EmailId = @EmailId)
                        THEN CAST (1 AS BIT) 
                        ELSE CAST (0 AS BIT) END;";
            return _dataProvider.Connection.ExecuteScalar<bool>(sql, new { EmailId = emailId });
        }

        public int GetUserRoleById(string userId)
        {
            string sql = @"SELECT ui.UserRoleId
                            FROM UserInfo AS ui
                            WHERE ui.UserId = @UserId";

            return _dataProvider.Connection.ExecuteScalar<int>(sql, new { UserId = userId });
        }
    }
}
