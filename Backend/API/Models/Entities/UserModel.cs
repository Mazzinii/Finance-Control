using Person.Services;

namespace PersonTransation.Models.Entities
{
    public class UserModel
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public List<TransationModel> Transations { get; set; } = new List<TransationModel>();

        private readonly PasswordHasherService passwordHash = new PasswordHasherService();

        public UserModel(string name, string email, string password)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            Password = passwordHash.Hash(password);
        }

        public UserModel(string email, string password)
        {
            Email = email;
            Password = password;
        }


        public void ChangeAttributes(string name, string password, string email)
        {
            if (name != null) Name = name;
            if (password != null) Password = passwordHash.Hash(password);
            if (email != null) Email = email;

        }
    }
}
