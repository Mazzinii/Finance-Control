using Microsoft.EntityFrameworkCore;
using PersonTransation.Models.Entities;


namespace Person.Data
{
    public class PersonTransationContext : DbContext
    {
        public  DbSet<UserModel> Users { get; set; }
        public  DbSet<TransationModel> Transations { get; set; }

        private readonly IConfiguration _configuration;

        public PersonTransationContext() { }

        public PersonTransationContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PersonTransationContext(DbContextOptions<PersonTransationContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connectionString = _configuration?.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
                base.OnConfiguring(optionsBuilder);
            }
            
        }



    }
}
