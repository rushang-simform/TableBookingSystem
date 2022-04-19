using TableBookingSystem.Application.Repository;
using TableBookingSystem.Domain.Entities.Restaurant;
using TableBookingSystem.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TableBookingSystem.Application.Extensions;

namespace TableBookingSystem.Persistence.Repository
{
    public class RestaurantRepository : IRestaurantRepository
    {
        private readonly IDataProvider _dataProvider;

        public RestaurantRepository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #region Repository Implementation

        #region Delete
        public void Delete(Restaurant entity)
        {
            Delete(entity.GetId());
        }

        public void Delete(Guid id)
        {
            string sql = @"UPDATE RestaurantMaster
                           SET IsDeleted = 1
                           WHERE RestaurantId = @RestaurantId";

            _dataProvider.Connection.Execute(sql, new { RestaurantId = id });
        }

        public async Task DeleteAsync(Restaurant entity)
        {
            await Task.Run(() => Delete(entity.GetId()));
        }

        public async Task DeleteAsync(Guid id)
        {
            await Task.Run(() => Delete(id));
        }

        #endregion

        #region Get

        public Restaurant GetById(Guid id)
        {
            string sql = @"	SELECT
		                        rm.RestaurantId
	                           ,rm.RestaurantCompanyId
	                           ,rm.RestaurantName
	                           ,rm.Description
	                           ,rm.Website
	                           ,rm.Phone
	                           ,rm.StreetAddress
	                           ,rm.State
	                           ,rm.Country
	                           ,rm.OpeningTime
	                           ,rm.ClosingTime
	                           ,rm.CurrentStatus
	                           ,rm.CreatedBy
	                           ,rm.CreatedDate
	                           ,rm.UpdatedBy
	                           ,rm.UpdatedDate
	                           ,rm.IsDeleted
	                           ,rm.Latitude
	                           ,rm.Longitude
	                        FROM RestaurantMaster rm
	                        WHERE rm.RestaurantId = @RestaurantId;";

            var retVal = _dataProvider.Connection.QueryFirstOrDefault<Restaurant>(sql, new
            {
                RestaurantId = id
            });

            return retVal;
        }

        public async Task<Restaurant> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(GetById(id));
        }

        public IList<Restaurant> GetByIds(IList<Guid> ids)
        {
            string sql = @"	SELECT
		                        rm.RestaurantId
	                           ,rm.RestaurantCompanyId
	                           ,rm.RestaurantName
	                           ,rm.Description
	                           ,rm.Website
	                           ,rm.Phone
	                           ,rm.StreetAddress
	                           ,rm.State
	                           ,rm.Country
	                           ,rm.OpeningTime
	                           ,rm.ClosingTime
	                           ,rm.CurrentStatus
	                           ,rm.CreatedBy
	                           ,rm.CreatedDate
	                           ,rm.UpdatedBy
	                           ,rm.UpdatedDate
	                           ,rm.IsDeleted
	                           ,rm.Latitude
	                           ,rm.Longitude
	                        FROM RestaurantMaster rm
	                        WHERE rm.RestaurantId IN (@RestaurantIds);";

            var retVal = _dataProvider.Connection.Query<Restaurant>(sql, new
            {
                RestaurantIds = ids.ToCommaSeparatedString(true)
            }).ToList();

            return retVal;
        }

        public async Task<IList<Restaurant>> GetByIdsAsync(IList<Guid> ids)
        {
            return await Task.FromResult(GetByIds(ids));
        }

        #endregion

        #region Insert
        public Restaurant Insert(Restaurant entity)
        {
            string sql = "usp_Restaurant_IU";
            var retVal = _dataProvider.Connection.QueryFirstOrDefault<Restaurant>(sql, new
            {
                RestaurantCompanyId = entity.RestaurantCompanyId,
                RestaurantName = entity.RestaurantName,
                Description = entity.Description,
                Website = entity.Website,
                Phone = entity.Phone,
                StreetAddress = entity.StreetAddress,
                State = entity.State,
                Country = entity.Country,
                OpeningTime = entity.OpeningTime,
                ClosingTime = entity.ClosingTime,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                CreatedOrUpdatedBy = entity.CreatedBy,
                CreatedOrUpdatedDate = entity.CreatedDate
            }, commandType: System.Data.CommandType.StoredProcedure);

            return retVal;
        }

        public async Task<Restaurant> InsertAsync(Restaurant entity)
        {
            return await Task.FromResult(Insert(entity));
        }


        #endregion

        #region Update
        public Restaurant Update(Restaurant entity)
        {
            string sql = "usp_Restaurant_IU";
            var retVal = _dataProvider.Connection.QueryFirstOrDefault<Restaurant>(sql, new
            {
                RestaurantId = entity.RestaurantId,
                RestaurantCompanyId = entity.RestaurantCompanyId,
                RestaurantName = entity.RestaurantName,
                Description = entity.Description,
                Website = entity.Website,
                Phone = entity.Phone,
                StreetAddress = entity.StreetAddress,
                State = entity.State,
                Country = entity.Country,
                OpeningTime = entity.OpeningTime,
                ClosingTime = entity.ClosingTime,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                CreatedOrUpdatedBy = entity.UpdatedBy,
                CreatedOrUpdatedDate = entity.UpdatedDate
            }, commandType: System.Data.CommandType.StoredProcedure);

            return retVal;
        }

        public async Task<Restaurant> UpdateAsync(Restaurant entity)
        {
            return await Task.FromResult<Restaurant>(Update(entity));
        }
        #endregion

        #endregion


        public async Task<List<Guid>> GetAssignedUsers(Guid restaurantId)
        {
            string sql = @"SELECT rum.UserId FROM RestaurantUserMapping rum
                            WHERE rum.RestaurantId = @RestaurantId";

            return (await _dataProvider.Connection.QueryAsync<Guid>(sql, new
            {
                RestaurantId = restaurantId
            })).ToList();
        }
        public async Task<List<Guid>> AssignUsersAsync(Guid restaurantId, List<Guid> users, Guid assignedBy)
        {

            string sql = @"usp_AssignRestaurantUsers";

            var response = await _dataProvider.Connection.QueryAsync<Guid>(sql, new
            {
                RestaurantId = restaurantId,
                UserIds = users.ToCommaSeparatedString(),
                AssignedBy = assignedBy
            }, commandType: System.Data.CommandType.StoredProcedure);

            return response.ToList();
        }
        public void RemoveUsers(Guid restaurantId, Guid userId)
        {
            string sql = @"DELETE FROM RestaurantUserMapping
                           WHERE RestaurantId = @RestaurantId AND UserId = @UserId";

            _dataProvider.Connection.Execute(sql, new
            {
                RestaurantId = restaurantId,
                UserId = userId
            });
        }

        public bool CheckRestaurantExists(Guid restaurantId, bool includeDeleted = false)
        {
            string sql = @"
                    IF EXISTS (SELECT
			                    1
		                    FROM RestaurantMaster rm
		                    WHERE rm.RestaurantId = @RestaurantId AND (1 = @IncludeDeleted OR rm.IsDeleted = @IncludeDeleted)
		                    )
	                    BEGIN

		                    SELECT 1;
	                    END
                    ELSE
	                    BEGIN
		                    SELECT 0;
	                    END
                ";
            var retVal = _dataProvider.Connection.ExecuteScalar<bool>(sql, new
            {
                RestaurantId = restaurantId,
                IncludeDeleted = includeDeleted
            });
            return retVal;
        }

        public async Task<bool> CheckUserHasAccessOfRestaurant(Guid restaurantId, Guid userId)
        {
            string sql = @"
                    IF EXISTS (SELECT
			                    1
		                    FROM RestaurantUserMapping rum
		                    WHERE rum.UserId = @UserId
		                    AND rum.RestaurantId = @RestaurantId)
	                    BEGIN

		                    SELECT 1;

	                    END
                    ELSE
	                    BEGIN
		                    SELECT 0;
	                    END
                    ";
            return await _dataProvider.Connection.ExecuteScalarAsync<bool>(sql, new
            {
                UserId = userId,
                RestaurantId = restaurantId
            });
        }

        public Guid? GetRestaurantIdFromUserId(Guid userId)
        {

            string sql = @"SELECT rum.RestaurantId FROM RestaurantUserMapping rum
                            WHERE rum.UserId = @UserId";

            return _dataProvider.Connection.QueryFirstOrDefault<Guid?>(sql, new { UserId = userId });
        }

        public async Task<List<Restaurant>> GetRestaurantsByCompany(Guid restaurantCompanyId)
        {
            string sql = @"	SELECT
		                        rm.RestaurantId
	                           ,rm.RestaurantCompanyId
	                           ,rm.RestaurantName
	                           ,rm.Description
	                           ,rm.Website
	                           ,rm.Phone
	                           ,rm.StreetAddress
	                           ,rm.State
	                           ,rm.Country
	                           ,rm.OpeningTime
	                           ,rm.ClosingTime
	                           ,rm.CurrentStatus
	                           ,rm.CreatedBy
	                           ,rm.CreatedDate
	                           ,rm.UpdatedBy
	                           ,rm.UpdatedDate
	                           ,rm.IsDeleted
	                           ,rm.Latitude
	                           ,rm.Longitude
	                        FROM RestaurantMaster rm
	                        WHERE rm.RestaurantCompanyId = @RestaurantCompanyId;";

            return (await _dataProvider.Connection.QueryAsync<Restaurant>(sql, new { RestaurantCompanyId =restaurantCompanyId })).ToList();
        }
    }
}
