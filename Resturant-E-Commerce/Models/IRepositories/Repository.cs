
using Microsoft.EntityFrameworkCore;
using Resturant_E_Commerce.Data;

namespace Resturant_E_Commerce.Models.IRepositories
{
    public class Repository<T> : IRepository<T> where T:class
    {
        private readonly ApplicationDbContext _context;
        private DbSet<T> _dbSet;
        public Repository(ApplicationDbContext context )
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T obj)
        {
            if (obj != null) 
            { 
                await _context.AddAsync<T>(obj);
            }
        }

        public async Task DeleteAsync(int id)
        {
            T? obj =await _dbSet.FindAsync(id);
            if (obj == null)
                throw new Exception("this id is not exists !");

            _dbSet.Remove(obj);

        }

        public async Task<IEnumerable<T>> GetAllAsync(string includes = "")
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            T? obj = await _dbSet.FindAsync(id);
            if (obj == null)
                throw new Exception("This id not exists !");

            return obj;
        }

        public async Task UpdateAsync(T obj)
        {
            _context.Update(obj);
        }
        public async Task SaveAsync() 
        {
            await _context.SaveChangesAsync();
        }
    }
}
