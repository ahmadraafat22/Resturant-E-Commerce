namespace Resturant_E_Commerce.Models.IRepositories
{
    public interface IRepository<T> where T:class
    {
        Task<IEnumerable<T>> GetAllAsync(string includes="");
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T obj);
        Task UpdateAsync(T obj);
        Task DeleteAsync(int id);
        Task SaveAsync();
    }
}
