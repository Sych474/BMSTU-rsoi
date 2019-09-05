using Microsoft.EntityFrameworkCore;

namespace CharactersHub.Models.Database
{
    public class CharactersHubDbContext : DbContext
    {
        public CharactersHubDbContext(DbContextOptions<CharactersHubDbContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Character>(entity =>
                entity.HasKey(c => c.Id)
            );
        }
    }
}
