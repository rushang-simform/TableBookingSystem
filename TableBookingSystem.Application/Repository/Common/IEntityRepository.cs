using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableBookingSystem.Entities.Domain.AbstractEntities;

namespace TableBookingSystem.Application.Repository
{
    public interface IBaseRepository { }

    public interface IEntityRepository<TEntity, TId> : IBaseRepository
        where TEntity : BaseEntity<TId>
    {
        TEntity GetById(TId id);
        IList<TEntity> GetByIds(IList<TId> ids);
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
        void Delete(TId id);

    }
    public interface IAsyncEntityRepository<TEntity, TId> : 
        IEntityRepository<TEntity, TId> where TEntity : BaseEntity<TId>
    {
        Task<TEntity> GetByIdAsync(TId id);
        Task<IList<TEntity>> GetByIdsAsync(IList<TId> ids);
        Task<TEntity> InsertAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task DeleteAsync(TId id);
    }
}
