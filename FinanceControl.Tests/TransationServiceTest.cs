

using Bogus.DataSets;
using Bogus;

namespace FinanceControl.Tests
{


    public class TransationServiceTest
    {
        private readonly TransationService _service = new TransationService();
        private readonly Faker _faker = new Faker("pt_BR");
        private readonly Faker _faker1 = new Faker("pt_BR");

        // List <string> transationStatus = new List<string>() { "entrada", "saida"};


        [Fact]
        public async Task Create_GivenAllParameters_ThenShoulInsertTransation()
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
            Assert.NotNull(transation.PersonId);
            Assert.Collection(context.Transation, transation =>
            {
                Assert.Equal(description, transation.Description);
                Assert.Equal(status, transation.Status);
                Assert.Equal(value, transation.Value);
                Assert.Equal(date, transation.Date);
                Assert.Equal(personId, transation.PersonId);
            });
        }

        [Fact]
        public async Task Get_GienAllValidParameters_ThenShoulReturnListofTransation()
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
            var transation  = new TransationModel(description, status, value, date, personId);
            var transation1 = new TransationModel(description1, status1, value1, date1, personId1);
            await _service.Create(transation,context);
            await _service.Create(transation1,context);
            var result = await _service.Get(context, 1, 2);


            //Assert
            Assert.NotNull(result);

            //Desserializa o conteudo do result
            var okResult = (Ok<List<TransationModel>>)result;
            var transations = okResult.Value;

            Assert.NotNull(transations);
            Assert.Equal(expected, transations.Count);
        }
    }
}
