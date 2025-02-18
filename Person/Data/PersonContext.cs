using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Data
{
    public class PersonContext : DbContext
    {
        public required DbSet<PersonModel> People { get; set; }
        public required DbSet<TransationModel> Transation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = person.sqlite");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransationModel>()
                .HasOne(e => e.Person)
                .WithMany(e => e.Transations)
                .HasForeignKey(e => e.PersonId)
                .IsRequired();
        }


    }
}
