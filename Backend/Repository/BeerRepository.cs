
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Backend.Repository
{
    public class BeerRepository : IRepository<Beer>
    {
        private StoreContext _context;

        public BeerRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Beer>> Get()
            => await _context.Beers.ToListAsync();

        public async Task<Beer> GetbyId(int id)
            => await _context.Beers.FindAsync(id);

        public async Task Add(Beer entity)
            => await _context.Beers.AddAsync(entity);

        public void Update(Beer beer)
        {
            _context.Beers.Attach(beer);
            _context.Beers.Entry(beer).State = EntityState.Modified;
        }
        public void Delete(Beer beer)
            => _context.Beers.Remove(beer);

        public async Task Save()
            => await _context.SaveChangesAsync();

        public IEnumerable<Beer> Search(Func<Beer, bool> filter) =>
            _context.Beers.Where(filter).ToList();
        public IQueryable<Beer> Search(Expression<Func<Beer, bool>> filter)
        {
            return _context.Beers.Where(filter);  // Devuelve un IQueryable
        }
        public async Task<IEnumerable<Beer>> SearchByNameStoredProcedure(string name)
        {
            return await _context.Beers
                .FromSqlInterpolated($"EXEC sp_filtraProductoPorNombre {name}")
                .ToListAsync();
        }
    }
}
