using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;
using Person.Models.Requests;

namespace Person.Routes
{
    public static class TransationRoute
    {
        public static void TransationRoutes(this WebApplication app)
        {
            var routes = app.MapGroup("Transition");


            //Create
            routes.MapPost("Create", async (TransationRequest request, PersonContext context) =>
            {
                var transation = new TransationModel(request.Value, request.Date, request.PersonId);
                await context.AddAsync(transation);
                await context.SaveChangesAsync();
            })
                .RequireAuthorization();

            //Read
            routes.MapGet("{id:Guid}",
                async (Guid id, PersonContext context) =>
                {
                    var transation = await context.Transation.Where(x => x.PersonId == id).ToListAsync();
                    if (transation == null) return Results.NotFound();
                    else return Results.Ok(transation);
                });

            //Update

            //Delete




            
        }
    }
}
