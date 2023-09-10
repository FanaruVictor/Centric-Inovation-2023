using InPositionChatBot.BusinessLogic;
using InPositionChatBot.Data;
using InPositionChatBot.Data.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDb(builder.Configuration);
builder.Services.AddBusinessLogicServices();

var allowedCorsHosts = builder.Configuration["AllowedHosts"]?.Split(",");
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(
		policyBuilder =>
		{
			policyBuilder.WithOrigins(allowedCorsHosts ?? new[] { "*" });
			policyBuilder.AllowAnyHeader();
			policyBuilder.AllowAnyMethod();
			policyBuilder.AllowCredentials();
		});
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	using (var scope = app.Services.CreateScope())
	{
		var db = scope.ServiceProvider.GetRequiredService<InPositionChatBotDbContext>();
		db.Database.EnsureCreated();
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
