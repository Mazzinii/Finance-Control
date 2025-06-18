using AutoMapper;
using PersonTransation.Models.DTOs;
using PersonTransation.Models.Entities;

namespace FinanceControl.Tests.Service
{
    public class UserServiceTests
    {

        private readonly UserService _service;
        private readonly Mock<IMapper> _mapper;
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly Faker _faker1 = new Faker("pt_BR");

        public UserServiceTests()
        {
            _mapper = new Mock<IMapper>();

            //configurando o mapper para fazer a conversão correta List<>

            _mapper.Setup(x => x.Map<List<UserDTO>>(It.IsAny<List<UserModel>>()))
                    .Returns((List<UserModel> sourceList) => 
                    sourceList.Select( user => new UserDTO
                    {
                        Name = user.Name,
                        Email = user.Email,
                        
                    }).ToList());


            _service = new UserService(_mapper.Object);
        }

        //make tests to check wrong parameters if they returns exception
        //check if class returns when data is wrong 
        //check if parameters are null
        //make tests more indepenent


        [Fact]
        public async Task Create_GivenAllParameters_ThenShoulInsertPerson()
        {
            // Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;


            var person = new UserModel(name, email, password);
            var context = new MockDb().CreateDbContext();

            // Act
            await _service.Create(person, context);

            // Assert
            Assert.NotNull(person);
            Assert.NotNull(person.Name);
            Assert.NotNull(person.Email);
            Assert.NotNull(person.Password);
            Assert.Collection(context.Users, person =>
            {
                Assert.Equal(name, person.Name);
                Assert.Equal(email, person.Email);
            });


        }

        [Fact]
        public async Task Create_GivenNullPersonOrNullContext_ThenShouldReturnBadRequest()
        {
            // Arrange
            UserModel person = null;
            var context = new MockDb().CreateDbContext();

            // Act
            var expected = await _service.Create(person, context);

            // Assert
            Assert.Null(person);

            var badRequestResult = Assert.IsType<BadRequest<string>>(expected);

            Assert.Equal("Invalid Data", badRequestResult.Value);
           


        }

        [Fact]
        public async Task Create_GivenEmailAlredyRegistered_ThenShouldReturnBadRequest()
        {
            //Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;

            string name1 = _faker1.Person.FullName;
            string email1 = email;
            string password1 = _faker1.Person.Avatar;


            var person = new UserModel(name, email, password);
            var newPerson = new UserModel(name1, email1, password1);
            var context = new MockDb().CreateDbContext();

            // Act
            await _service.Create(person, context);
            var expected = await _service.Create(newPerson, context);

            // Assert
            var badRequestResult = Assert.IsType<BadRequest<string>>(expected);

            Assert.Equal("Email is alredy registered", badRequestResult.Value);



        }

        [Fact] 
        public async Task CheckEmail_WhenGivenAllParameters_ThenShouldReturnTrueIfEmailIsAlredyRegistered()
        {
            // Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;


            var person = new UserModel(name, email, password);
            var context = new MockDb().CreateDbContext();

            // Act
            await _service.Create(person, context);
           var result = await _service.CheckEmail(person, context);

            // Assert
            Assert.True(result);

        }

        [Fact]
        public async Task CheckEmail_WhenGivenAllParameters_ThenShouldReturnFalseIfEmailIsNotRegistered()
        {
            // Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;

            string newName = _faker1.Person.FullName;
            string newEmail = _faker1.Person.Email;
            string newPassword = _faker1.Person.UserName;


            var person = new UserModel(name, email, password);
            var newPerson = new UserModel(newName, newEmail, newPassword);
            var context = new MockDb().CreateDbContext();

            // Act
            await _service.Create(person, context);
            var result = await _service.CheckEmail(newPerson, context);

            // Assert
            Assert.False(result);

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
            var person = new UserModel(name, email, password);
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
            var person = new UserModel(name, email, password);
            var person2 = new UserModel(name1, email1, password1);
            await _service.Create(person, context);
            await _service.Create(person2, context);
            var result = await _service.Get(context, 1, 2);

            //Assert
            Assert.NotNull(result);

            //Desserializa o conteudo do result
            var okResult = (Ok<List<UserDTO>>)result;
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
            var person = new UserModel(name, email, password);
            var patchPerson = new UserModel(newName, newEmail, newPassword);
            await _service.Create(person, context);
            await _service.Patch(patchPerson, context,person.Id);

            //Assert
            Assert.NotNull(person);
            Assert.Equal(newName, person.Name);
            Assert.Equal(newEmail, person.Email);
        }

        [Fact]
        public async Task Patch_WhenGivenInvalidId_ThenShouldReturnBadRequest()
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
            var person = new UserModel(name, email, password);
            var invalidPersonId = new UserModel(newName, newEmail, newPassword);
            await _service.Create(person, context);

            var expected = await _service.Patch(person, context, invalidPersonId.Id);

            //Assert
            var badRequestResult = Assert.IsType<BadRequest<string>>(expected);

            Assert.Equal("Invalid Id", badRequestResult.Value);

        }


        [Fact]
        public async Task Delete_WhenGivenAllValidId_ThenShouldDeletePerson()
        {

            //Arrange
            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;

            var context = new MockDb().CreateDbContext();

            //Act

            var person = new UserModel(name, email, password);
            await _service.Create(person, context);
            await _service.Delete(context, person.Id);

            //Assert
            var hasPerson = await context.Users.FirstOrDefaultAsync(x => x.Id == person.Id);
            Assert.Null(hasPerson);
        }

        [Fact]
        public async Task Delete_WhenGivenInvalidID_ThenSHouldReturnBadRequest()
        {
            //Arrange
            var context = new MockDb().CreateDbContext();
            var newGuid = _faker.Random.Guid();

            //Act
            var expected = await _service.Delete(context, newGuid);

            //Assert
            var actualRequest = Assert.IsType<BadRequest<string>>(expected);

            Assert.Equal("Invalid Id", actualRequest.Value);

        }

    }
}