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
            DetrackApiUrl = "https://app.detrack.com/api/v2/delivery"; // Replace with the actual API endpoint

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
                    return false; // Fail if the address is not found
                }

                // Now you can use the fetched address details to create the DetrackJobDto
                var detrackJob = new DetrackJobDto
                {
                    DoNumber = orderDto.OrderId.ToString(), // Unique order reference number
                    OrderNumber = orderDto.OrderNumber.ToString(),
                    Address = $"{address.StreetName}, {address.City}, {address.State} {address.ZipCode}",
                    PostalCode = address.ZipCode,
                    City = address.City,
                    Country = address.Country,
                    DeliverTo = "Customer Name", // You may adjust this if you store customer details separately
                    PhoneNumber = "CustomerPhone", // Same for phone number, ensure you have this info
                    Date = orderDto.OrderDate.ToString("yyyy-MM-dd"),
                    Instructions = "Handle with care" // Add specific instructions if required
                };

                // Prepare the request data
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
