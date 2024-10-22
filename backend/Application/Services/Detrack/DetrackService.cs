using Application.Dto.Detrack;
using Application.Dto.Order;
using Infrastructure.Repositories.AddressRepo;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace Application.Services.Detrack
{
    public class DetrackService : IDetrackService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<DetrackService> _logger;

        private const string
            DetrackApiUrl = "https://app.detrack.com/api/v2/dn/jobs"; // Replace with the actual API endpoint

        private const string ApiKey = "7d5e523be182472211381fa6a6ba508e5919251796bb588a";
        private readonly IAddressRepository _addressRepository; // Inject AddressRepository

        public DetrackService(HttpClient httpClient, ILogger<DetrackService> logger,
            IAddressRepository addressRepository)
        {
            _httpClient = httpClient;
            _logger = logger;
            _addressRepository = addressRepository; // Initialize AddressRepository
        }

        public async Task<bool> CreateDetrackJobAsync(OrderDto orderDto)
        {
            try
            {
                // Fetch the address from the database based on AddressId
                var address = await _addressRepository.GetAddressByIdAsync(orderDto.AddressId);
                if (address == null)
                {
                    _logger.LogError("Address not found for ID: {AddressId}", orderDto.AddressId);
                    return false;
                }

                // Create the DetrackJobDto with nested data object
                var detrackJob = new DetrackJobDto
                {
                    data = new DetrackJobDto.JobData
                    {
                        do_number = orderDto.OrderId.ToString(), // Unique order reference number
                        date = DateTime.Now.ToString("yyyy-MM-dd"),
                        order_number = orderDto.OrderNumber.ToString(),
                        address = $"{address.StreetName}, {address.City}, {address.State} {address.ZipCode}",
                        postal_code = address.ZipCode,
                        deliver_to_collect_from = "Customer Name",  // You may fetch customer name dynamically
                        phone_number = "CustomerPhone", // Fetch phone number dynamically if needed
                        instructions = "Handle with care"
                    }
                };

                // Serialize the request
                var requestData = JsonSerializer.Serialize(detrackJob);
                var content = new StringContent(requestData, Encoding.UTF8, "application/json");

                // Add the API key header
                _httpClient.DefaultRequestHeaders.Add("X-API-KEY", ApiKey);

                // Send the request to the Detrack API
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
