using Microsoft.EntityFrameworkCore;
using UserManagement.Repositories.DbContext;
using UserManagement.Repositories.UsersRepository;
using UserManagement.Services.UsersService;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

string? connectionStringValue = builder.Configuration
                                       .GetSection("ConnectionStrings")
                                       .GetSection("DB_CONNECTION_STRING")
                                       .Value;

string connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") ?? connectionStringValue ?? string.Empty;
builder.Services.AddDbContext<PostgreSqlContext>(options => { options.UseNpgsql(connectionString); });

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
