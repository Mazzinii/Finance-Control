using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Services;
using PersonTransation.Models.DTOs;
using PersonTransation.Models.Entities;

namespace PersonTransation.Models.Requests
{
    public class LoginHashRequests
    {
        private readonly PersonTransactionContext _personContext;
        private readonly IPasswordHasher _passwordHasher;
        private readonly TokenService _tokenService;

        public LoginHashRequests(PersonTransactionContext personContext, IPasswordHasher passwordHasher, TokenService tokenService)
        {
            _personContext = personContext;
            _passwordHasher = passwordHasher;
            _tokenService = tokenService;
        }

        public record LoginRequest(string Email, string Password);

        public async Task<LoginDTO> Handle(LoginRequest req)
        {
            UserModel? person = await _personContext.Users
                .FirstOrDefaultAsync(x => x.Email == req.Email);

            if (person == null)
                throw new Exception("The user was not found");

            bool verified = _passwordHasher.Verify(req.Password, person.Password);

            if (!verified)
                throw new Exception("The password is incorrect");

            var loginDTO = _tokenService.GenerateToken(person);

            return loginDTO;
        }
    }
}
