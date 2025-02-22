using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Services;

namespace Person.Models.Requests
{
    public class LoginHashRequests
    {
        private readonly PersonContext _personContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenService _tokenService;

        public LoginHashRequests(PersonContext personContext, IPasswordHasher passwordHasher, TokenService tokenService)
        {
            _personContext = personContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public record LoginRequest(string Email, string Password);

        public async Task<string> Handle(LoginRequest req)
        {
            PersonModel? person = await _personContext.People
                .FirstOrDefaultAsync(x => x.Email == req.Email);

            if (person == null)
                throw new Exception("The user was not found");

            bool verified = _passwordHasher.Verify(req.Password, person.Password);

            if (!verified)
                throw new Exception("The password is incorrect");

            string token = _tokenService.GenerateToken(person);

            return token;
        }
    }
}
