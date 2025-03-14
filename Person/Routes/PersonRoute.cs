using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;
using Person.Models.Requests;
using PersonTransation.Services;

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
                async (PersonRequest req, PersonTransationContext context, PersonService service) =>
                {
                    var person = new PersonModel(req.Name, req.Email, req.Password);

                    return await service.Create(person, context);              
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
                async (int page,PersonTransationContext context, PersonService service) =>
                {

                     return await service.Get(context, page);

                });

            //Update
            routes.MapPatch("{id:Guid}",
                async (Guid id, PersonRequest req, PersonTransationContext context, PersonService service) =>
                {
                    var person = new PersonModel(req.Name,req.Email,req.Password);

                    return await service.Patch(person, context,id);

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
