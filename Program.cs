using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Traverse.DbContexts;
using Traverse.Models;
using Traverse.Models.Dto;
using Traverse.Models.Graph;
using Traverse.Models.Records.Maps;
using Traverse.Options;
using Traverse.Providers;
using Traverse.Providers.Impl;
using Traverse.Repository;
using Traverse.Repository.Graph;
using Traverse.Repository.Graph.Impl;
using Traverse.Repository.Impl;
using Traverse.Services;
using Traverse.Services.Graph;
using Traverse.Services.Graph.Impl;
using Traverse.Services.Impl;
using Traverse.Services.Maps;
using Traverse.Services.Maps.Impl;
using Traverse.Services.Timezone;
using Traverse.Services.Timezone.Impl;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    builder.Configuration.AddUserSecrets<Program>(optional: true, reloadOnChange: true);   
}

builder.Configuration.AddEnvironmentVariables("TRAVERSE");

var travConnString = builder.Configuration.GetConnectionString("TraverseConnectionString");

builder.Services.AddDbContext<CoreDbContext>(options => {
    options.UseNpgsql(travConnString, options => options.UseNetTopologySuite());
    options.UseSnakeCaseNamingConvention();
});

builder.Services.AddHangfireServer();

builder.Services.AddHangfire(config => 
    config.UsePostgreSqlStorage(options => options.UseNpgsqlConnection(travConnString))
);

// converts exceptions to problem details responses when useExceptionHandler is used without path
builder.Services.AddProblemDetails();
builder.Services.AddControllers();

builder.Services.AddSingleton<ITimeZoneService, GeoTimeZoneService>();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IItineraryService, ItineraryService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<ITripService, TripService>();

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IItineraryRepository, ItineraryRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IRatingRepository, RatingRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();

builder.Services.AddHttpClient<IMapProvider<EventDto, Transportation>, AppleMapsProvider>();
builder.Services.AddScoped<IMapProvider<EventDto, Transportation>, AppleMapsProvider>();


builder.Services.AddMemoryCache();

builder.Services.AddOptions<MapsOptions>()
    .Bind(builder.Configuration.GetSection("MapsApi:Apple"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<IMapTokenService<string>, AppleMapsTokenService>();
builder.Services.AddScoped<IEdgeService<EventDto, Transportation>, TransportationEdgeService>();
builder.Services.AddScoped<IEdgeRepository<Transportation>, TransportationEdgeRepository>();
builder.Services.AddScoped<IGraphService<ItineraryGraph, EventDto, Transportation>, ItineraryGraphService>();
builder.Services.AddScoped<IOptimizationService<long, ItineraryGraph>, RouteOptimizationService>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

// converts non-success status codes to problem details responses
app.UseStatusCodePages();

app.UseHangfireDashboard("/hangfire");

// app.MapGet("/", () => app.Configuration.AsEnumerable());

app.MapControllers();
app.Run();