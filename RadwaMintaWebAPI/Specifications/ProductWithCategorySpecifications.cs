using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Specifications
{
    public class ProductWithCategorySpecifications : BaseSpecifications<Product, int>
    {
        public ProductWithCategorySpecifications() : base()
        {
            AddInclude(p => p.Category);
        }

        // Specification to get products by CategoryId with their category included
        public ProductWithCategorySpecifications(int categoryId) : base(p => p.CategoryId == categoryId)
        {
            AddInclude(p => p.Category);
        }

        // Specification to get a single product by Id with its category included
        // This constructor still takes criteria
        public ProductWithCategorySpecifications(int productId, bool byId) : base(p => p.Id == productId)
        {
            // The 'byId' parameter here serves as a flag if you need multiple constructors with int.
            // If you only need to get by ID, this can be simplified.
            AddInclude(p => p.Category);
        }
    }
}
