using Storage.Repositories.Interfaces;

namespace Storage.Repositories
{
    public class StringRepository : IRepository<string>
    {
        public Task CreateAsync(string entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(string entity)
        {
            throw new NotImplementedException();
        }
    }
}