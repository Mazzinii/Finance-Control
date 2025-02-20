using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Services;

namespace Person.Models
{
    public class LoginHashRequest
    {
        private readonly PersonContext _personContext;
        private readonly IPasswordHasher _passwordHasher;

        public LoginHashRequest(PersonContext personContext, IPasswordHasher passwordHasher)
        {
            _personContext = personContext;
            _passwordHasher = passwordHasher;
        }

        public record Request(string Email, string Password);

        public async Task<PersonModel> Handle(Request req)
        {
            PersonModel? person = await _personContext.People
                .FirstOrDefaultAsync(x => x.Email == req.Email);

            if (person == null)
                throw new Exception("The user was not found");

            bool verified = _passwordHasher.Verify(req.Password, person.Password);

            if (!verified)
                throw new Exception("The password is incorrect");

            return person;
        }
    }
}
