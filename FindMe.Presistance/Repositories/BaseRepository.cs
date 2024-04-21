using FindMe.Application.Interfaces.Repositories;
using FindMe.Presistance.Context;
using Microsoft.EntityFrameworkCore;

namespace FindMe.Presistance.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AnyAsync(Func<T, bool> predicate)
        {
            await Task.CompletedTask;
            return  _context.Set<T>().Any(predicate);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task AddAsync(T input)
        {
            _context.Set<T>().Add(input);
            _context.SaveChanges();
            return Task.CompletedTask;
        }
        public async Task AddRangeAsync(List<T> input)
        {
            await _context.Set<T>().AddRangeAsync(input);
        }

        public async Task UpdateRangeAsync(List<T> input)
        {
            await Task.CompletedTask;
            _context.Set<T>().UpdateRange(input);
        }
        public Task UpdateAsync(T input)
        {
            _context.Update(input);
            _context.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T input)
        {
            _context.Remove(input);

            _context.SaveChanges();
            return Task.CompletedTask;
        }
        public Task DeleteRange(List<T> input)
        {
            _context.RemoveRange(input);
            return Task.CompletedTask;
        }
        public async Task<T> GetItemOnAsync(Func<T, bool> match)
        {
            
            return _context.Set<T>().FirstOrDefault(match);
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public IQueryable<T> Entities() => _context.Set<T>();
    }
}
