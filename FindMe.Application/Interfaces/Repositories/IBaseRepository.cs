
namespace FindMe.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        public Task<bool> AnyAsync(Func<T,bool> predicate);
        public Task<T> GetByIdAsync(int id);
        public Task<T> GetItemOnAsync(Func<T, bool> match);
        public Task<IEnumerable<T>> GetAllAsync();
        public IQueryable<T> Entities();
        public Task AddAsync(T input);
        Task AddRangeAsync(List<T> input);

        public Task UpdateAsync(T input);

        public Task DeleteAsync(T input);
        Task DeleteRange(List<T> input);
    }
}
