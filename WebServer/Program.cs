using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using ASPNet_WPF_ChatApp.WebServer;


WebHost.CreateDefaultBuilder()
    .UseStartup<Startup>()
    .Build()
    .Run();


/*
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    //app.UseExceptionHandler("/Home/Error");

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseExceptionHandler("/Home/Error");

    //app.UseHsts();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{moreInfo?}"
);

app.MapControllerRoute(
        name: "aboutPage",
        pattern: "more",
        defaults: new { controller = "About", action = "TellMeMore" }
);

app.UseAuthorization();


app.Run();
*/