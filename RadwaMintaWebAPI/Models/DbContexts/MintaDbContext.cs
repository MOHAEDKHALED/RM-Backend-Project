using Microsoft.EntityFrameworkCore;
using RadwaMintaWebAPI.Models.Entities;

namespace RadwaMintaWebAPI.Models.DbContexts
{
    public class MintaDbContext : DbContext
    {
        public MintaDbContext(DbContextOptions<MintaDbContext> options) : base(options)
        {
        }
        #region DbSets

        public DbSet<AdminUser> Admins { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContactUs> Contacts { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<SocialMedia> Medias { get; set; }
        public DbSet<Quality> Qualities { get; set; }
        public DbSet<RevokedToken> RevokedTokens { get; set; }
        public DbSet<PasswordResetOtp> PasswordResetOtps { get; set; }


        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ExperienceSettings>().HasData(
               new ExperienceSettings
               {
                   Id = 1,
                   StartDate = new DateTime(2006, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                   Description = "Company Experience Start Date"
               }
           );

            

            //modelBuilder.Entity<Admin>().ToTable("Admins");

        }
    }
}
