using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.Models.Entities;
using System.Linq.Expressions;

namespace RadwaMintaWebAPI.Specifications
{
    public class BaseSpecifications<TEntity, TKey> : ISpecifications<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        protected BaseSpecifications()
        {

        }

        protected BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;
        }

        public Expression<Func<TEntity, bool>>? Criteria { get; private set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; } = [];

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        => IncludeExpressions.Add(includeExpression);
    }
}
