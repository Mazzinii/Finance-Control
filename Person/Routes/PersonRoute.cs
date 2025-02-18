
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
            var routeDelete = app.MapGroup("Person/Delete");


            //Create
            routes.MapPost("Create",
                async (PersonRequest request, PersonContext context) =>
                {
                    var person = new PersonModel(request.name, request.email, request.password);
                    await context.AddAsync(person);
                    await context.SaveChangesAsync();
                });


            //Read
            routeCreate.MapGet( "{id:Guid}",
                async (Guid id,  PersonContext context) =>
                {

                    var person = await context.People.FirstOrDefaultAsync(x => x.Id == id);

                    if (person == null)
                        return Results.NotFound();

                    else return Results.Ok(person);

                });

            //Update
            routeUpdate.MapPatch("{id:Guid}",
                async (Guid id, PersonRequest req, PersonContext context) =>
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
            routeDelete.MapDelete("{id:Guid}",
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
