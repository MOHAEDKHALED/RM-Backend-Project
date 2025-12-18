using RadwaMintaWebAPI.Models.Entities;
using System.Linq.Expressions;

namespace RadwaMintaWebAPI.Contracts
{
    public interface ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        // Property Signature for Each Dynamic Part in Query

        public Expression<Func<TEntity, bool>>? Criteria { get; }
        List<Expression<Func<TEntity, Object>>> IncludeExpressions { get; }
    }
}
