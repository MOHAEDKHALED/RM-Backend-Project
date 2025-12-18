using RadwaMintaWebAPI.DTOs.Products;

namespace RadwaMintaWebAPI.Interfaces
{
    public interface IProductService
    {
        //// Get All Products 
        Task<IEnumerable<ProductDTo>> GetAllProductsAsync(string lang);
        Task<IEnumerable<CategoryDTo>> GetAllCategoriesAsync(string lang);
        Task<IEnumerable<ProductDTo>> GetFirst4ProductsAsync(string lang);
        Task<IEnumerable<ProductDTo>> GetProductsByCategoryIdAsync(int categoryId, string lang);
        Task<IEnumerable<ProductDTo>> GetRelatedProductsByCategoryIdAsync(int categoryId, int currentProductId, string lang);

        //// For Order
        Task<string> GenerateWhatsappLinkForOrder(int productId);

        //Task<OrderMessageDto> GetOrderMessageAsync(int productId, bool trackChanges);

        //// Get Product by Id
        Task<ProductDTo> GetProductByIdAsync(int id, string lang);
        Task<ProductDTo> GetProductByIdAsync(int id);

        // Get All Products 
        //Task<IEnumerable<ProductDTo>> GetAllProductsAsync(string lang = "en");
        //Task<IEnumerable<CategoryDTo>> GetAllCategoriesAsync(string lang = "en");
        //Task<IEnumerable<ProductDTo>> GetFirst4ProductsAsync(string lang = "en");
        //Task<IEnumerable<ProductDTo>> GetProductsByCategoryIdAsync(int categoryId, string lang = "en");
        //Task<IEnumerable<ProductDTo>> GetRelatedProductsByCategoryIdAsync(int categoryId, int currentProductId, string lang = "en");

        //// For Order
        //Task<string> GenerateWhatsappLinkForOrder(int productId);

        //// Get Product by Id
        //Task<ProductDTo> GetProductByIdAsync(int id, string lang = "en");
    }
}
