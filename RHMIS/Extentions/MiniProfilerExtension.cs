using System;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Profiling;
using StackExchange.Profiling.Storage;

namespace PHMIS.Extensions;

public static class MiniProfilerExtension
{
    public static IServiceCollection AddMiniProfilerServices(this IServiceCollection services)
    {
        services.AddMiniProfiler(options =>
        {
            options.RouteBasePath = "/profiler"; // default is /mini-profiler-resources
            if (options.Storage is MemoryCacheStorage memoryStorage)
            {
                memoryStorage.CacheDuration = TimeSpan.FromMinutes(60);
            }
            options.SqlFormatter = new StackExchange.Profiling.SqlFormatters.InlineFormatter();
            options.TrackConnectionOpenClose = true;
            options.ColorScheme = ColorScheme.Auto;
            options.PopupDecimalPlaces = 1;
            options.EnableMvcFilterProfiling = true;
            options.EnableMvcViewProfiling = true;
        }).AddEntityFramework();

        return services;
    }
}
