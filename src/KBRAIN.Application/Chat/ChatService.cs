using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;

namespace KBRAIN.Application.Chat
{
    public class ChatService(IChatClient chatClient, IOptions<AssistantOptions> options)
    {
        public async Task<string> AskAsync(string question, CancellationToken cancellationToken)
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

            var response = await chatClient.GetResponseAsync(chatMessages, chatOptions, cancellationToken: cancellationToken);

            return response.Text;
        }
    }
}
