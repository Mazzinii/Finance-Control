using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;
using Person.Models.Requests;
using Person.Services;

namespace Person.Routes
{
    public static class PersonRoute
    {
        public static void PersonRoutes(this WebApplication app)
        {
            //mapping routes
            var routes = app.MapGroup("Person");



            //Create
            routes.MapPost("Create",
                async (PersonRequest request, PersonTransationContext context) =>
                {
                    
                    var person = new PersonModel(request.Name, request.Email, request.Password);
                    
                    //checking if the email is not registered 
                    var hasEmail = await context.People.FirstOrDefaultAsync(x => x.Email == request.Email);

                    if (hasEmail != null) return Results.BadRequest("Email is alredy registered");
                    
                   else
                    {
                        context.Add(person);
                        await context.SaveChangesAsync();
                        return TypedResults.Created($"/Person/{person.Id}", person);
                    } 
                    
                });

            //Login
            routes.MapPost("Login",
                async (LoginHashRequests.LoginRequest req, LoginHashRequests login) =>
                {
                    var person = await login.Handle(req);
                    return Results.Ok(person);

                });

            //Read
            routes.MapGet("Read",
                async (  PersonTransationContext context) =>
                {

                    var person = await context.People.ToListAsync();

                    return Results.Ok(person);

                });

            //Update
            routes.MapPatch("{id:Guid}",
                async (Guid id, PersonRequest req, PersonTransationContext context) =>
                {
                    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                    if (person == null)
                        return Results.NotFound();
                    else
                    {
                        person.ChangeAttributes(req.Name, req.Password, req.Email);
                        await context.SaveChangesAsync();
                        return Results.Ok(person);
                    }

                })
                .RequireAuthorization();

            //Delete
            routes.MapDelete("{id:Guid}",
                async (Guid id, PersonTransationContext context) =>
                {
                    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                    if (person == null)
                        return Results.NotFound();
                    else
                    {
                        context.Remove(person);
                        await context.SaveChangesAsync();
                        return Results.Ok(person);
                    }
                })
                .RequireAuthorization();




        }
    }
}
