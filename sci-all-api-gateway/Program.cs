using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using sci_all_api_gateway.aggregator;
using sci_all_api_gateway.handlers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var env = builder.Environment;
builder.Configuration.AddOcelot("./Routes/", env);
builder.Services.AddOcelot(builder.Configuration)
    .AddDelegatingHandler<RemoveEncodingDelegatingHandler>(true)
    .AddSingletonDefinedAggregator<StreamPublicationAggregator>();

var app = builder.Build();

app.UseOcelot().Wait();
app.UseHttpsRedirection();
app.Run();
