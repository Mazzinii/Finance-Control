using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using PersonTransation.Models.DTOs;
using PersonTransation.Models.Entities;

namespace PersonTransation.Services
{
    public class UserService : IModel<UserModel> 
    {
        //adicionar requests nos parametros para menor repetição de código
        private readonly IMapper _mapper;

        public UserService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<IResult> Create(UserModel person, PersonTransationContext context)
        {
            //validando entrada
            if(person == null || context == null)
            {
                return TypedResults.BadRequest("Invalid Data");
            }
            try
            {
                //verificando se o email já esta registrado
                bool hasEmail = await CheckEmail(person, context);

                if (hasEmail)
                {
                    return TypedResults.BadRequest("Email is alredy registered");
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
                return TypedResults.Problem($"Error: {ex.Message}");

            }
              
        }

        public async Task<bool> CheckEmail(UserModel person, PersonTransationContext context)
        {
            return await context.Users.AnyAsync(x => x.Email == person.Email);

        }


        public async Task<IResult> Get(PersonTransationContext context, int page, int limit)
        {
            try
            {
                var person = await context.Users.ToListAsync();

                var pagination = person.Skip((page - 1) * limit).Take(limit).ToList();

                
                var paginationView = _mapper.Map<List<UserDTO>>(pagination);

                return TypedResults.Ok(paginationView);
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }
        }

         
        public async Task<IResult> Patch(UserModel person, PersonTransationContext context, Guid id)
        {
            try
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
            catch(Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message} ");
            }
        }

        public async Task<IResult> Delete(PersonTransationContext context, Guid id)
        {
            try
            {
                var hasPerson = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if (hasPerson == null)
                    return TypedResults.BadRequest("Invalid Id");
                else
                {
                    context.Remove(hasPerson);
                    await context.SaveChangesAsync();
                    return TypedResults.Ok("Deleted");
                }
            }
            catch (Exception ex)
            {
                return TypedResults.Problem($"Error: {ex.Message}");
            }
        }


    }
}
