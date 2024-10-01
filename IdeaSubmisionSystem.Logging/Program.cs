var builder = WebApplication.CreateBuilder(args);

// Build configuration
var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

var app = builder.Build();

// Map a simple endpoint
app.MapGet("/", () => "Hello World!");

app.Run();
