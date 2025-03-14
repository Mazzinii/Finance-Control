using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Person.Models;

namespace Person.Data
{
    public class PersonTransationContext : DbContext
    {
        public  DbSet<PersonModel> People { get; set; }
        public  DbSet<TransationModel> Transation { get; set; }
       
        public PersonTransationContext() { }

        public PersonTransationContext(DbContextOptions<PersonTransationContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite("Data Source = person.sqlite");
                base.OnConfiguring(optionsBuilder);
            }
            
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
