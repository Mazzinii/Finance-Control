namespace FinanceControl.Tests.Service
{
    public class PersonServiceTests
    {

        private readonly PersonService _service = new PersonService();
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly Faker _faker1 = new Faker("pt_BR");
        //tentar adicionar person de maneira global para menor repetição de código
        //testar check email
        //testar caso negativo das ações

        [Fact]
        public async Task Create_GivenAllParameters_ThenShoulInsertPerson()
        {
            // Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;


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
        public async Task CheckEmail_WhenGivenAllParameters_ThenShouldReturnTrueIfEmailIsAlredyRegistered()
        {
            // Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;


            var person = new PersonModel(name, email, password);
            var context = new MockDb().CreateDbContext();

            // Act
            await _service.Create(person, context);
           var result = await _service.CheckEmail(person, context);

            // Assert
            Assert.True(result);

        }

        [Fact]
        public async Task Handle_WhenGivenAllValidParameters_ThenShouldReturnTokenAndId()
        {
            //Arrange 
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.Avatar;

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
        public async Task Get_WhenGivenAllValidParameters_ThenShouldReturnListofPerson()
        {
            //Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.Avatar;

            string name1 = _faker1.Person.FullName;
            string email1 = _faker1.Person.Email;
            string password1 = _faker1.Person.Avatar;

            int expected = 2;


            var context = new MockDb().CreateDbContext();

            // Act
            var person = new PersonModel(name, email, password);
            var person2 = new PersonModel(name1, email1, password1);
            await _service.Create(person, context);
            await _service.Create(person2, context);
            var result = await _service.Get(context, 1, 2);

            //Assert
            Assert.NotNull(result);

            //Desserializa o conteudo do result
            var okResult = (Ok<List<PersonModel>>)result;
            var people = okResult.Value;

            Assert.NotNull(people);
            Assert.Equal(expected, people.Count);

        }

        [Fact]
        public async Task Patch_WhenGivenAllValidParameters_ThenShouldReturnUpdatePerson()
        {
            //Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.Avatar;

            string newName = _faker1.Person.FullName;
            string newEmail = _faker1.Person.Email;
            string newPassword = _faker1.Person.Avatar;

            var context = new MockDb().CreateDbContext();

            //Act
            var person = new PersonModel(name, email, password);
            var patchPerson = new PersonModel(newName, newEmail, newPassword);
            await _service.Create(person, context);
            await _service.Patch(patchPerson, context,person.Id);

            //Assert
            Assert.NotNull(person);
            Assert.Equal(newName, person.Name);
            Assert.Equal(newEmail, person.Email);
        }


        [Fact]
        public async Task Delete_WhenGivenAllValidId_ThenShouldDeletePerson()
        {

            // Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;

            var context = new MockDb().CreateDbContext();

            // Act

            var person = new PersonModel(name, email, password);
            await _service.Create(person, context);
            await _service.Delete(context, person.Id);

            //Assert
            var hasPerson = await context.People.FirstOrDefaultAsync(x => x.Id == person.Id);
            Assert.Null(hasPerson);
        }

    }
}