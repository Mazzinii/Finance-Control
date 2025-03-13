using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

namespace PersonTransation.Services
{
    public class PersonService
    {

        public async Task<IResult> AddPerson(PersonModel person, PersonTransationContext context)
        {
            //validando entrada
            if(person == null || context == null)
            {
                return Results.BadRequest("Invalid Data");
            }
            try
            {
                //verificando se o email já esta registrado
                bool hasEmail = await CheckingEmail(person, context);

                if (hasEmail)
                {
                    return Results.BadRequest("Email is alredy registered");
                }
                
                else
                {
                    // adiciona pessoa no banco de dados e salva alteraçõe
                    context.Add(person);
                    await context.SaveChangesAsync();
                    //retorna response 201 created com a localização do recurso criado
                    return TypedResults.Created($"/Person/{person.Id}", person);
                }
            }
            // trata exceções e retorna um erro interno 
            catch(Exception ex)
            {
                return Results.Problem($"Error: {ex.Message}");

            }
              
        }

        public async Task<bool> CheckingEmail(PersonModel person, PersonTransationContext context)
        {
            var hasEmail = await context.People.FirstOrDefaultAsync(x => x.Email == person.Email);

            if (hasEmail != null) return true;
            else return false;
        }


        public async Task<IResult> GetPerson (PersonTransationContext context, int itemperpage)
        {
            var person = await context.People.ToListAsync();

            var pagination = person.Skip(0).Take(itemperpage);

            return TypedResults.Ok(pagination);
        }
    }
}
