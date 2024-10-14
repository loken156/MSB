using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Infrastructure.Services.Detrack.DTOs;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services.Detrack
{
    public class DetrackService : IDetrackService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DetrackService> _logger;
        private const string DetrackApiUrl = "https://app.detrack.com/api/v2/delivery"; // Replace with the actual API endpoint
        private const string ApiKey = "9943520c80ee2aaad2cc80c29bdfb298e85feed021ef0328";

        public DetrackService(HttpClient httpClient, ILogger<DetrackService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<bool> CreateDetrackJobAsync(DetrackJobDto job)
        {
            try
            {
                // Prepare the request data
                var requestData = JsonSerializer.Serialize(job);
                var content = new StringContent(requestData, Encoding.UTF8, "application/json");

                // Add the API key header
                _httpClient.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

                // Send the request
                var response = await _httpClient.PostAsync(DetrackApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully created a Detrack job.");
                    return true;
                }

                _logger.LogError("Failed to create Detrack job. Status Code: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a Detrack job.");
                return false;
            }
        }
    }
}