using Microsoft.EntityFrameworkCore;
using UserManagement.Repositories.DbContext;
using UserManagement.Repositories.UsersRepository;
using UserManagement.Services.UsersService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? string.Empty;
builder.Services.AddDbContext<PostgreSqlContext>(options => { options.UseNpgsql(connectionString); });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
