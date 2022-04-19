using TableBookingSystem.Application.Repository;
using TableBookingSystem.Entities.Domain.RestaurantCompany;
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
    public class RestaurantCompanyRepository : IRestaurantCompanyRepository
    {
        private readonly IDataProvider _dataProvider;

        public RestaurantCompanyRepository(IDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        #region Repository Implementation

        #region Delete
        public void Delete(RestaurantCompany entity)
        {
            string sql = @"UPDATE RestaurantCompanyMaster
                           SET IsDeleted = 1
                           WHERE rcm.RestaurantCompanyId = @RestaurantCompanyId;
                        ";
            _dataProvider.Connection.Execute(sql, new { RestaurantCompanyId = entity.GetId() });
        }

        public void Delete(Guid id)
        {
            string sql = @"UPDATE RestaurantCompanyMaster
                           SET IsDeleted = 1
                           WHERE RestaurantCompanyId = @RestaurantCompanyId";
            _dataProvider.Connection.Execute(sql, new { RestaurantCompanyId = id });
        }

        public async Task DeleteAsync(RestaurantCompany entity)
        {
            await Task.Run(() => Delete(entity));
        }

        public async Task DeleteAsync(Guid id)
        {
            await Task.Run(() => Delete(id));
        }



        #endregion

        #region Get
        public async Task< IEnumerable<RestaurantCompany>> GetAll()
        {
            string sql = @"SELECT rcm.RestaurantCompanyId,
		                        rcm.RestaurantCompanyName,
		                        rcm.Description,
		                        rcm.Phone,
		                        rcm.Website,
		                        rcm.IsDeleted,
		                        rcm.CreatedBy,
		                        rcm.CreatedDate,
		                        rcm.UpdatedBy,
		                        rcm.UpdatedDate
                        FROM RestaurantCompanyMaster AS rcm
                        WHERE rcm.IsDeleted = 0";
            
            var retVal = await _dataProvider.Connection.QueryAsync<RestaurantCompany>(sql);
            return retVal;
        }
        public RestaurantCompany GetById(Guid id)
        {
            string sql = @"SELECT rcm.RestaurantCompanyId,
		                        rcm.RestaurantCompanyName,
		                        rcm.Description,
		                        rcm.Phone,
		                        rcm.Website,
		                        rcm.IsDeleted,
		                        rcm.CreatedBy,
		                        rcm.CreatedDate,
		                        rcm.UpdatedBy,
		                        rcm.UpdatedDate
                        FROM RestaurantCompanyMaster AS rcm
                        WHERE rcm.RestaurantCompanyId = @RestaurantCompanyId AND rcm.IsDeleted = 0";

            var retVal = _dataProvider.Connection.QueryFirstOrDefault<RestaurantCompany>(sql, new { RestaurantCompanyId = id });
            return retVal;

        }

        public async Task<RestaurantCompany> GetByIdAsync(Guid id)
        {
            return await Task.FromResult(GetById(id));
        }

        public IList<RestaurantCompany> GetByIds(IList<Guid> ids)
        {
            string sql = @"SELECT rcm.RestaurantCompanyId,
		                            rcm.RestaurantCompanyName,
		                            rcm.Description,
		                            rcm.Phone,
		                            rcm.Website,
		                            rcm.IsDeleted,
		                            rcm.CreatedBy,
		                            rcm.CreatedDate,
		                            rcm.UpdatedBy,
		                            rcm.UpdatedDate
                            FROM RestaurantCompanyMaster AS rcm
                            WHERE rcm.RestaurantCompanyId IN (@RestaurantCompanyIds) AND rcm.IsDeleted = 0";

            var retVal = _dataProvider.Connection.Query<RestaurantCompany>(sql, new { RestaurantCompanyIds = ids.ToCommaSeparatedString(true) });
            return retVal.ToList();
        }

        public async Task<IList<RestaurantCompany>> GetByIdsAsync(IList<Guid> ids)
        {
            return await Task.FromResult(GetByIds(ids));
        }

        #endregion

        #region Insert
        public RestaurantCompany Insert(RestaurantCompany entity)
        {
            string sql = @"usp_RestaurantCompany_IU";

            var retVal = _dataProvider.Connection.QueryFirstOrDefault<RestaurantCompany>(sql, new
            {
                RestaurantCompanyName = entity.RestaurantCompanyName,
                Description = entity.Description,
                Website = entity.Website,
                Phone = entity.Phone,
                CreatedOrUpdatedBy = entity.CreatedBy,
                CreatedOrUpdatedDate = entity.CreatedDate,
            }, commandType: System.Data.CommandType.StoredProcedure);
            return retVal;
        }

        public async Task<RestaurantCompany> InsertAsync(RestaurantCompany entity)
        {
            return await Task.FromResult(Insert(entity));
        }
        #endregion

        #region Update
        public RestaurantCompany Update(RestaurantCompany entity)
        {
            string sql = @"usp_RestaurantCompany_IU";

            var retVal = _dataProvider.Connection.QueryFirstOrDefault<RestaurantCompany>(sql, new
            {
                RestaurantCompanyId=entity.RestaurantCompanyId,
                RestaurantCompanyName = entity.RestaurantCompanyName,
                Description = entity.Description,
                Website = entity.Website,
                Phone = entity.Phone,
                CreatedOrUpdatedBy = entity.UpdatedBy,
                CreatedOrUpdatedDate = entity.UpdatedDate,
            }, commandType: System.Data.CommandType.StoredProcedure);
            return retVal;
        }

        public async Task<RestaurantCompany> UpdateAsync(RestaurantCompany entity)
        {
            return await Task.FromResult(Update(entity));
        }
        #endregion

        #endregion
    }
}
