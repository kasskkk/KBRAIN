using KBRAIN.Application.Tiles;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace KBRAIN.Application.Chat
{
    public class ChatService(IChatClient chatClient, IOptions<AssistantOptions> options, IKnowledgeTileRepository knowledgeTileRepository)
    {
        public async Task<string> AskAsync(string question, CancellationToken cancellationToken)
        {
            var (chatMessages, chatOptions) = await BuildRequestAsync(question, cancellationToken);

            var response = await chatClient.GetResponseAsync(chatMessages, chatOptions, cancellationToken: cancellationToken);

            return response.Text;
        }

        public async IAsyncEnumerable<string> StreamAsync(string question, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var (chatMessages, chatOptions) = await BuildRequestAsync(question, cancellationToken);

            await foreach (var update in chatClient.GetStreamingResponseAsync(chatMessages, chatOptions, cancellationToken))
            {
                if (string.IsNullOrEmpty(update.Text))
                {
                    continue;
                }

                yield return update.Text;
            }
        }

        private async Task<(List<ChatMessage> Messages, ChatOptions Options)> BuildRequestAsync(string question, CancellationToken cancellationToken)
        {
            var pinnedTiles = await knowledgeTileRepository.GetPinnedAsync(cancellationToken);

            var assistant = options.Value;

            var prompt = string.Join("\n", assistant.SystemPrompt).Replace("{date}", DateTime.Now.ToString("yyyy-MM-dd"));

            var chatOptions = new ChatOptions()
            {
                Temperature = assistant.Temperature,
            };

            if (pinnedTiles.Count > 0)
            {
                var knowledge = string.Join(
                    "\n\n",
                    pinnedTiles.Select(tile => $"## {tile.Title}\n{tile.Content}"));

                prompt += $"\n\nWiedza o firmie (traktuj jako prawdziwą):\n{knowledge}";
            }

            List<ChatMessage> chatMessages = [
                new ChatMessage(ChatRole.System, prompt),
                new ChatMessage(ChatRole.User, question)
                ];

            return (chatMessages, chatOptions);
        }

    }
}
