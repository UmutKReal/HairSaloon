using BarberSaloon.Services;
using Microsoft.AspNetCore.Mvc;

namespace BarberSaloon.Controllers
{
    public class CustomerController : Controller
    { 
        private readonly LightXService _lightXService;

        public CustomerController(LightXService lightXService)
        {
            _lightXService = lightXService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Randevu()
        {
            return View();
        }
        public IActionResult CustomerServis()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile image, string textPrompt)
        {
            if (image == null || string.IsNullOrWhiteSpace(textPrompt))
            {
                return BadRequest("Both image and text prompt are required.");
            }

            // Temporarily save the image
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            try
            {
                // Upload the image to a cloud storage to get a public URL
                string imageUrl = await UploadToCloudAsync(filePath);

                // Process the image with LightXService
                var response = await _lightXService.ProcessHairstyleAsync(imageUrl, textPrompt);

                // Return the API response
                return Content(response, "application/json");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            finally
            {
                // Clean up the temporary file
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }
        }

        // Upload to Imgur
        private async Task<string> UploadToCloudAsync(string localFilePath)
        {
            const string imgurApiUrl = "https://api.imgur.com/3/upload";
            const string clientId = "8eebf54a6945180"; // Replace with your Imgur Client ID

            using (var httpClient = new HttpClient())
            {
                // Add Imgur Authorization header
                httpClient.DefaultRequestHeaders.Add("Authorization", "Client-ID " + clientId);

                var form = new MultipartFormDataContent();
                form.Add(new ByteArrayContent(await System.IO.File.ReadAllBytesAsync(localFilePath)), "image");

                var response = await httpClient.PostAsync(imgurApiUrl, form);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
                    return result.data.link; // Return Imgur public URL
                }
                else
                {
                    throw new Exception($"Imgur upload failed: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                }
            }
        }


    }
}
