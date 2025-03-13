using FinanceControl.Tests.Helpers;
using Person.Models;
using PersonTransation.Services;

namespace FinanceControl.Tests
{
    public class PersonTests
    {

        [Fact]
        public async Task CreatePerson()
        {
            // Arrange
            string name = "John Doe";
            string email = "john.doe@example.com";
            string password = "password123";

            var service = new PersonService();
            var person = new PersonModel(name, email, password);
            var context = new MockDb().CreateDbContext();

            // Act
            await service.AddPerson(person, context);
            var personCOunt = context.People.Count();
            Console.WriteLine(personCOunt);

            // Assert
            Assert.NotNull(person);
            Assert.NotNull(person.Name);
            Assert.NotNull(person.Email);
            Assert.NotNull(person.Password);
            Assert.Collection(context.People, person =>
            {
                Assert.Equal(name, person.Name);
                Assert.Equal(email, person.Email);
            });


        }


    }
}