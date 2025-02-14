
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;

namespace Person.Route
{
    public static class PersonRoute
    {
        public static void PersonRoutes(this WebApplication app)
        {
            //mapping routes
            var routes = app.MapGroup("Person");
            var routeCreate = app.MapGroup("Person/Read");
            var routeUpdate = app.MapGroup("Person/Update");


            //Person

            routes.MapPost("Create",
                async (PersonRequest req, PersonContext context) =>
                {
                    var person = new PersonModel(req.name, req.email, req.password);
                    await context.AddAsync(person);
                    await context.SaveChangesAsync();
                });

            routeCreate.MapGet( "{id:Guid}",
                async (Guid id,  PersonContext context) =>
                {

                    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                    if (person == null)
                        return Results.NotFound();

                    else return Results.Ok(person);

                });

            routeUpdate.MapPut("{id:Guid}",
                async (Guid id, UpdateRequest req, PersonContext context) =>
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

        }
    }
}
