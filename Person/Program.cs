using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Person.Data;
using Person.Extensions;
using PersonTransation.Models.Requests;
using Person.Routes;
using Person.Services;
using PersonTransation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithAuth();
builder.Services.AddScoped<PersonTransationContext>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasherService>();
builder.Services.AddScoped<LoginHashRequests>();
builder.Services.AddScoped<UserService>();
builder.Services.AddSingleton<TokenService>();
builder.Services.AddAuthorization();

//add auth JWT token
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(x =>
    {
        x.RequireHttpsMetadata = false;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Key:Jwt"]!)),
            ValidateIssuer = false,
            ValidateAudience = false,
        };

    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.PersonRoutes();
app.TransationRoutes();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

