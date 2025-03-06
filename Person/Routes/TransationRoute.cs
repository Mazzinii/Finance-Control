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
            routes.MapPost("Create", 
                async (TransationRequest request, PersonContext context) =>
                {
                var transation = new TransationModel(request.Description, request.Status, request.Value, request.Date, request.PersonId);
                await context.AddAsync(transation);
                await context.SaveChangesAsync();
                return Results.Ok(transation);
                })
                .RequireAuthorization();

            //Read
            routes.MapGet("{id:Guid}",
                async (Guid id, PersonContext context) =>
                {
                    var transation = await context.Transation.Where(x => x.PersonId == id).ToListAsync();
                    if (transation == null) return Results.NotFound();
                    else return Results.Ok(transation);
                })
                .RequireAuthorization();

            //Update
            routes.MapPatch("{id:guid}",
                async (Guid id, TransationUpdateRequest req, PersonContext context) =>
                {
                    var transation = await context.Transation.FirstOrDefaultAsync(x => x.Id == id);

                    if (transation == null)
                        return Results.NotFound();
                    else
                    {
                        transation.ChangeAttributes(req.Description, req.Value, req.Date);
                        await context.SaveChangesAsync();
                        return Results.Ok(transation);
                    }
                })
                .RequireAuthorization();
                    
            //Delete
            routes.MapDelete("{id:guid}", 
                async (Guid id, PersonContext context) =>
                {
                    var transation = context.Transation.FirstOrDefault(x => x.Id == id);

                    if (transation == null)
                        return Results.NotFound();
                    else
                    {
                        context.Remove(transation);
                        await context.SaveChangesAsync();
                        return Results.Ok();
                    }
                })
                .RequireAuthorization();




            
        }
    }
}
