namespace PersonTransation.Models.DTOs
{
    public class TransationDTO
    {
        public string Description { get; private set; }
        public string Status { get; private set; }
        public int Value { get; private set; }
        public DateTime? Date { get; private set; }
    }
}
