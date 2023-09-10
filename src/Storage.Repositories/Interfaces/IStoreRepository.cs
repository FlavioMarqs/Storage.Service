using Storage.DAOs;

namespace Storage.Repositories.Interfaces
{
    public interface IStoreRepository<T> where T : IdentifiableDAO
    {
        Task<T> CreateAsync(T entity);

        Task<T> UpdateAsync(T entity);

        Task<T> DeleteAsync(int entity);

        Task<IEnumerable<T>> GetAllAsync(bool IncludeDeleted = false);

        Task<T> GetById(int id);
    }
}
