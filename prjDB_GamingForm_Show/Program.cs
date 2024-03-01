using Microsoft.EntityFrameworkCore;
using prjDB_GamingForm_Show.Models.Entities;
using System.Text.Json.Serialization;
using prjDB_GamingForm_Show.Hubs;
using DotNetEnv;
using DB_GamingForm_Show.Job.DeputeClass;
using prjDB_GamingForm_Show.Models.CallBack.Depute;

Env.Load();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.//
builder.Services.AddControllersWithViews();
builder.Services.AddSession();

builder.Services.AddDbContext<DbGamingFormTestContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SchoolConnection")));
builder.Services.AddTransient<CDeputeViewModel>();
builder.Services.AddTransient<List<CDeputeViewModel>>();
builder.Services.AddScoped<DeputeDataLoad>();
builder.Services.AddScoped<DeputeCookie>();
builder.Services.AddScoped<DeputeDataSearch>();
builder.Services.AddScoped<CDeputeDataLoad>();
builder.Services.AddScoped<CDeputeCookie>();
builder.Services.AddScoped<CDeputeSearch>();

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
pattern: "{controller=Depute}/{action=DeputeMain}/{id?}");
//pattern: "{controller=Admin}/{action=Index}/{id?}");
//pattern: "{controller=Home}/{action=Homepage}/{id?}");

app.Run();