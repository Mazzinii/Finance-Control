namespace Person.Models
{
    public class TransationModel
    {
        public Guid Id { get; init; }
        public string Description { get; private set; }
        public int Value { get; private set; }
        public DateTime Date{ get;  private set; }
        public Guid PersonId { get; set; }
        public PersonModel Person { get; set; } = null!;

        public TransationModel(string description ,int value, DateTime date, Guid personId)
        {
            Id = Guid.NewGuid();
            Description = description;
            Value = value;
            Date = date;
            PersonId = personId;
        }

        public void ChangeAttributes(string description, int value, DateTime date)
        {
            if (description != null) Description = description;
            if (value != default) Value = value;
            if (date != default) Date = date;
        }
    }
}
