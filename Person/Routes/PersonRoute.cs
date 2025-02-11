

using Person.Models;

namespace Person.Route
{
    public static class PersonRoute
    {
        public static void PersonRoutes(this WebApplication app)
        {
            app.MapGet("person", () => new PersonModel("Luiz Eduardo Mazzini", "luizeduardomazzini@gmail.com", "Pokemom2024@") );
        }
    }
}
