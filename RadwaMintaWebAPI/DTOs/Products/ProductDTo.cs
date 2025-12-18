namespace RadwaMintaWebAPI.DTOs.Products
{
    public class ProductDTo
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string About { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public string? ScientificName { get; set; }
        public List<string> Forms { get; set; } = default!;
        public List<string> ActiveIngredients { get; set; } = default!;
        public List<string> HarvestSeason { get; set; } = default!;
        public List<string> Availability { get; set; } = default!;
        public List<string> ContainerCapacity { get; set; } = default!;
        public List<string> NaturalWonders { get; set; } = default!;
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = default!;
    }
}
