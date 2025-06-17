var builder = WebApplication.CreateBuilder(args);
System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
Console.OutputEncoding = System.Text.Encoding.UTF8;

// Add services to the container.
builder.Services.AddControllersWithViews();

// Ajouter la configuration de la session
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Durée d'expiration de la session
    options.Cookie.HttpOnly = true; // Sécuriser les cookies
    options.Cookie.IsEssential = true; // Requis pour le RGPD
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Ajouter cette ligne pour activer l'utilisation de la session
app.UseSession(); // Active l'utilisation de la session

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.Run();
