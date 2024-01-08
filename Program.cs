using Carter;

// Builder Configure
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCarter();

// Use builder services
var app = builder.Build();
app.MapCarter();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Minimal API v1");
    c.RoutePrefix = "";
});

app.Run();
