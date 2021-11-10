using OTP.Challenge.Hubs;
using OTP.Challenge.Jobs;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSignalR() ;

builder.Services.AddQuartz(config =>
{
    config.UseMicrosoftDependencyInjectionJobFactory();

    var changeUserOtpJob = new JobKey("ChangeUserOtp");
    config.AddJob<RefreshOtpJob>(opts => opts.WithIdentity(changeUserOtpJob));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ResetOtpHub>("/resetotp");
});

app.Run();
