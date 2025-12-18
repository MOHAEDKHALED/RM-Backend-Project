namespace RadwaMintaWebAPI.Models.Entities
{
    public class ExperienceSettings : BaseEntity<int>
    {
        public DateTime StartDate { get; set; } 
        public string Description { get; set; } = "Company Experience Start Date";
    }
}
