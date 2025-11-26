var builder = WebApplication.CreateBuilder(args);

// Dependency Injection, Inversion of Control
// Concrete decision of instancing is given to the runtime, instead of us defining it definitively
// builder.Services.AddScoped()...

// Documentation
builder.Services.AddOpenApi();

var app = builder.Build();

app.MapGet("/time1", () => DateTime.UtcNow.ToString("n"));

app.UseHttpsRedirection();
app.Run();
