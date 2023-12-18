using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using System.Text.Json.Serialization;
using prjDB_GamingForm_Show.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.//
builder.Services.AddControllersWithViews();
builder.Services.AddSession();
builder.Services.AddDbContext<DbGamingFormTestContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnection")));
builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
builder.Services.AddSignalR();
var app = builder.Build();
// //Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.MapHub<ChatHub>("/chatHub");
app.MapHub<MemberChatHub>("/memberChatHub");
app.MapControllerRoute(
    name: "default",
//pattern: "{controller=depute}/{action=homeframe}/{id?}");
//pattern: "{controller=Blog}/{action=List}/{id?}");
//pattern: "{controller=Shop}/{action=Index}/{id?}");
//pattern: "{controller=Depute}/{action=DeputeList}/{id?}");
pattern: "{controller=Admin}/{action=Index}/{id?}");

app.Run();