using glmanager.Data;
using glmanager.Models;
using glmanager.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddOpenApi();

//banco
builder.Services.AddDbContext<AppDbContext>(options =>
{
   options.UseNpgsql("Host=localhost;Port=5432;Database=glmanager;Username=postgres;Password=senha"); 
});

//identity
builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<Usuario>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireNonAlphanumeric = true;
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
})
.AddRoles<IdentityRole>() //liga suporte a roles (papéis)
.AddEntityFrameworkStores<AppDbContext>(); //Guarda os dados via entity framework através do dbcontext

builder.Services.AddScoped<IUsuariosService, UsuariosService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();
//Gera endpoints
app.MapIdentityApi<Usuario>();

app.MapControllers();

app.Run();
