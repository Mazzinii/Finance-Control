﻿using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Models;
using Person.Models.Requests;
using PersonTransation.Services;

namespace Person.Routes
{
    public static class TransationRoute
    {
        private static readonly TransationService _service = new TransationService();
        private static readonly PersonTransationContext _context = new PersonTransationContext();

        public static void TransationRoutes(this WebApplication app)
        {
            var routes = app.MapGroup("Transition");

            //Create
            routes.MapPost("Create", 
                async (TransationRequest request) =>
                {
                    var transation = new TransationModel(request.Description, request.Status, request.Value, request.Date, request.PersonId);
                    return await _service.Create(transation, _context);
                
                })
                .RequireAuthorization();

            //Read
            routes.MapGet("{id:Guid}",
                async (Guid id, int pageNumber, int pageQuantity) =>
                {
                    return await _service.Get(_context, pageNumber, pageQuantity);
                })
                .RequireAuthorization();

            //Update
            routes.MapPatch("{id:guid}",
                async (Guid id, TransationUpdateRequest req, PersonTransationContext context) =>
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
                async (Guid id, PersonTransationContext context) =>
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
