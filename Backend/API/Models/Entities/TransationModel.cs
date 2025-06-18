namespace PersonTransation.Models.Entities
{
    public class TransationModel
    {
        public Guid Id { get; init; }
        public string Description { get; private set; }
        public string Status { get; private set; }
        public int Value { get; private set; }
        public DateTime? Date { get; private set; }
        public Guid UserId { get; set; }
        public UserModel Users { get; set; } = null!;

        public TransationModel(string description, string status, int value, DateTime? date, Guid userId)
        {
            Id = Guid.NewGuid();
            Description = description;
            Status = status;
            Value = value;
            Date = date;
            UserId = userId;
        }

        public TransationModel(string description, string status, int value, DateTime? date = null)
        {
            Id = Guid.NewGuid();
            Description = description;
            Status = status;
            Value = value;
            Date = date;
        }

        public void ChangeAttributes(string description, int value, DateTime? date = null)
        {
            if (description != null) Description = description;
            if (value != default) Value = value;
            if (date != default) Date = date;
        }
    }
}
