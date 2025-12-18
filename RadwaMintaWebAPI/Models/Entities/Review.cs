namespace RadwaMintaWebAPI.Models.Entities
{
    public class Review : BaseEntity<int>
    {
        public string? FirstName { get; set; } = default!;
        public string? FirstNameAr { get; set; } = default!;
        public string? LastName { get; set; } = default!;
        public string? LastNameAr { get; set; } = default!;
        public string? JobTitle { get; set; } = default!;
        public string? JobTitleAr { get; set; } = default!;
        public string? Email { get; set; } = default!;
        public string? Description { get; set; } = default!;
        public string? DescriptionAr { get; set; } = default!;
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}
