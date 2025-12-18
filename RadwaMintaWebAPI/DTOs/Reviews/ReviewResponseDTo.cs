namespace RadwaMintaWebAPI.DTOs.Reviews
{
    public class ReviewResponseDTo
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string JobTitle { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Description { get; set; } = default!;
        public int Rating { get; set; }
        public DateTime ReviewDate { get; set; } = DateTime.Now;
    }
}
