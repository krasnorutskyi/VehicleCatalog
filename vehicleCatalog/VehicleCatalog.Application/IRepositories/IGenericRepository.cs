using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleCatalog.Core.Entities;
using VehicleCatalog.Application.Paging;
using System.Linq.Expressions;

namespace VehicleCatalog.Application.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : EntityBase
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities);
        Task<TEntity> GetOneAsync(int id);
        Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters);
        Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters, Expression<Func<TEntity, bool>> predicate);

    }
}
