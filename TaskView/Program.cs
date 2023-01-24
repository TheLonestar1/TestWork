

using Microsoft.EntityFrameworkCore;
using TaskView.Model;

var builder = WebApplication.CreateBuilder(args);
string filename = builder.Configuration.GetValue<string>("FileName");
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(filename));
// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseAuthorization();

app.MapRazorPages();

app.Run();
