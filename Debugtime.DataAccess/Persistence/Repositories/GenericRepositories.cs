using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Debugtime.DataAccess.Core.IRepositories;
using System.Data.Entity;
using Debugtime.Common.Configurations;
using Debugtime.Common.Persistence;

namespace Debugtime.DataAccess.Persistence.Repositories
{
    internal class GenericRepositories<TEntity> : AutoMapperProfileConfiguration, IGenericRepositories<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext _db;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepositories(ApplicationDbContext db)
        {
            this._db = db;
            this._dbSet = _db.Set<TEntity>();
        }

        TEntity IGenericRepositories<TEntity>.Create(TEntity entity)
        {
            if (entity == null)
                return null;
            return _dbSet.Add(entity);
        }

        async Task<TEntity> IGenericRepositories<TEntity>.Delete(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            var entity = await _dbSet.FindAsync(id);

            if (entity == null)
                return null;
            return _dbSet.Remove(entity);
        }

        TEntity IGenericRepositories<TEntity>.Delete(TEntity entity)
        {
            if (entity == null)
                return null;

            return _dbSet.Remove(entity);
        }

        async Task<IEnumerable<TEntity>> IGenericRepositories<TEntity>.GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        async Task<TEntity> IGenericRepositories<TEntity>.GetById(string id)
        {
            if (String.IsNullOrWhiteSpace(id))
                return null;

            return await _dbSet.FindAsync(id);
        }

        TEntity IGenericRepositories<TEntity>.Update(TEntity entityToUpdate)
        {
            if (entityToUpdate == null)
                return null;

            var updatedEntity = _dbSet.Attach(entityToUpdate);
            _db.Entry(entityToUpdate).State = EntityState.Modified;
            return updatedEntity;
        }
    }
}
