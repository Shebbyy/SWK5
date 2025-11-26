using System.Runtime.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Dependency Injection, Inversion of Control
// Concrete decision of instancing is given to the runtime, instead of us defining it definitively
// builder.Services.AddScoped()...

// Documentation
builder.Services.AddOpenApi();

// Controller-Based Approach gives Structure to the project
builder.Services.AddControllers();

var app = builder.Build();

// Always think about resources, name the controller accordingly, resource = time, TimeController
// Minimal API preferred nowadays in enterprise
// Fluent API, Structure of Project is developers problem
app.MapGet("/time1", () => DateTime.UtcNow.ToString("d"));

app.UseHttpsRedirection();
app.Run();
