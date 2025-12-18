namespace RadwaMintaWebAPI.Models.Entities
{
    public class Category : BaseEntity<int>
    {
        public  string Name { get; set; } = default!;
        public string NameAr { get; set; } = default!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
