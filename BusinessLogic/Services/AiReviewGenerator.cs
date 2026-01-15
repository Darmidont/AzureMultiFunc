using Azure;
using Azure.AI.OpenAI;
using BusinessLogic.Interfaces;
using Data.Entities;
using OpenAI.Chat;

namespace BusinessLogic.Services
{
    public class AiReviewGenerator : IAiReviewGenerator
    {

        public async Task<string?> GenerateReviewAsync(Product product)
        {
            var endpoint = new Uri("https://azureopenaidra.openai.azure.com/");
            var deploymentName = "gpt-4o";
            var apiKey = Environment.GetEnvironmentVariable("ApiKey");

            AzureOpenAIClient azureClient = new(
                endpoint,
                new AzureKeyCredential(apiKey));
            ChatClient chatClient = azureClient.GetChatClient(deploymentName);

            var requestOptions = new ChatCompletionOptions()
            {
                MaxOutputTokenCount = 4096,
                Temperature = 1.0f,
                TopP = 1.0f,

            };

            var reviews = product.Reviews.Select(r => r.Body).ToList();
            var reviewMessage = string.Join("\n\n", reviews);
            List<ChatMessage> messages = new List<ChatMessage>()
            {
                new SystemChatMessage($"You are an AI agent that compiles a review summary for product {product.Name} based on {product.Reviews.Count} reviews"),
                new UserChatMessage(reviewMessage)
            };

            var response = await chatClient.CompleteChatAsync(messages, requestOptions);
            var reviewSummary = response.Value.Content[0].Text;
            return reviewSummary;
        }
    }
}
