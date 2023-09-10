using Microsoft.EntityFrameworkCore;
using Storage.DAOs;
using Storage.Repositories.Interfaces;

namespace Storage.Repositories
{
    public class StringDAORepository : IStoreRepository<StringDAO>
    {
        private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

        public StringDAORepository(IDbContextFactory<AppDbContext> dbContextFactory) 
        {
            _dbContextFactory = dbContextFactory ?? throw new ArgumentNullException(nameof(dbContextFactory));
        }
        public async Task<StringDAO> CreateAsync(StringDAO entity)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                //redundancy for safety purposes:
                var obj = new StringDAO
                {
                    CreatedAt = DateTime.UtcNow,
                    StringValue = entity.StringValue
                };

                context.StringsSet.Add(entity);

                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<StringDAO> DeleteAsync(int identifier)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var obj = await GetById(identifier);
                if(obj is not null)
                {
                    obj.DeletedAt = DateTime.UtcNow;
                    await context.SaveChangesAsync();
                    return obj;
                }
                else
                {
                    throw new KeyNotFoundException(identifier.ToString());
                }
            }
        }

        public async Task<IEnumerable<StringDAO>> GetAllAsync()
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var results = context.StringsSet.Where(d => d.DeletedAt == null);

                return results.ToList();
            }
        }

        public Task<StringDAO> GetById(int identifier)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var obj = context.StringsSet.Single(d => d.Identifier == identifier && d.DeletedAt == null);
                if (obj is not null)
                {
                    return Task.FromResult(obj);
                }
                else
                {
                    throw new KeyNotFoundException(identifier.ToString());
                }
            }
        }

        public async Task<StringDAO> UpdateAsync(StringDAO entity)
        {
            using (var context = _dbContextFactory.CreateDbContext())
            {
                var obj = await GetById(entity.Identifier);
                if (obj is not null)
                {
                    obj.LastModifiedAt = DateTime.UtcNow;
                    obj.StringValue = entity.StringValue;
                    await context.SaveChangesAsync();
                    return obj;
                }
                else
                {
                    throw new KeyNotFoundException(entity.Identifier.ToString());
                }
            }
        }
    }
}