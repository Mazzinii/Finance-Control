namespace PersonTransation.Models.DTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public string Description { get;  set; }
        public string Status { get;  set; }
        public decimal Value { get;  set; }
        public DateTime Date {  get;  set; }
        public string? FormatedDate => Date.ToString("dd/MM/yy");
        public string? EditDate => Date.ToString("yyyy-MM-dd");
    }
}
