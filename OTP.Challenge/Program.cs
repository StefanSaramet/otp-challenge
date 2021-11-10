using Otp.Challenge.PasswordGeneration;
using Otp.Challenge.Persistence;
using OTP.Challenge.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddSignalR();

builder.Services.AddSingleton<IOtpRepository, OtpRepository>();
builder.Services.AddSingleton<IOtpGenerator, OtpGenerator>();

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
