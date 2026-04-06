using Fallah_App.Context;
using Fallah_App.QuartzJobs;
using Microsoft.EntityFrameworkCore;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddSession(opt =>
{
    opt.IdleTimeout = TimeSpan.FromDays(10);
}
);

builder.Services.AddQuartz(q =>
{
    q.UseMicrosoftDependencyInjectionScopedJobFactory();
    // Just use the name of your job that you created in the Jobs folder.
    var jobKey = new JobKey("EnvoyerConseil");
    q.AddJob<EnvoyerConseil>(opts => opts.WithIdentity(jobKey));

    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithIdentity("EnvoyerConseil-trigger")
          //This Cron interval can be described as "run every minute" (when second is zero)
          .WithSimpleSchedule(x => x.WithIntervalInMinutes(10).RepeatForever())
    );
}); 


builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true); 


if (Environment.GetEnvironmentVariable("DB_NAME") != null)
{
    builder.Services.AddDbContext<MyContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("MyContext"));
    });
    
}
else
{
    builder.Services.AddDbContext<MyContext>(opt =>
    {
        opt.UseSqlServer(builder.Configuration.GetConnectionString("localConnection"));
    });

}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
   
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
