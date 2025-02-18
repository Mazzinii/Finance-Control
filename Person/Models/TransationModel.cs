namespace Person.Models
{
    public class TransationModel
    {
        public Guid Id { get; set; }
        public int Value { get; set; }
        public DateTime Date{ get; set; }
        public Guid PersonId { get; set; }
        public PersonModel Person { get; set; } = null!;

        public TransationModel(int value, DateTime date, Guid personId)
        {
            Id = Guid.NewGuid();
            Value = value;
            Date = date;
            PersonId = personId;
        }
    }
}
