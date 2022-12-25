using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BookExperience.Infrastrucutre.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using BookExperience.Core.Extensions;
using BookExperience.Core.ModelBinders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>
    (options => options.SignIn.RequireConfirmedAccount = false)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddRazorPages();

builder.Services.AddAuthentication()
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration.GetValue<string>("Facebook:AppId");
        options.AppSecret = builder.Configuration.GetValue<string>("Facebook:AppSecret");
    });

builder.Services.AddControllersWithViews()
    .AddMvcOptions(options =>
    {
        options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider());
        options.ModelBinderProviders.Insert(1, new BookExperience.Core.ModelBinders.DateTimeModelBinderProvider("dd.mm.yyyy"));
        options.ModelBinderProviders.Insert(2, new DoubleModelBinderProvider());
    });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

app.PrepareDatabase();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "Areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

    endpoints.MapDefaultControllerRoute();
    endpoints.MapRazorPages();
    app.MapRazorPages();
});

app.MapRazorPages();

app.Run();
