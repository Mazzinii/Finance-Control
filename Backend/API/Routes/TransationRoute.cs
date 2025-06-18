using Person.Data;
using Person.Models.Requests;
using PersonTransation.Services;
using PersonTransation.Models.Entities;

namespace Person.Routes
{
    public static class TransationRoute
    {
        private static readonly TransationService _service = new TransationService();
        private static readonly PersonTransationContext _context = new PersonTransationContext();

        public static void TransationRoutes(this WebApplication app)
        {
            var routes = app.MapGroup("Transation").WithTags("Transations");

            //Create
            routes.MapPost("Create",
                async (TransationRequest request) =>
                {
                    var transation = new TransationModel(request.Description, request.Status, request.Value, request.Date, request.PersonId);
                    return await _service.Create(transation, _context);

                })
                ;

            //Read
            routes.MapGet("{personId:Guid}",
                async (Guid personId, int page, int limit) =>
                {
                    return await _service.Get(_context, personId, page, limit);
                })
                ;
            
            //GetId 
            routes.MapGet("{value:int}/{date:Datetime}/{personId:Guid}", 
                async (string description, string status, int value, DateTime date, Guid personId) =>
                {
                    var transation = new TransationModel(description, status, value, date, personId);

                    return await _service.GetId(transation, _context);
                });
           

            //Update
            routes.MapPatch("{transationId:guid}",
                async (Guid transationId, TransationUpdateRequest oldRequest) =>
                {
                    var patchedTransation = new TransationModel(oldRequest.Description, oldRequest.Status, oldRequest.Value, oldRequest.Date);
                  
                  
                    return await _service.Patch(patchedTransation, _context, transationId);
                    

                })
                .RequireAuthorization();
                    
            //Delete
            routes.MapDelete("{transationId:guid}", 
                async (Guid transationId) =>
                {
                    return await _service.Delete(_context, transationId);
                })
                .RequireAuthorization();




            
        }
    }
}
