using Microsoft.Extensions.AI;

namespace KBRAIN.Application.Chat
{
    public class ChatService(IChatClient chatClient)
    {
        public async Task<string> AskAsync(string question, CancellationToken cancellationToken)
        {
            List<ChatMessage> chatMessages = [
                new ChatMessage(ChatRole.System, $"Jesteś asystentem AI firmy. Dzisiejsza data: {DateTime.Now:yyyy-MM-dd}"),
                new ChatMessage(ChatRole.User, question)
                ];

            var response = await chatClient.GetResponseAsync(chatMessages, cancellationToken: cancellationToken);

            return response.Text;
        }
    }
}
