using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PersonTransation.Models;


namespace Person.Data
{
    public class PersonTransationContext : DbContext
    {
        public  DbSet<UsersModel> Users { get; set; }
        public  DbSet<TransationModel> Transations { get; set; }
       
        public PersonTransationContext() { }

        public PersonTransationContext(DbContextOptions<PersonTransationContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = users.sqlite");
                base.OnConfiguring(optionsBuilder);
            }
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransationModel>()
                .HasOne(e => e.Users)
                .WithMany(e => e.Transations)
                .HasForeignKey(e => e.UserId)
                .IsRequired();
        }


    }
}
