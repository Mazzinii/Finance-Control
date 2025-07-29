using Microsoft.EntityFrameworkCore;
using PersonTransation.Models.Entities;


namespace Person.Data
{
    public class PersonTransactionContext : DbContext
    {
        public  DbSet<UserModel> Users { get; set; }
        public  DbSet<TransactionModel> Transations { get; set; }

        private readonly IConfiguration _configuration;

        public PersonTransactionContext() { }

        public PersonTransactionContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public PersonTransactionContext(DbContextOptions<PersonTransactionContext> options) : base(options) { }
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
