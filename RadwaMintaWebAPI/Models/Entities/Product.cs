namespace RadwaMintaWebAPI.Models.Entities
{
    public class Product : BaseEntity<int>
    {
        public string? Name { get; set; } = default!;
        public string? NameAr { get; set; } = default!;
        public string? About { get; set; } = default!;
        public string? AboutAr { get; set; } = default!;
        public string? PictureUrl { get; set; } = default!;
        public string? ScientificName { get; set; } = default!;
        public string? ScientificNameAr { get; set; } = default!;
        public List<string>? Forms { get; set; } = default!;
        public List<string>? FormsAr { get; set; } = default!;
        public List<string>? ActiveIngredients { get; set; } = default!;
        public List<string>? ActiveIngredientsAr { get; set; } = default!;
        public List<string>? HarvestSeason { get; set; } = default!;
        public List<string>? HarvestSeasonAr { get; set; } = default!;
        public List<string>? Availability { get; set; } = default!;
        public List<string>? AvailabilityAr { get; set; } = default!;
        public List<string>? ContainerCapacity { get; set; } = default!;
        public List<string>? ContainerCapacityAr { get; set; } = default!;
        public List<string>? NaturalWonders { get; set; } = default!;
        public List<string>? NaturalWondersAr { get; set; } = default!;
        // For Relationship
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}
