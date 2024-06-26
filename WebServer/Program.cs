using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

using Dna.AspNet;

using ASPNet_WPF_ChatApp.WebServer;
using Dna;

WebHost.CreateDefaultBuilder()
    // Add Dna Framework
    .UseDnaFramework(construct =>
    {
        // Configure framework

        // Add file logger
        construct.AddFileLogger();
    })
    .ConfigureLogging(logging =>
    {
        // Use LogLevel.Debug or LogLevel.Trace for more detailed output
        logging.ClearProviders();
        logging.AddConsole();
        logging.AddDebug();
        logging.SetMinimumLevel(LogLevel.Information); // Trace is the lowest log level, and Nothing is the highest (least verbose) mode.
    })
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