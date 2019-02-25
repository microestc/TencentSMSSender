using System;
using Microsoft.Extensions.DependencyInjection;
using Polly;

namespace TencentSMSSender
{
    public static class TencentSMSSenderExtension
    {
        public static IServiceCollection AddTencentSMSSenderServices(this IServiceCollection services, Action<TencentSMSSenderOptions> setupAction)
        {

            if (setupAction != null)
            {
                services.Configure(setupAction);
            }

            services.AddTransient<HttpClientDelegatingHandler>();

            services.AddHttpClient<ITencentSMSSender, TencentSMSSender>(option =>
            {
                option.DefaultRequestHeaders.Add("Conetent-Type", "application/json");
            })
            .AddHttpMessageHandler<HttpClientDelegatingHandler>()
            .AddTransientHttpErrorPolicy(p => p.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(600)))
            .AddTransientHttpErrorPolicy(p => p.CircuitBreakerAsync(5, TimeSpan.FromSeconds(10)));

            return services;
        }

        
    }
}
