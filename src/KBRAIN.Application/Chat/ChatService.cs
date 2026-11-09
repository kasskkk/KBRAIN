using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace KBRAIN.Application.Chat
{
    public class ChatService(IChatClient chatClient, IOptions<AssistantOptions> options)
    {
        public async Task<string> AskAsync(string question, CancellationToken cancellationToken)
        {
            var (chatMessages, chatOptions) = BuildRequest(question);

            var response = await chatClient.GetResponseAsync(chatMessages, chatOptions, cancellationToken: cancellationToken);

            return response.Text;
        }

        public async IAsyncEnumerable<string> StreamAsync(string question, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            var (chatMessages, chatOptions) = BuildRequest(question);

            await foreach (var update in chatClient.GetStreamingResponseAsync(chatMessages, chatOptions, cancellationToken))
            {
                if (string.IsNullOrEmpty(update.Text))
                {
                    continue;
                }

                yield return update.Text;
            }
        }

        private (List<ChatMessage> Messages, ChatOptions Options) BuildRequest(string question)
        {
            var assistant = options.Value;

            var prompt = string.Join("\n", assistant.SystemPrompt).Replace("{date}", DateTime.Now.ToString("yyyy-MM-dd"));

            var chatOptions = new ChatOptions()
            {
                Temperature = assistant.Temperature,
            };

            List<ChatMessage> chatMessages = [
                new ChatMessage(ChatRole.System, prompt),
                new ChatMessage(ChatRole.User, question)
                ];

            return (chatMessages, chatOptions);
        }

    }
}
