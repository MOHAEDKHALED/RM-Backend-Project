using AutoMapper;
using RadwaMintaWebAPI.Contracts;
using RadwaMintaWebAPI.DTOs.Products;
using RadwaMintaWebAPI.Helpers;
using RadwaMintaWebAPI.Interfaces;
using RadwaMintaWebAPI.Models.Entities;
using RadwaMintaWebAPI.Specifications;
using System.Globalization;

namespace RadwaMintaWebAPI.Services
{
    public class ProductService(IUnitOfWork _unitOfWork, IMapper _mapper, IConfiguration _configuration) : IProductService
    {

        private readonly string _whatsappNumber = _configuration["WhatsappSettings:PhoneNumber"] ??
                                                 throw new ArgumentNullException("Whatsapp phone number is not configured in appsettings.json.");


        private string GetFullPictureUrl(string relativeUrl)
        {
            if (string.IsNullOrEmpty(relativeUrl))
                return string.Empty;
            else
            {
                var baseUrl = _configuration.GetSection("URLs")["BaseUrl"];
                return $"{baseUrl}{relativeUrl}";
            }
        }


        public async Task<IEnumerable<ProductDTo>> GetAllProductsAsync(string lang)
        {
            var spec = new ProductWithCategorySpecifications();

            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var productDtos = new List<ProductDTo>();
            foreach (var product in products)
            {
                productDtos.Add(new ProductDTo
                {
                    Id = product.Id,
                    Name = lang == "ar" ? product.NameAr : product.Name,
                    About = lang == "ar" ? product.AboutAr : product.About,
                    PictureUrl = GetFullPictureUrl(product.PictureUrl),
                    ScientificName = lang == "ar" ? product.ScientificNameAr : product.ScientificName,
                    Forms = lang == "ar" ? (product.FormsAr ?? new List<string>()) : (product.Forms ?? new List<string>()),
                    ActiveIngredients = lang == "ar" ? (product.ActiveIngredientsAr ?? new List<string>()) : (product.ActiveIngredients ?? new List<string>()),
                    HarvestSeason = lang == "ar" ? (product.HarvestSeasonAr ?? new List<string>()) : (product.HarvestSeason ?? new List<string>()),
                    Availability = lang == "ar" ? (product.AvailabilityAr ?? new List<string>()) : (product.Availability ?? new List<string>()),
                    ContainerCapacity = lang == "ar" ? (product.ContainerCapacityAr ?? new List<string>()) : (product.ContainerCapacity ?? new List<string>()),
                    NaturalWonders = lang == "ar" ? (product.NaturalWondersAr ?? new List<string>()) : (product.NaturalWonders ?? new List<string>()),
                    CategoryId = product.CategoryId,
                    CategoryName = lang == "ar" ? product.Category?.NameAr : product.Category?.Name
                });
            }
            return productDtos;
        }


        public async Task<IEnumerable<ProductDTo>> GetFirst4ProductsAsync(string lang)
        {
            var spec = new ProductWithCategorySpecifications();
            var productsEn = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);
            var selectData = productsEn.Take(4);

            var productDtos = new List<ProductDTo>();
            foreach (var product in selectData)
            {
                productDtos.Add(new ProductDTo
                {
                    Id = product.Id,
                    Name = lang == "ar" ? product.NameAr : product.Name,
                    About = lang == "ar" ? product.AboutAr : product.About,
                    PictureUrl = GetFullPictureUrl(product.PictureUrl),
                    ScientificName = lang == "ar" ? product.ScientificNameAr : product.ScientificName,
                    Forms = lang == "ar" ? (product.FormsAr ?? new List<string>()) : (product.Forms ?? new List<string>()),
                    ActiveIngredients = lang == "ar" ? (product.ActiveIngredientsAr ?? new List<string>()) : (product.ActiveIngredients ?? new List<string>()),
                    HarvestSeason = lang == "ar" ? (product.HarvestSeasonAr ?? new List<string>()) : (product.HarvestSeason ?? new List<string>()),
                    Availability = lang == "ar" ? (product.AvailabilityAr ?? new List<string>()) : (product.Availability ?? new List<string>()),
                    ContainerCapacity = lang == "ar" ? (product.ContainerCapacityAr ?? new List<string>()) : (product.ContainerCapacity ?? new List<string>()),
                    NaturalWonders = lang == "ar" ? (product.NaturalWondersAr ?? new List<string>()) : (product.NaturalWonders ?? new List<string>()),
                    CategoryId = product.CategoryId,
                    CategoryName = lang == "ar" ? product.Category?.NameAr : product.Category?.Name
                });
            }
            return productDtos;
        }

        public async Task<IEnumerable<CategoryDTo>> GetAllCategoriesAsync(string lang)
        {
            var categories = await _unitOfWork.GetRepository<Category, int>().GetAllAsync();

            var categoryDtos = new List<CategoryDTo>();
            foreach (var category in categories)
            {
                categoryDtos.Add(new CategoryDTo
                {
                    Id = category.Id,
                    Name = lang == "ar" ? category.NameAr : category.Name
                });
            }
            return categoryDtos;
        }


        public async Task<IEnumerable<ProductDTo>> GetProductsByCategoryIdAsync(int categoryId, string lang)
        {
            var spec = new ProductWithCategorySpecifications(categoryId);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var productDtos = new List<ProductDTo>();
            foreach (var product in products)
            {
                productDtos.Add(new ProductDTo
                {
                    Id = product.Id,
                    Name = lang == "ar" ? product.NameAr : product.Name,
                    About = lang == "ar" ? product.AboutAr : product.About,
                    PictureUrl = GetFullPictureUrl(product.PictureUrl),
                    ScientificName = lang == "ar" ? product.ScientificNameAr : product.ScientificName,
                    Forms = lang == "ar" ? (product.FormsAr ?? new List<string>()) : (product.Forms ?? new List<string>()),
                    ActiveIngredients = lang == "ar" ? (product.ActiveIngredientsAr ?? new List<string>()) : (product.ActiveIngredients ?? new List<string>()),
                    HarvestSeason = lang == "ar" ? (product.HarvestSeasonAr ?? new List<string>()) : (product.HarvestSeason ?? new List<string>()),
                    Availability = lang == "ar" ? (product.AvailabilityAr ?? new List<string>()) : (product.Availability ?? new List<string>()),
                    ContainerCapacity = lang == "ar" ? (product.ContainerCapacityAr ?? new List<string>()) : (product.ContainerCapacity ?? new List<string>()),
                    NaturalWonders = lang == "ar" ? (product.NaturalWondersAr ?? new List<string>()) : (product.NaturalWonders ?? new List<string>()),
                    CategoryId = product.CategoryId,
                    CategoryName = lang == "ar" ? product.Category?.NameAr : product.Category?.Name
                });
            }
            return productDtos;
        }



        public async Task<IEnumerable<ProductDTo>> GetRelatedProductsByCategoryIdAsync(int categoryId, int currentProductId, string lang)
        {
            var spec = new ProductWithCategorySpecifications(categoryId);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(spec);

            var filteredProducts = products.Where(p => p.Id != currentProductId);
            var selectProducts = filteredProducts.Take(4);

            var productDtos = new List<ProductDTo>();
            foreach (var product in selectProducts)
            {
                productDtos.Add(new ProductDTo
                {
                    Id = product.Id,
                    Name = lang == "ar" ? product.NameAr : product.Name,
                    About = lang == "ar" ? product.AboutAr : product.About,
                    PictureUrl = GetFullPictureUrl(product.PictureUrl),
                    ScientificName = lang == "ar" ? product.ScientificNameAr : product.ScientificName,
                    Forms = lang == "ar" ? (product.FormsAr ?? new List<string>()) : (product.Forms ?? new List<string>()),
                    ActiveIngredients = lang == "ar" ? (product.ActiveIngredientsAr ?? new List<string>()) : (product.ActiveIngredients ?? new List<string>()),
                    HarvestSeason = lang == "ar" ? (product.HarvestSeasonAr ?? new List<string>()) : (product.HarvestSeason ?? new List<string>()),
                    Availability = lang == "ar" ? (product.AvailabilityAr ?? new List<string>()) : (product.Availability ?? new List<string>()),
                    ContainerCapacity = lang == "ar" ? (product.ContainerCapacityAr ?? new List<string>()) : (product.ContainerCapacity ?? new List<string>()),
                    NaturalWonders = lang == "ar" ? (product.NaturalWondersAr ?? new List<string>()) : (product.NaturalWonders ?? new List<string>()),
                    CategoryId = product.CategoryId,
                    CategoryName = lang == "ar" ? product.Category?.NameAr : product.Category?.Name
                });
            }
            return productDtos;
        }



        //// Get By Id
        public async Task<ProductDTo> GetProductByIdAsync(int id, string lang)
        {
            var spec = new ProductWithCategorySpecifications(id, byId: true);

            var ProductEn = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);


            if (ProductEn == null)
            {
                return null;

            }
            var ProductDto = new ProductDTo();
            ProductDto.Id = id;
            ProductDto.PictureUrl = GetFullPictureUrl(ProductEn.PictureUrl);
            ProductDto.Name = lang == "ar" ? ProductEn.NameAr : ProductEn.Name;
            ProductDto.About = lang == "ar" ? ProductEn.AboutAr : ProductEn.About;
            ProductDto.ScientificName = lang == "ar" ? ProductEn.ScientificNameAr : ProductEn.ScientificName;
            ProductDto.Forms = lang == "ar" ? ProductEn.FormsAr : ProductEn.Forms;
            ProductDto.ActiveIngredients = lang == "ar" ? ProductEn.ActiveIngredientsAr : ProductEn.ActiveIngredients;
            ProductDto.HarvestSeason = lang == "ar" ? ProductEn.HarvestSeasonAr : ProductEn.HarvestSeason;
            ProductDto.Availability = lang == "ar" ? ProductEn.AvailabilityAr : ProductEn.Availability;
            ProductDto.ContainerCapacity = lang == "ar" ? ProductEn.ContainerCapacityAr : ProductEn.ContainerCapacity;
            ProductDto.NaturalWonders = lang == "ar" ? ProductEn.NaturalWondersAr : ProductEn.NaturalWonders;
            ProductDto.CategoryId = ProductEn.CategoryId;
            ProductDto.CategoryName = lang == "ar" ? ProductEn.Category?.NameAr : ProductEn.Category?.Name;

            return (ProductDto);


        }
        public async Task<ProductDTo> GetProductByIdAsync(int id)
        {
            var spec = new ProductWithCategorySpecifications(id, byId: true);

            var ProductEn = await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(spec);


            if (ProductEn == null)
            {
                return null;

            }

            return _mapper.Map<Product, ProductDTo>(ProductEn);


        }



        //// For Order 

        public async Task<string> GenerateWhatsappLinkForOrder(int productId)
        {
            if (string.IsNullOrEmpty(_whatsappNumber))
            {
                throw new InvalidOperationException("رقم الواتساب غير مكوّن، لا يمكن إنشاء الرابط.");
            }

            var productDetails = await GetProductByIdAsync(productId);

            if (productDetails == null)
            {
                throw new ArgumentException($"لم يتم العثور على المنتج بالمعرف: {productId}");
            }

            var message = $"أرغب في طلب المنتج:\n" +
                          $"اسم المنتج: {productDetails.Name}\n" +
                            $"صورة المنتج: {GetFullPictureUrl(productDetails.PictureUrl)}\n" +
                          $"الرجاء التواصل معي لإتمام الطلب وتحديد التفاصيل.";


            var encodedMessage = Uri.EscapeDataString(message);
            var whatsappLink = $"https://api.whatsapp.com/send?phone={_whatsappNumber}&text={encodedMessage}";

            return whatsappLink;
        }

        
    }
}
