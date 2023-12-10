
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

public class RateLimitMiddleware
{
    private readonly RequestDelegate _next;
    private readonly int _limit;
    private readonly TimeSpan _timeSpan;
    private readonly IMemoryCache _cache;

    public RateLimitMiddleware(RequestDelegate next, int limit, TimeSpan timeSpan, IMemoryCache cache)
    {
        _next = next;
        _limit = limit;
        _timeSpan = timeSpan;
        _cache = cache;
    }

    public async Task Invoke(HttpContext context)
    {
        var ipAddress = context.Connection.RemoteIpAddress;

        var cacheKey = ipAddress.ToString();
        var entryOptions = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _timeSpan
        };

        var requestCount = _cache.GetOrCreate(cacheKey, entry =>
        {
            entry.SetOptions(entryOptions);
            return 0;
        });

        if (requestCount >= _limit)
        {
            context.Response.StatusCode = 429; // Too Many Requests
            await context.Response.WriteAsync("Rate limit exceeded.");
            return;
        }

        // Increment the request count
        _cache.Set(cacheKey, requestCount + 1, entryOptions);

        await _next(context);
    }
}

public static class RateLimitMiddlewareExtensions
{
    public static IApplicationBuilder UseRateLimitMiddleware(this IApplicationBuilder builder, int limit, TimeSpan timeSpan)
    {
        return builder.UseMiddleware<RateLimitMiddleware>(limit, timeSpan);
    }
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Other middleware configurations

    // Add RateLimitMiddleware
    app.UseRateLimitMiddleware(limit: 100, timeSpan: TimeSpan.FromMinutes(1));

    // More middleware configurations

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
}