namespace RadwaMintaWebAPI.Models.Entities
{
    public class SocialMedia : BaseEntity<int>
    {
        public string? WhatsApp { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Youtube { get; set; }
        public string? X { get; set; }
        public string? Pinterest { get; set; }
        public string? LinkedIn { get; set; }
        public string? Platform { get; set; } 

        
    }
}
