using Microsoft.EntityFrameworkCore;
using PoliziaMunicipaleApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Aggiungi i servizi al contenitore
builder.Services.AddControllersWithViews();

// Configura Entity Framework Core con SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configura altre impostazioni, se necessarie (ad esempio, per l'autenticazione, se usata)

var app = builder.Build();

// Configura la pipeline delle richieste HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
