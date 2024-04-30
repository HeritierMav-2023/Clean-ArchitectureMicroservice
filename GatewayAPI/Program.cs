using Ocelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

builder.Services.AddOcelot()
    .AddDelegatingHandler<RequestLogger>()
    .AddCacheManager(x => {
        x.WithDictionaryHandle();
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

//Ocelot
await app.UseOcelot();

app.Run();

#region Class Logger
public class RequestLogger : DelegatingHandler
{
    private readonly ILogger<RequestLogger> _logger;

    public RequestLogger(ILogger<RequestLogger> logger)
    {
        _logger = logger;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Request [{request.Method}] - {request.RequestUri?.PathAndQuery} : {request.RequestUri?.Port}");
        return base.SendAsync(request, cancellationToken);
    }
}
#endregion
