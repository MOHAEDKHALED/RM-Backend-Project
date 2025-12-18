namespace RadwaMintaWebAPI.Models.Entities
{
    public class ContactUs : BaseEntity<int>
    {
        public string Name { get; set; } = default!;
        public string PhoneNumber { get; set; } = default!;
        public string email { get; set; } = default!;
        public string desc { get; set; } = default!;
        public DateTime SentDate { get; set; } = DateTime.Now;
    }
}
