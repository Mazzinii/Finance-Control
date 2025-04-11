using Person.Data;
using Person.Models.Requests;
using PersonTransation.Models;
using PersonTransation.Models.Requests;
using PersonTransation.Services;

namespace Person.Routes
{
    public static class PersonRoute
    {
        public static void PersonRoutes(this WebApplication app)
        {
            //mapping routes
            var routes = app.MapGroup("User").WithTags("Users");



            //Create
            routes.MapPost("",
                async (PersonRequest req, PersonTransationContext context, UserService service) =>
                {
                    var person = new UsersModel(req.Name, req.Email, req.Password);

                    return await service.Create(person, context);              
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
                async (int page, int limit, PersonTransationContext context, UserService service) =>
                {

                     return await service.Get(context,page,limit);

                });

            //Update
            routes.MapPatch("{id:Guid}",
                async (Guid id, PersonRequest req, PersonTransationContext context, UserService service) =>
                {
                    var person = new UsersModel(req.Name,req.Email,req.Password);

                    return await service.Patch(person, context,id);

                })
                .RequireAuthorization();

            //Delete
            routes.MapDelete("{id:Guid}",
                async (Guid id, PersonTransationContext context, UserService service) =>
                {
                    return await service.Delete(context, id);
                  
                })
                .RequireAuthorization();




        }
    }
}
