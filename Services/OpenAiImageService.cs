using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using System.Text;

namespace BarberSaloon.Services
{
    public class OpenAiImageService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "";                                               // api  keyini buraya giriniz
        public OpenAiImageService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
            
        }

        public async Task<string> GenerateImageAsync(string prompt)
        {
            Console.WriteLine("Sending request to OpenAI with prompt: " + prompt); // Konsola bilgi yazdırma

            var content = new StringContent(JsonSerializer.Serialize(new
            {
                prompt = "generate hairstyle image of  a man for  this guys  face with"  + prompt,
                n = 1,
                size = "1024x1024"
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/images/generations", content);
            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("Received successful response from OpenAI."); // Başarılı yanıt geldiğinde bilgi yazdırma
                var jsonResponse = await response.Content.ReadAsStringAsync();
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    var url = doc.RootElement.GetProperty("data").EnumerateArray().FirstOrDefault().GetProperty("url").GetString();
                    return url; // JSON'dan URL'yi çekip döndürme
                }
            }
            else
            {
                Console.WriteLine("Failed to receive a successful response from OpenAI."); // Hata olduğunda bilgi yazdırma
                throw new Exception($"API call failed: {await response.Content.ReadAsStringAsync()}");
            }
        }
    }
}









/*
       
        
        public async Task<string> GenerateImageAsync(string prompt)
        {
            var content = new StringContent(JsonSerializer.Serialize(new
            {
                prompt = prompt,
                n = 1,
                size = "1024x1024"
            }), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/images/generations", content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("API Response: {Response}", jsonResponse); // API'den gelen yanıtı loglayın

            if (response.IsSuccessStatusCode)
            {
                using (JsonDocument doc = JsonDocument.Parse(jsonResponse))
                {
                    var url = doc.RootElement.GetProperty("data").EnumerateArray().FirstOrDefault().GetProperty("url").GetString();
                    return url; // JSON'dan URL'yi çekip döndürme
                }
            }
            else
            {
                Console.WriteLine("API call failed with status code {StatusCode} and response {Response}", response.StatusCode, jsonResponse);
                throw new Exception($"API call failed: {jsonResponse}");
            }
        }
        
*/