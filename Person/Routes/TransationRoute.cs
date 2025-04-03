using Person.Data;
using PersonTransation.Models;
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
                ;

            //Read
            routes.MapGet("{id:Guid}",
                async (Guid id, int page, int limit) =>
                {
                    return await _service.Get(_context, page, limit);
                })
                ;
            
            //GetId Why dosent work?
           /* routes.MapGet("Id", 
                async (TransationRequest request) =>
                {
                    var transation = new TransationModel(request.Description, request.Status, request.Value, request.Date, request.PersonId);

                    return await _service.GetId(transation, _context);
                });
           */

            //Update
            routes.MapPatch("{id:guid}",
                async (Guid id, TransationRequest oldRequest) =>
                {
                    var oldTransation = new TransationModel(oldRequest.Description, oldRequest.Status, oldRequest.Value, oldRequest.Date, oldRequest.PersonId);
                  
                  
                    return await _service.Patch(oldTransation, _context, id);
                    

                })
                .RequireAuthorization();
                    
            //Delete
            routes.MapDelete("{id:guid}", 
                async (Guid id) =>
                {
                    return await _service.Delete(_context, id);
                })
                .RequireAuthorization();




            
        }
    }
}
