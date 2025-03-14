using FinanceControl.Tests.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Moq;
using Person.Models;
using Person.Models.Requests;
using Person.Services;
using PersonTransation.Services;

namespace FinanceControl.Tests
{
    public class PersonTests
    {

        private readonly PersonService _service = new PersonService();

        [Fact]
        public async Task CreatePerson()
        {
            // Arrange
            string name = "John Doe";
            string email = "john.doe@example.com";
            string password = "password123";

        
            var person = new PersonModel(name, email, password);
            var context = new MockDb().CreateDbContext();

            // Act
            await _service.Create(person, context);
 
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
            var passwordHasher = new PasswordHasherService();

            //setando a IConfig
            var configMock = new Mock<IConfiguration>();
            configMock.Setup(c => c["Key:Jwt"]).Returns("k9Lm2nQp4rT7vXw8yZ1aB3cD5eF6gH8jJ9lK0oP1iU2sV3bN4tM5xW6qY7zA");

            //Act
            var person = new PersonModel(name, email, password);
            var resultAdd = await _service.Create(person, context);
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

        [Fact]
        public async Task GetPerson()
        {
            //Arrange
            string name = "John Doe";
            string email = "john.doe@example.com";
            string password = "password123";

            string name2 = "Luiz Gallan";
            string email2 = "luizmazzini@gmail.com";
            string password2 = "1234";

            int expected = 2;

            var person = new PersonModel(name, email, password);
            var peron2 = new PersonModel(name2, email2, password2);
            var context = new MockDb().CreateDbContext();

            // Act
            await _service.Create(person, context);
            await _service.Create(peron2, context);
            var result = await _service.Get(context, 1, 2);

            //Assert
            Assert.NotNull(result);

            //Desserializa o conteudo da resposta
            var okResult = (Ok<List<PersonModel>>)result;
            var people = okResult.Value;

            Assert.NotNull(people);
            Assert.Equal(expected, people.Count);

        }

        [Fact]
        public async Task PatchPerson()
        {
            //Arrange
            string name = "John Doe";
            string email = "john.doe@example.com";
            string password = "password123";

            string newName = "Luiz Gallan";
            string newEmail = "luizmazzini@gmail.com";
            string newPassword = "1234";

            var person = new PersonModel(name,email,password);
            var patchPerson = new PersonModel(newName, newEmail, newPassword);
            var context = new MockDb().CreateDbContext();

            //Act
            await _service.Create(person,context);
            await _service.Patch(patchPerson,context,person.Id);

            //Assert
            Assert.NotNull(person);
            Assert.Equal(newName, person.Name);
            Assert.Equal(newEmail, person.Email);
        }




    }
}