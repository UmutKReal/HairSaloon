using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace BarberSaloon.Services
{
    public class LightXService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;

        public LightXService(HttpClient httpClient, IOptions<LightXApiSettings> apiSettings)
        {
            _httpClient = httpClient;
            _apiKey = apiSettings.Value.ApiKey;
        }

        public async Task<string> ProcessHairstyleAsync(string imageUrl, string textPrompt)
        {
            if (string.IsNullOrWhiteSpace(imageUrl) || string.IsNullOrWhiteSpace(textPrompt))
            {
                throw new ArgumentException("Image URL and text prompt must be provided.");
            }

            // Ensure the image URL is public
            if (!Uri.IsWellFormedUriString(imageUrl, UriKind.Absolute))
            {
                throw new ArgumentException("The provided image URL is not valid.");
            }

            var request = new
            {
                imageUrl = imageUrl,  // Public URL of the image
                textPrompt = textPrompt  // User's input text
            };

            var jsonContent = JsonConvert.SerializeObject(request);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "https://api.lightxeditor.com/external/api/v1/hairstyle")
            {
                Content = content
            };

            // Add headers
            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(_apiKey);
            requestMessage.Headers.Add("x-api-key", _apiKey); // API key header

            // Log sent headers (optional)
            Console.WriteLine("Sent Headers:");
            foreach (var header in requestMessage.Headers)
            {
                Console.WriteLine($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            var response = await _httpClient.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }

            // Return error details for debugging
            var errorContent = await response.Content.ReadAsStringAsync();
            return $"Error: {response.StatusCode}, Details: {errorContent}";
        }
    }
}
