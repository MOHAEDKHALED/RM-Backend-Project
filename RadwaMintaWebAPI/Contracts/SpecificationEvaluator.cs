using Microsoft.EntityFrameworkCore;
using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Contracts
{
    public class SpecificationEvaluator
    {
        // Create Query
        // _dbContext.Products.where(p => p.id == id).Include(p => p.ProductBrand).Include(p => p.ProductType)
        public static IQueryable<TEntity> CreateQuery<TEntity, TKey>(IQueryable<TEntity> inputQuery, ISpecifications<TEntity, TKey> specification) where TEntity : BaseEntity<TKey>
        {
            var query = inputQuery;

            // Apply criteria if it exists (not null)
            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            // Apply includes (Expression-based)
            query = specification.IncludeExpressions.Aggregate(query, (current, include) => current.Include(include));

            return query;


        }
    }
}
