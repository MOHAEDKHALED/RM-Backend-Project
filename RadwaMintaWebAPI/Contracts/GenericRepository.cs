using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1;
using RadwaMintaWebAPI.Models.DbContexts;
using RadwaMintaWebAPI.Models.Entities;
using System.Linq.Expressions;

namespace RadwaMintaWebAPI.Contracts
{
    public class GenericRepository<TEntity, TKey>(MintaDbContext _dbContext) : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // Add
        public async Task AddAsync(TEntity entity) => await _dbContext.Set<TEntity>().AddAsync(entity);


        // Get All (without specifications)
        public async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().ToListAsync();


        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate) => await _dbContext.Set<TEntity>().ToListAsync();
        

        // Get By Id
        public async Task<TEntity?> GetByIdAsync(TKey id) => await _dbContext.Set<TEntity>().FindAsync(id);

        // Delete
        public void Delete(TEntity entity) => _dbContext.Set<TEntity>().Remove(entity);

        // Update
        public void Update(TEntity entity) => _dbContext.Set<TEntity>().Update(entity);


        #region With Specifications

        // GetAllAsync with specifications
        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications)
        {
            // هنا بيستخدم الـ SpecificationEvaluator عشان يطبق الفلترة والـ includes
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).ToListAsync();
        }

        // GetByIdAsync with specifications
        public async Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications)
        {
            return await SpecificationEvaluator.CreateQuery(_dbContext.Set<TEntity>(), specifications).FirstOrDefaultAsync();
        }

        #endregion
    }
}
