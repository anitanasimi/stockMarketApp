using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.AspNetCore;
using StockMarketWithSignalR.Database;
using StockMarketWithSignalR.Hubs;
using StockMarketWithSignalR.Jobs;
using StockMarketWithSignalR.Repositories.Currency;
using StockMarketWithSignalR.Repositories.Market;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy",
        b => b
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddControllers();

builder.Services.AddDbContext<StockMarketDb>(
    opt =>
    {
        opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);

builder.Services.AddScoped<ICurrencyRepository, CurrencyRepository>();
builder.Services.AddScoped<IMarketRepository, MarketRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddQuartz(q =>
//{
//    // Just use the name of your job that you created in the Jobs folder.
//    var jobKey = new JobKey("ChangeMarketJob");
//    q.AddJob<ChangeMarketJob>(opts => opts.WithIdentity(jobKey));

//    q.AddTrigger(opts => opts
//        .ForJob(jobKey)
//        .WithIdentity("ChangeMarketJob-trigger")
//        .WithCronSchedule("0/30 * * ? * *")
//    );
//});
//builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

//// ASP.NET Core hosting
//builder.Services.AddQuartzServer(options =>
//{
//    // when shutting down we want jobs to complete gracefully
//    options.WaitForJobsToComplete = true;
//});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(x => x
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials()); // allow credentials
}

app.UseCors("CORSPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapHub<MarketHub>("/market-hub");

app.Run();
