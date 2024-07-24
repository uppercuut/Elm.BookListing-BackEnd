using AspNetCoreRateLimit;
using Elm.BookListing.Application.Queries;
using Elm.BookListing.Domain.Abstractions;
using Elm.BookListing.Domain.Repositories;
using Elm.BookListing.Infrastructure.Repositories.Books;
using Elm.BookListing.Application.Queries;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Cors.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();



var options = new QueryExecuterOptions
{
    ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
};
builder.Services.AddSingleton(options);
builder.Services.AddScoped<IQueryExecuter, QueryExecuter>();
builder.Services.AddScoped<IBookReadRepository, BookReadRepository>();
builder.Services.AddScoped<IBookQueries, BookQueries>();

builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

builder.Services.AddInMemoryRateLimiting();

builder.Services.AddSingleton<IClientPolicyStore, DistributedCacheClientPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();


Action<CorsOptions> corsOptions = null;
CorsPolicyBuilder corsPolicyBuilder = new CorsPolicyBuilder()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed((host) => true)
                        .AllowAnyMethod();
if (builder.Environment.IsDevelopment())
{
    corsPolicyBuilder.AllowAnyOrigin();
    corsOptions = options => options.AddDefaultPolicy(corsPolicyBuilder.Build());
}
else
{
    var origins = builder.Configuration.GetSection("AllowedOrigins").Value.Split(',');
    corsPolicyBuilder.WithOrigins(origins).AllowCredentials();
    corsOptions = options => options.AddPolicy("AllowCors", corsPolicyBuilder.Build());
}
builder.Services.AddCors(corsOptions);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors();
}
else
{
    app.UseCors("AllowCors");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();



