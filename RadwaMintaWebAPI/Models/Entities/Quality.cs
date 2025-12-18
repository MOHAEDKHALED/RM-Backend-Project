namespace RadwaMintaWebAPI.Models.Entities
{
    public class Quality : BaseEntity<int>
    {
        public string Title { get; set; } = default!;
        public string TitleAr { get; set; } = default!;
        public string PictureUrl { get; set; } = default!;
        public string Content { get; set; } = default!;
        public string ContentAr { get; set; } = default!;

    }
}
