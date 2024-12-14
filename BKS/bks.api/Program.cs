using bks.api.Utilities;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors(configuration);
builder.Services.ConfigureDbContext(configuration);
builder.Services.ConfigureAppSettings(configuration);
builder.Services.ConfigureAuth(configuration);
builder.Services.ConfigureRedis(configuration);
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureServiceResolver();
//builder.Services.AddControllers().AddJsonOptions(option =>
//{
//	option.JsonSerializerOptions.PropertyNamingPolicy = null;
//	//option.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
//});
builder.Services.AddControllers()
	.AddJsonOptions(options =>
	{
		options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
		options.JsonSerializerOptions.MaxDepth = 64; // Adjust based on your needs
	});
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

app.Run();
