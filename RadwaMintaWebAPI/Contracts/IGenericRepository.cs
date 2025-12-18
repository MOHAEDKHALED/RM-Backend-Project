using RadwaMintaWebAPI.Models.Entities;
using System.Linq.Expressions;

namespace RadwaMintaWebAPI.Contracts
{
    public interface IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetByIdAsync(TKey id);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        #region With Specifications
        Task<TEntity?> GetByIdAsync(ISpecifications<TEntity, TKey> specifications); // هذا هو الـ overload اللي بياخد ISpecifications
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, TKey> specifications); // هذا هو الـ overload اللي بياخد ISpecifications


        #endregion
    }
}
