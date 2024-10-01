using IdeaSystem.Logger.SerilogConfig;
using IdeaSystem.Logging.Serilig.Logger;
using IdeaSystem.Infrastructure;
using IdeaSystem.Application;
var builder = WebApplication.CreateBuilder(args);

builder.UseSerilog(cfg =>
{
    cfg.LogFilePath = builder.Configuration["Serilog:LogPath"]!;
    cfg.RollingInterval = RollingDuration.Day;
    cfg.ApplicationName = builder.Configuration["Serilog:Properties:ApplicationName"];
    cfg.FileSizeLimitBytes = builder.Configuration.GetSection("Serilog:FileSizeLimitBytes")?.Get<long?>();
});
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddInfrastructureServices(builder.Configuration, builder.Environment);
builder.Services.AddApplicationServices();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAntiforgery(options => options.HeaderName = "X-CSRF-TOKEN");

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseCorrelationIdMiddleware();
app.UseLoggingMiddleware();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { }