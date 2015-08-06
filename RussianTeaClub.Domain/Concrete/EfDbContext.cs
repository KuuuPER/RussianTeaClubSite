using System.Data.Entity;
using System.Data.SqlClient;
using RussianTeaClub.Domain.Entities;

namespace RussianTeaClub.Domain.Concrete
{
    public class EfDbContext : DbContext
    {
        public EfDbContext() : base()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfDbContext, Migrations.Configuration>());
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<ContentImage> Images { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Article>()
                .HasMany(a => a.ImagesData)
                .WithOptional(i => i.Article)
                .HasForeignKey(a => a.ArticleId)
                .WillCascadeOnDelete(true);
        }
    }
}
