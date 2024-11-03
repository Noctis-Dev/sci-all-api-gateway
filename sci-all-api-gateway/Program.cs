using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddJsonFile("./Routes/ocelot.auth.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("./Routes/ocelot.payments.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

var app = builder.Build();

app.UseOcelot().Wait();
app.UseHttpsRedirection();
app.Run();
