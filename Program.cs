using VehicleRegisterApplication.Scripts.ObjectHandlers;
using VehicleRegisterApplication.Scripts;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<VehicleDataHandler>();
builder.Services.AddSingleton<VehicleIdHandler>();

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

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//this one has to be changed. figure out if you can have 2 home controllers. home controller should have priority. 
app.MapControllerRoute(
    name: "home",
    pattern: "{controller=Home}/{action=Index}");

app.MapControllerRoute(
    name: "vehicle",
    pattern: "{controller=Vehicle}/{action=vehicles}");

app.Run();
