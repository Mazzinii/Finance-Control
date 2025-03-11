

using Microsoft.AspNetCore.Http.HttpResults;
using Person.Data;
using Person.Models;

namespace PersonTransation
{
    public class PersonEndpoint
    {
        public static Created<PersonModel> AddPerson(PersonModel person, PersonTransationContext context)
        {
            context.Add(person);
            context.SaveChanges();

            return TypedResults.Created($"/Person/{person.Id}", person);        }
    }
}
