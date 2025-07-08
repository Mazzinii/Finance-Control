namespace PersonTransation.Models.DTOs
{
    public class TransationDTO
    {
        public string Description { get;  set; }
        public string Status { get;  set; }
        public int Value { get;  set; }
        public DateTime? Date { private get;  set; }
        public string? FormatedDate => Date?.ToString("dd/MM/yy");
    }
}
