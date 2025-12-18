namespace RadwaMintaWebAPI.Models.Entities
{
    public class RevokedToken : BaseEntity<int>
    {
        public string Jti { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
