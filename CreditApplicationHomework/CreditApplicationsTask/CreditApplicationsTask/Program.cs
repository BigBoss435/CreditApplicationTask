using CreditApplicationsTask.Services;

// Web application builder is created
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Razor pages provide more efficient way to build dynamic web pages
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddScoped<ICreditDecisionService, CreditDecisionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // HTTP strict transport security
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
