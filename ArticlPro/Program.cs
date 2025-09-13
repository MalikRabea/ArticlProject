using ArticlPro.Code;
using ArticlPro.Core;
using ArticlPro.Data;
using ArticlPro.Data.SqlServerEF;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ????? Connection String ?? Environment Variable
var appDbConnection = Environment.GetEnvironmentVariable("APP_DB_CONNECTION")
                      ?? builder.Configuration.GetConnectionString("DefaultConnection");

// ????? ApplicationDbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(appDbConnection));

// ????? DBContext ?? ArticlPro.Data
var dataDbConnection = Environment.GetEnvironmentVariable("DATA_DB_CONNECTION")
                       ?? builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DBContext>(options =>
    options.UseNpgsql(dataDbConnection));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Identity
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Injects Tables
builder.Services.AddScoped<IDataHelper<Category>, CategoryEntity>();
builder.Services.AddScoped<IDataHelper<Author>, AuthorEntity>();
builder.Services.AddScoped<IDataHelper<AuthorPost>, AuthorPostEntity>();


// Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireClaim("Admin", "Admin"));
    options.AddPolicy("User", policy => policy.RequireClaim("User", "User"));
});

// Email Sender
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// Auto Migration ??? DbContext
using (var scope = app.Services.CreateScope())
{
    var appDb = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    appDb.Database.Migrate();

    var dataDb = scope.ServiceProvider.GetRequiredService<DBContext>();
    dataDb.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages();
});

app.Run();
