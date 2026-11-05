using KBRAIN.Infrastructure.Ai;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OllamaSharp;

namespace KBRAIN.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
        {
            services.AddOptions<OllamaOptions>()
                .Bind(configuration.GetSection(OllamaOptions.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<IChatClient>(serviceProvider =>
            {
                var options = serviceProvider.GetRequiredService<IOptions<OllamaOptions>>().Value;
                return new OllamaApiClient(new Uri(options.EndPoint), options.ModelName);
            });

            return services;
        }
    }
}
