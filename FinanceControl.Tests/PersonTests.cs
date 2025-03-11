using FinanceControl.Tests.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Person.Models;
using PersonTransation;

namespace FinanceControl.Tests
{
    public class PersonTests
    {
        [Fact]
        public async void CreatePerson()
        {
            //Arrange
            string name = "Luiz";
            string email = "luizeduardomazzini@gmail.com";
            string password = "Pokemom2025@";
            var person = new PersonModel(name, email, password);

            await using var context = new MockDb().CreateDbContext();

            //Act
            var result = PersonEndpoint.AddPerson(person, context);


            //Assert
            Assert.IsType<Created<PersonModel>>(result);
            Assert.NotNull(person);
            Assert.NotNull(person.Name);
            Assert.NotNull(person.Password);
            Assert.NotNull(person.Email);
            Assert.NotEmpty(context.People);
            Assert.Collection(context.People, person =>
            {
                Assert.Equal(name, person.Name);
                Assert.Equal(email, person.Email);
            });
            
            



        }
    }
}