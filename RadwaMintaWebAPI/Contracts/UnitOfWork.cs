using RadwaMintaWebAPI.Models.DbContexts;
using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Contracts
{
    public class UnitOfWork(MintaDbContext _dbContext) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _repositories = [];
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : BaseEntity<TKey>
        {
            var typeName = typeof(TEntity).Name;
            if (_repositories.TryGetValue(typeName, out object? value))
            {
                return (IGenericRepository<TEntity, TKey>)value;
            }
            else
            {
                /// Create Object
                var Repo = new GenericRepository<TEntity, TKey>(_dbContext);
                /// Store Object in Dictionary
                _repositories[typeName] = Repo;
                /// Return Object
                return Repo;
            }
        }

        public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    }
}
