using System.Text.Json.Serialization;
using OrderManagement.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Adding Serialization Options
/**
builder.Services.AddControllers()
    .AddXmlDataContractSerializerFormatters();
**/
builder.Services.AddControllers(options => options.ReturnHttpNotAcceptable = true).AddJsonOptions(options => {
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
    options.JsonSerializerOptions.AllowTrailingCommas = true;
    options.JsonSerializerOptions
        .Converters
            .Add(new JsonStringEnumConverter()); // Enums no longer return numbers but the Enum Values Label instead
});


builder.Services.AddScoped<IOrderManagementLogic, OrderManagementLogic>(); // Instance Per Request, great first step, refine later to singleton or transient if beneficial
//builder.Services.AddTransient<IOrderManagementLogic, OrderManagementLogic>(); // Instance per Injection
//builder.Services.AddSingleton<IOrderManagementLogic, OrderManagementLogic>(); // Instance per Runtime; Db Service with permanent Connection, HttpClient with Port allocation 

builder.Services.AddCors(corsBuilder => corsBuilder.AddDefaultPolicy(policy => policy
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
)); // Cors Configuration; For Prod -> WithOrigin

builder.Services.AddOpenApiDocument(settings => 
    settings.Title = "Order Management Api");

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseCors(); // Cors Middleware added
app.UseOpenApi();
app.UseSwaggerUi(settings => settings.Path = "/swagger");

app.Run();