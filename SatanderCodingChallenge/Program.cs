using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddResponseCaching(); // Add Response Caching middleware
builder.Services.AddAuthorization();
builder.Services.AddControllers();
builder.Services.AddHttpClient("HackerNews", c =>
{
    c.BaseAddress = new Uri("https://hacker-news.firebaseio.com/v0/");
    c.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddMemoryCache();
builder.Services.AddScoped<IHackerNewsService, HackerNewsService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseResponseCaching(); // Enable Response Caching middleware

app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
