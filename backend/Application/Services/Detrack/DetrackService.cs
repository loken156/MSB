using Application.Dto.Detrack;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Application.Services.Detrack
{
    public class DetrackService : IDetrackService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DetrackService> _logger;
        private const string DetrackApiUrl = "https://app.detrack.com/api/v2/delivery"; // Replace with the actual API endpoint
        private const string ApiKey = "7d5e523be182472211381fa6a6ba508e5919251796bb588a";

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