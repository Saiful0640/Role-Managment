
using RazorMVC.Models.Settings;
using System.Text;
using System.Text.Json;

namespace RazorMVC.Services
{
    public class SettingService
    {
        private readonly HttpClient _httpCLient;
        public SettingService(HttpClient httpClient)
        {
            _httpCLient = httpClient;
        }


        public async Task<List<Settings>> GetSettingsByCategoryAsync(string category)
        {
            var response= await _httpCLient.GetAsync($"api/Setting/category/test{category}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();

           
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Handle case insensitivity
                };

                var settings = JsonSerializer.Deserialize<List<Settings>>(json, options);

                return settings ?? new List<Settings>(); // Ensure it's not null
            }

          
            return new List<Settings>();
        }



        public async Task SaveOrUpdateSettingAsync(string key, string value)
        {
            // Validate key and value
            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));
            }

            var settings = new Dictionary<string, string>
            {
                { key, value ?? string.Empty } // Ensure value is not null; if it is, use an empty string
            };

            var json = JsonSerializer.Serialize(settings);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Adjust URL to the correct API endpoint
            var response = await _httpCLient.PostAsync("api/Setting/SettingSave", content);

            // Ensure successful status code
            response.EnsureSuccessStatusCode();
        }
    }
}
