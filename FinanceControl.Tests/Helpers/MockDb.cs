﻿using Microsoft.EntityFrameworkCore;
using Person.Data;


namespace FinanceControl.Tests.Helpers
{
    public class MockDb : IDbContextFactory<PersonTransationContext>
    {

        public PersonTransationContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<PersonTransationContext>()
                .UseInMemoryDatabase($"InMemoryTestDb - {DateTime.Now.ToFileTimeUtc()}")
                .Options;

            var context = new PersonTransationContext(options);

            return context;
        }
    }
}
