using BTL_NguyenVanTruong_.Models;
using BTL_NguyenVanTruong_.DAL.Interfaces;
using BTL_NguyenVanTruong_.BLL.Interfaces;
using BTL_NguyenVanTruong_.BLL;
using BTL_NguyenVanTruong_.DAL;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BTL_NguyenVanTruong_;




var builder = WebApplication.CreateBuilder(args);

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
//builder.Services.AddTransient<IUserRepository, UserRepository>();
//builder.Services.AddTransient<IUserBussiness, UserBussiness>();
app.UseHttpsRedirection();
app.UseStaticFiles();

////IConfiguration configuration = builder.Configuration;
////var appSettingsSection = configuration.GetSection("AppSettings");
////builder.Services.Configure<AppSettings>(appSettingsSection);

////// configure jwt authentication
////var appSettings = appSettingsSection.Get<AppSettings>();
////var key = Encoding.ASCII.GetBytes(appSettings.Secret);
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
