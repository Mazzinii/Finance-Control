using System.Net.Http.Json;
using System.Net;
using FinanceControl.Tests.Helpers;
using Microsoft.AspNetCore.Http.HttpResults;
using Person.Models;
using Person.Models.Requests;
using PersonTransation.Services;

namespace FinanceControl.Tests
{
    public class PersonTests
    {
        private static readonly HttpClient client = new HttpClient();

        [Fact]
        public async Task CreatePerson_ReturnsCreatedResponse_WhenEmailIsUnique()
        {
            /*
            string name = "John Doe";
            string email = "john.doe@example.com";
            string password = "password123";

            // Arrang
            var request = new PersonRequest(name, email, password); 

            // Act
            var response = await client.PostAsJsonAsync("/Create", request);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var person = await response.Content.ReadFromJsonAsync<PersonModel>();
            Assert.NotNull(person);
            Assert.Equal(request.Name, person.Name);
            Assert.Equal(request.Email, person.Email);
            */
        }



    }
    }
}