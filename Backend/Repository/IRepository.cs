using Backend.Models;
using System.Collections;
using System.Linq.Expressions;

namespace Backend.Repository
{
    public interface IRepository<TEntity> 
    {
        Task<IEnumerable<TEntity>> Get();
        Task<TEntity> GetbyId(int id);
        Task Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(Beer beer);
        Task Save();

        IQueryable<TEntity> Search(Expression<Func<TEntity, bool>> filter);
        Task<IEnumerable<TEntity>> SearchByNameStoredProcedure(string name); // Cambia a TEntity
    }
}
