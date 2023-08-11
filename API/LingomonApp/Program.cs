using LingomonApp.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddCors();
builder.Services.AddDbContext<TheDbContext>(options =>
    options.UseNpgsql("Server=lingomonserver.postgres.database.azure.com;Port=5432;Database=postgres;User Id=postgres;Password=LingoMon2023Summer;"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseRouting();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map API controllers.
    //endpoints.MapRazorPages(); // This line is for Razor Pages (if used).
});
app.MapRazorPages();

app.UseHttpsRedirection();
app.UseStaticFiles();



app.Run();
