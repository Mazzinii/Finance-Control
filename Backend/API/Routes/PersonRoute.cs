using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Person.Data;
using Person.Models.Requests;
using PersonTransation.Models;
using PersonTransation.Models.Requests;
using PersonTransation.Services;

namespace Person.Routes
{
    public static class PersonRoute
    {

        private static readonly UserService _service = new UserService();
        private static readonly PersonTransationContext _context = new PersonTransationContext();

        public static void PersonRoutes(this WebApplication app)
        {
            //mapping routes
            var routes = app.MapGroup("User").WithTags("Users");



            //Create
            routes.MapPost("",
                async (PersonRequest req) =>
                {
                    var person = new UsersModel(req.Name, req.Email, req.Password);

                    return await _service.Create(person, _context);              
                });

            //Login
            routes.MapPost("Login",
                async (LoginHashRequests.LoginRequest req, LoginHashRequests login) =>
                {
                    var person = await login.Handle(req);
                    return Results.Ok(person);

                });

            //Read
            routes.MapGet("",
                async (int page, int limit) =>
                {

                     return await _service.Get(_context, page, limit);

                });

            //Update
            routes.MapPatch("{id:Guid}",
                async (Guid id, PersonRequest req) =>
                {
                    var person = new UsersModel(req.Name,req.Email,req.Password);

                    return await _service.Patch(person, _context,id);

                })
                .RequireAuthorization();

            //Delete
            routes.MapDelete("{id:Guid}",
                async (Guid id, UserService service) =>
                {
                    return await _service.Delete(_context, id);
                  
                })
                .RequireAuthorization();




        }
    }
}
