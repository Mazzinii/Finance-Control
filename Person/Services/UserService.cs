using Microsoft.EntityFrameworkCore;
using Person.Data;
using PersonTransation.Models;

namespace PersonTransation.Services
{
    public class UserService : IModel<UsersModel> 
    {
        //adicionar requests nos parametros para menor repetição de código

        public async Task<IResult> Create(UsersModel person, PersonTransationContext context)
        {
            //validando entrada
            if(person == null || context == null)
            {
                return Results.BadRequest("Invalid Data");
            }
            try
            {
                //verificando se o email já esta registrado
                bool hasEmail = await CheckEmail(person, context);

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

        public async Task<bool> CheckEmail(UsersModel person, PersonTransationContext context)
        {
            var hasEmail = await context.Users.FirstOrDefaultAsync(x => x.Email == person.Email);

            if (hasEmail != null) return true;
            else return false;
        }


        public async Task<IResult> Get(PersonTransationContext context, int page, int limit)
        {
            var person = await context.Users.ToListAsync();

            var pagination = person.Skip((page - 1) * limit).Take(limit).ToList();

            return TypedResults.Ok(pagination);
        }

        public async Task<IResult> Patch(UsersModel person, PersonTransationContext context, Guid id)
        {
            var hasId = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if (hasId == null)
                return TypedResults.BadRequest("Invalid Id");
            else
            {
                hasId.ChangeAttributes(person.Name, person.Password, person.Email);
                await context.SaveChangesAsync();
                return TypedResults.Ok(person);
            }
        }

        public async Task<IResult> Delete(PersonTransationContext context, Guid id)
        {
            var hasPerson = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

            if(hasPerson == null)
                return TypedResults.BadRequest("Invalid Id");
            else
            {
                context.Remove(hasPerson);
                await context.SaveChangesAsync();
                return TypedResults.Ok("Deleted");
            }
        }


    }
}
