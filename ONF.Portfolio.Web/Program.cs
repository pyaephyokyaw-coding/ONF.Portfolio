using ONF.Portfolio.Application.Interfaces;
using ONF.Portfolio.Application.Services;
using ONF.Portfolio.Infrastructure.Data;
using ONF.Portfolio.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ProjectRepository>();
builder.Services.AddSingleton<DapperContext>();
//builder.Services.AddScoped<DapperContext>(provider =>
//{
//    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
//    return new DapperContext(connectionString);
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
