using API.Extension;
// using API.Data;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.EntityFrameworkCore;
// using API.Interface;
// using API.Service;
// using Microsoft.IdentityModel.Tokens;
// using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddApplicationServices(builder.Configuration);
//Equivalent to -> ApplicationServiceExtensions.AddApplicationServices(builder.Services,builder.Configuration);

builder.Services.AddIdentityService(builder.Configuration);

var app = builder.Build();

app.UseCors(x => x.
        AllowAnyHeader()
        .AllowAnyMethod()
        .WithOrigins("http://localhost:4200", "https://localhost:4200")
        );

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
