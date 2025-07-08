namespace PersonTransation.Models.DTOs
{
    public class LoginDTO
    {
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public string Token { get; set; }
    }
}
