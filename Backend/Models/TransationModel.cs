namespace PersonTransation.Models
{
    public class TransationModel
    {
        public Guid Id { get; init; }
        public string Description { get; private set; }
        public string Status { get; private set; }
        public int Value { get; private set; }
        public DateTime Date{ get;  private set; }
        public Guid UsersId { get; set; }
        public UsersModel Users { get; set; } = null!;

        public TransationModel(string description, string status ,int value, DateTime date, Guid usersId)
        {
            Id = Guid.NewGuid();
            Description = description;
            Status = status;
            Value = value;
            Date = date;
            UsersId = usersId;
        }

        public void ChangeAttributes(string description, int value, DateTime date)
        {
            if (description != null) Description = description;
            if (value != default) Value = value;
            if (date != default) Date = date;
        }
    }
}
