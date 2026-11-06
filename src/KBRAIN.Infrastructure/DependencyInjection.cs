using KBRAIN.Infrastructure.Ai;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using OllamaSharp;
using OllamaSharp.Models.Chat;

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

                var client = new HttpClient()
                {
                    BaseAddress = new Uri(options.EndPoint),
                    Timeout = TimeSpan.FromMinutes(options.TimeoutMinutes)
                };


                var ollamaClient = new OllamaApiClient(client, options.ModelName);

                return new ChatClientBuilder(ollamaClient)
                    .ConfigureOptions(chatOptions =>
                    {
                        chatOptions.AdditionalProperties ??= new();

                        chatOptions.AdditionalProperties["think"] = options.Think;

                        chatOptions.AdditionalProperties["num_ctx"] = options.ContextSize;
                    })
                    .Build();
            });

            return services;
        }
    }
}
