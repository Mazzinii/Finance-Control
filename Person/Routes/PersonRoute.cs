using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

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
                async (PersonRequest request, PersonContext context) =>
                {
                    
                    var person = new PersonModel(request.name, request.email, request.password);
                    await context.AddAsync(person);
                    await context.SaveChangesAsync();
                });

            //Login
            routes.MapPost("Login",
                async (LoginHashRequest.Request req, LoginHashRequest login) =>
                {
                    var person = await login.Handle(req);

                });

            //Read
            routes.MapGet( "{id:Guid}",
                async (Guid id,  PersonContext context) =>
                {

                    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                    if (person == null)
                        return Results.NotFound();

                    else return Results.Ok(person);

                });

            //Update
            routes.MapPatch("{id:Guid}",
                async (Guid id,PersonRequest req, PersonContext context) =>
                {
                    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                    if (person == null)
                        return Results.NotFound();
                    else
                    {
                        person.ChangeAttributes(req.name,req.password,req.email);
                        await context.SaveChangesAsync();
                        return Results.Ok(person);
                    }

                });

            //Delete
            routes.MapDelete("{id:Guid}",
                async (Guid id, PersonContext context) =>
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
                });



        }
    }
}
