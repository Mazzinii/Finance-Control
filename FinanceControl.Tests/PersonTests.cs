using FinanceControl.Tests.Helpers;
using Microsoft.Extensions.Configuration;
using Moq;
using Person.Models;
using Person.Models.Requests;
using Person.Services;
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
            await service.Create(person, context);
 
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

        [Fact]
        public async Task LoginPerson()
        {
            //Arrange 
            string name = "Luiz Gallan";
            string email = "luizmazzini@gmail.com";
            string password = "1234";

            var context = new MockDb().CreateDbContext();
            var serviceAdd = new PersonService();
            var passwordHasher = new PasswordHasherService();

            //setando a IConfig
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["Key:Jwt"]).Returns("k9Lm2nQp4rT7vXw8yZ1aB3cD5eF6gH8jJ9lK0oP1iU2sV3bN4tM5xW6qY7zA");

            //Act
            var person = new PersonModel(name, email, password);
            var resultAdd = await serviceAdd.Create(person, context);
            var login = new LoginHashRequests.LoginRequest(email, password);
            var token = new TokenService(configMock.Object);
            var serviceLogin = new LoginHashRequests(context, passwordHasher, token);
            var resultLogin = await serviceLogin.Handle(login);

            //Assert
            Assert.NotNull(person.Name);
            Assert.NotNull(person.Email);
            Assert.NotNull(person.Password);
            Assert.DoesNotContain("The password is incorrect", resultLogin);
            Assert.DoesNotContain("The user was not found", resultLogin);

            





        }


    }
}