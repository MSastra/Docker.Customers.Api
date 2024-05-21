using Customers.Api.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
Console.WriteLine("connection string :" + builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddEntityFrameworkMySQL()
				.AddDbContext<DBContext>(options =>
				{
					options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection"));
				});
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply migrations at startup
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	try
	{
		var context = services.GetRequiredService<DBContext>();
		context.Database.Migrate();
	}
	catch (Exception ex)
	{
		// Handle exceptions if any
		Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
	}
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
