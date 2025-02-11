namespace Person.Models
{
    public class PersonModel
    {
        public Guid Id { get; init; }
        public string Name { get; private set; } 
        public string Email { get; private set; } 
        public string Password { get; private set; } 

        public PersonModel(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = password;
        }
    }
}
