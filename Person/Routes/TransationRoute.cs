using System.Runtime.CompilerServices;
using Person.Data;
using Person.Models;

namespace Person.Routes
{
    public static class TransationRoute
    {
        public static void TransationRoutes(this WebApplication app)
        {
            app.MapPost("Transation/Create", async (TransationRequest request, PersonContext context) =>
            {
                var transation = new TransationModel(request.value, request.date, request.personId);
                await context.AddAsync(transation);
                await context.SaveChangesAsync();
            });
        }
    }
}
