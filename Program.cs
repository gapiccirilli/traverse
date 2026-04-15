using Microsoft.EntityFrameworkCore;
using Traverse.DbContexts;
using Traverse.Models;
using Traverse.Options;
using Traverse.Providers;
using Traverse.Providers.Impl;
using Traverse.Repository;
using Traverse.Repository.Impl;
using Traverse.Services;
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

builder.Services.AddHttpClient<IMapProvider<Event>, AppleMapsProvider>();

builder.Services.AddMemoryCache();

builder.Services.AddOptions<MapsOptions>()
    .Bind(builder.Configuration.GetSection("MapsApi:Apple"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<IMapTokenService<string>, AppleMapsTokenService>();
builder.Services.AddScoped<IMapProvider<Event>, AppleMapsProvider>();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler();
}

// converts non-success status codes to problem details responses
app.UseStatusCodePages();

// app.MapGet("/", () => app.Configuration.AsEnumerable());

app.MapControllers();
app.Run();