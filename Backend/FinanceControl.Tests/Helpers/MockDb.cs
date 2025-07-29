using Microsoft.EntityFrameworkCore;
using Person.Data;


namespace FinanceControl.Tests.Helpers
{
    public class MockDb : IDbContextFactory<PersonTransactionContext>
    {

        public PersonTransactionContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<PersonTransactionContext>()
                .UseInMemoryDatabase($"InMemoryTestDb - {DateTime.Now.ToFileTimeUtc()}")
                .Options;

            return new PersonTransactionContext(options);
 
        }
    }
}
