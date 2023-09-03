using Auth.Extensions;
using Event_Management.Data;
using Event_Management.Extensions;
using Event_Management.Services.Iservices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adding Db Connection
builder.Services.AddDbContext<ApplicationDbContext>(options => {options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));});

// Registering a Service --Dependancy Injection
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEventService, EventService>();

// AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//authentication
builder.AddAppAuthentication();

//Adding Authorization options

builder.addAuthorizationExtension();

builder.AddSwaggenGenExtension();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.ApplyMigration();

app.Run();
