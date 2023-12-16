using Microsoft.EntityFrameworkCore;
namespace ZealandBrugerAPI.EDbContext
{
    public class BrugerDbContext : DbContext
    {
        public BrugerDbContext(DbContextOptions<BrugerDbContext> options) : base(options) 
        { 
        
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(@"Data Source=mssql11.unoeuro.com;Initial Catalog=zealandid_dk_db_test;User ID=zealandid_dk;Password=4tn2gwfADdeRB5EGzm6b;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }

        public DbSet<Bruger> bruger {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Lokaler entity
            modelBuilder.Entity<Bruger>()
                .HasKey(b => b.Id); // Assuming Bruger has a property named Id
            modelBuilder.Entity<Bruger>()
                .HasKey(b => b.Admin);

            modelBuilder.Entity<Bruger>()
                .Property(b => b.Brugernavn)
                .IsRequired()
                .HasMaxLength(50); // Example: Set max length for the Brugernavn property
            modelBuilder.Entity<Bruger>()
                .Property(b => b.Password)
                .IsRequired()
                .HasMaxLength(50); // Example: Set max length for the Password property

            
        }

    }
}
