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
            Assert.NotNull(transation.Value);
            Assert.NotNull(transation.Date);
            Assert.NotNull(transation.UsersId);
            Assert.Collection(context.Transations, transation =>
            {
                Assert.Equal(description, transation.Description);
                Assert.Equal(status, transation.Status);
                Assert.Equal(value, transation.Value);
                Assert.Equal(date, transation.Date);
                Assert.Equal(personId, transation.UsersId);
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
            Assert.Equal(transation.Id, id);
          
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

            string description1 = _faker1.Finance.AccountName();
            string status1 = "Entrada";
            int value1 = _faker1.Random.Int();
            DateTime date1 = _faker1.Date.Past();
            var personId1 = _faker1.Random.Guid();

            var context = new MockDb().CreateDbContext();

            int expected = 2;

            //Act
            var transation = new TransationModel(description, status, value, date, personId);
            var transation1 = new TransationModel(description1, status1, value1, date1, personId1);
            await _service.Create(transation, context);
            await _service.Create(transation1, context);
            var result = await _service.Get(context, 1, 2);


            //Assert
            Assert.NotNull(result);

            //Desserializa o conteudo do result
            var okResult = (Ok<List<TransationModel>>)result;
            var transations = okResult.Value;

            Assert.NotNull(transations);
            Assert.Equal(expected, transations.Count);
        }
        //melhorar metodo de pesquisa
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
            Assert.Equal(personId,transation.UsersId);
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
    }
}
