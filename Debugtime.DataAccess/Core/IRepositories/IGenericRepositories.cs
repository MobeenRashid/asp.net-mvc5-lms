using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Debugtime.DataAccess.Core.IRepositories
{
    public interface IGenericRepositories<TEntity> where TEntity:class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(string id);
        TEntity Create(TEntity entity);
        Task<TEntity> Delete(string id);
        TEntity Delete(TEntity entity);
        TEntity Update(TEntity entity);
    }
}
