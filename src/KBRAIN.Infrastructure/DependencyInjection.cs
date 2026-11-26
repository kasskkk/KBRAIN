using KBRAIN.Application.Tiles;
using KBRAIN.Infrastructure.Ai;
using KBRAIN.Infrastructure.Persistence;
using KBRAIN.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
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
            var connectionString = configuration.GetConnectionString("Default");

            if (string.IsNullOrWhiteSpace(connectionString) || connectionString.Contains("CHANGE_ME", StringComparison.Ordinal))
            {
                throw new InvalidOperationException(
                    "Missing or unconfigured connection string 'Default' (replace 'CHANGE_ME' with your local password).");
            }

            services.AddDbContext<AppDbContext>(options =>
            {
                options
                    .UseNpgsql(connectionString)
                    .UseSnakeCaseNamingConvention();
            });

            services.AddScoped<IKnowledgeTileRepository, KnowledgeTileRepository>();

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
