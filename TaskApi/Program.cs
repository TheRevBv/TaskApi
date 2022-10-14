using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TaskApi.Data;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.LogTo(Console.WriteLine, new[]
    {
        DbLoggerCategory.Database.Command.Name
    }, LogLevel.Information).EnableSensitiveDataLogging();
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLServerConnection")!);
});
//Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
                      builder =>
                      {
                          builder.WithOrigins("*",
                                              "http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                      });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseCors(myAllowSpecificOrigins);

app.UseAuthorization();

app.MapControllers();

app.Run();
