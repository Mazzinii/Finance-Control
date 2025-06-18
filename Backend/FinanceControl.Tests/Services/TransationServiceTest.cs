using PersonTransation.Models.Entities;

namespace FinanceControl.Tests.Service
{


    public class TransationServiceTest
    {
        private readonly TransationService _service = new TransationService();
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly Faker _faker1 = new Faker("pt_BR");

        // List <string> transationStatus = new List<string>() { "entrada", "saida"};
        //make tests to check wong parameters if they returns exception
        //check if is possible to create two same transations and get by id
        //check if parameters are null


        [Fact]
        public async Task Create_WhenGivenAllParameters_ThenShoulInsertTransation()
        {
            //Arrange
            string description = _faker.Finance.AccountName();
            string status = "Entrada";
            int value = _faker.Random.Int();
            DateTime date = _faker.Date.Past();
            var personId = _faker.Random.Guid();

            var context = new MockDb().CreateDbContext();

            //Act
            var transation = new TransationModel(description, status, value, date, personId);
            await _service.Create(transation, context);


            //Assert
            Assert.NotNull(transation);
            Assert.NotNull(transation.Description);
            Assert.NotNull(transation.Status);
            Assert.Collection(context.Transations, transation =>
            {
                Assert.Equal(description, transation.Description);
                Assert.Equal(status, transation.Status);
                Assert.Equal(value, transation.Value);
                Assert.Equal(date, transation.Date);
                Assert.Equal(personId, transation.UserId);
            });
        }

        [Fact]
        public async Task GetId_WhenGivenAllValidParameters_ThenShoulReturnGuid()
        {
            //Arrange
            string description = _faker.Finance.AccountName();
            string status = "Entrada";
            int value = _faker.Random.Int();
            DateTime date = _faker.Date.Past();
            var personId = _faker.Random.Guid();

            var context = new MockDb().CreateDbContext();

            //Act
            var transation = new TransationModel(description, status, value, date, personId);
            await _service.Create(transation, context);
            var id = await _service.GetId(transation, context);

            //Assert
            Assert.NotNull(transation);

            var okResult = (Ok<Guid>)id;
            var result = okResult.Value;

            Assert.Equal(transation.Id, result);
          
        }

        [Fact]
        public async Task GetId_WhenGivenInvalidParameters_ThenShoulReturnNotFound()
        {
            string description = _faker.Finance.AccountName();
            string status = "Entrada";
            int value = _faker.Random.Int();
            DateTime date = _faker.Date.Past();
            var personId = _faker.Random.Guid();

            var context = new MockDb().CreateDbContext();

            //Act
            var transation = new TransationModel(description, status, value, date, personId);
            var id = await _service.GetId(transation, context);

            //Assert
            Assert.NotNull(transation);

            var notFound = (NotFound<string>)id;
            var result = notFound.Value;

            Assert.Equal("transation not found", result);

        }

        [Fact]
        public async Task Get_WhenGivenAllValidParameters_ThenShoulReturnListofTransation()
        {
            //Arrange
            string description = _faker.Finance.AccountName();
            string status = "Entrada";
            int value = _faker.Random.Int();
            DateTime date = _faker.Date.Past();
            var personId = _faker.Random.Guid();

            string name = _faker.Person.FullName;
            string email = _faker.Person.Email;
            string password = _faker.Person.UserName;

            string description1 = _faker1.Finance.AccountName();
            string status1 = "Entrada";
            int value1 = _faker1.Random.Int();
            DateTime date1 = _faker1.Date.Past();
            var personId1 = _faker1.Random.Guid();

            string name1 = _faker1.Person.FullName;
            string email1 = _faker1.Person.Email;
            string password1 = _faker1.Person.UserName;

            var context = new MockDb().CreateDbContext();

            int expected = 1;

            //Act
            var user = new UserModel(name, email, password);
            var user1 = new UserModel(name1, email1, password1);
            var transation = new TransationModel(description, status, value, date, user.Id);
            var transation1 = new TransationModel(description1, status1, value1, date1, user1.Id);
            await _service.Create(transation, context);
            await _service.Create(transation1, context);
            var result = await _service.Get(context,user.Id, 1, 1);


            //Assert
            Assert.NotNull(result);

            //Desserializa o conteudo do result
            var okResult = (Ok<List<TransationModel>>)result;
            var transations = okResult.Value;

            Assert.NotNull(transations);
            Assert.Equal(expected, transations.Count);
        }

       
        [Fact]
        public async Task Patch_WhenGivelAllParameters_ThenShouldUpdateTransation()
        {
            //Arrange
            string description = _faker.Finance.AccountName();
            string status = "Entrada";
            int value = _faker.Random.Int();
            DateTime date = _faker.Date.Past();
            var personId = _faker.Random.Guid();

            string newDescription = _faker1.Finance.AccountName();
            string newStatus = "Entrada";
            int newValue = _faker1.Random.Int();
            DateTime newDate = _faker1.Date.Past();
            var newPersonId = _faker1.Random.Guid();

            var context = new MockDb().CreateDbContext();

            //Act
            var transation = new TransationModel(description, status, value, date, personId);
            var newTransation = new TransationModel(newDescription, newStatus, newValue, newDate, newPersonId);
            await _service.Create(transation, context);
            await _service.Patch(newTransation,context,transation.Id);

            //Asert
            Assert.NotNull(transation);
            Assert.Equal(newDescription, transation.Description);
            Assert.Equal(newStatus, transation.Status);
            Assert.Equal(newValue, transation.Value);
            Assert.Equal(newDate, transation.Date);
            
            //Person Id dont change
            Assert.Equal(personId,transation.UserId);
        }

        [Fact]
        public async Task Patch_WhenGivenInvalidId_ThenShouldReturnBadRequest()
        {
            //Arrange
            string description = _faker.Finance.AccountName();
            string status = "Entrada";
            int value = _faker.Random.Int();
            DateTime date = _faker.Date.Past();
            var personId = _faker.Random.Guid();

            string newDescription = _faker1.Finance.AccountName();
            string newStatus = "Entrada";
            int newValue = _faker1.Random.Int();
            DateTime newDate = _faker1.Date.Past();
            var newPersonId = _faker1.Random.Guid();

            var context = new MockDb().CreateDbContext();

            //Act
            var transation = new TransationModel(description, status, value, date, personId);
            var newTransation = new TransationModel(newDescription, newStatus, newValue, newDate, newPersonId);
            await _service.Create(transation, context);
            var wrongId = await _service.Patch(newTransation, context, newTransation.Id);

            //Assert
            Assert.NotNull(transation);

            var badRequest = (BadRequest<string>)wrongId;
            var result = badRequest.Value;

            Assert.Equal("Invalid Id", result);
        }

        [Fact]
        public async Task Delete_WhenGivenValidId_ThenShouldDeletePerson()
        {
            //Arrange
            string description = _faker.Finance.AccountName();
            string status = "Entrada";
            int value = _faker.Random.Int();
            DateTime date = _faker.Date.Past();
            var personId = _faker.Random.Guid();

            var context = new MockDb().CreateDbContext();

            //Act
            var transation = new TransationModel(description,status, value, date, personId);
            await _service.Create(transation,context);
            await _service.Delete(context, transation.Id);

            //Assert
            var hasTransation = await context.Transations.FirstOrDefaultAsync(x => x.Id == transation.Id);
            Assert.Null(hasTransation);
        }

        [Fact]
        public async Task Delete_WhenGivenInvalidId_ThenShouldReturnBadRequest()
        {
            //Arrange
            string description = _faker.Finance.AccountName();
            string status = "Entrada";
            int value = _faker.Random.Int();
            DateTime date = _faker.Date.Past();
            var personId = _faker.Random.Guid();

            var newTransationGuid = _faker.Random.Guid(); 

            var context = new MockDb().CreateDbContext();

            //Act
            var transation = new TransationModel(description, status, value, date, personId);
            await _service.Create(transation, context);
            var invalidId = await _service.Delete(context, newTransationGuid);

            //Assert
            Assert.NotNull(transation);

            var badRequest = (BadRequest<string>)invalidId;
            var result = badRequest.Value;

            Assert.Equal("Invalid Id", result);

        }
    }
}
