using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleCatalog.Infrastructure.EF;
using VehicleCatalog.Core.Entities;
using VehicleCatalog.Application.IRepositories;
using VehicleCatalog.Application.Paging;
using System.Linq.Expressions;

namespace VehicleCatalog.Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : EntityBase
    {

        private readonly ApplicationContext _db;
        private readonly DbSet<TEntity> _table;
        public GenericRepository(ApplicationContext db)
        {
            this._db = new ApplicationContext();
            this._table = this._db.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity)
        {
            await this._table.AddAsync(entity);
            await this._db.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            this._table.Remove(entity);
            await this._db.SaveChangesAsync();
        }

        public async Task<TEntity> GetOneAsync(int id)
        {
            return await this._table.FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters)
        {
            var elements = await this._table
                                     .AsNoTracking()
                                     .Skip((pageParameters.PageIndex - 1) * pageParameters.PageSize)
                                     .Take(pageParameters.PageSize)
                                     .ToListAsync();
            var totalElements = await this._table.CountAsync();

            return new PagedList<TEntity>(elements, pageParameters, totalElements);
        }

        public async Task<PagedList<TEntity>> GetPageAsync(PageParameters pageParameters, Expression<Func<TEntity, bool>> predicate)
        {
            var elements = await this._table
                                     .AsNoTracking()
                                     .Where(predicate)
                                     .Skip((pageParameters.PageIndex - 1) * pageParameters.PageSize)
                                     .Take(pageParameters.PageSize)
                                     .ToListAsync();
            var totalElements = await this._table.CountAsync(predicate);

            return new PagedList<TEntity>(elements, pageParameters, totalElements);
        }

        public async Task UpdateAsync(TEntity entity)
        {
            this._table.Update(entity);
            await this._db.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            this._table.UpdateRange(entities);
            await this._db.SaveChangesAsync();
        }
    }
}
