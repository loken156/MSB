using Domain.Models.Car;
using Domain.Models.Employee;
using Domain.Models.Order;
using Infrastructure.Repositories.AddressRepo;
using Infrastructure.Repositories.CarRepo;
using Infrastructure.Repositories.EmployeeRepo;
using Infrastructure.Repositories.OrderRepo;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

// This class represents a delivery service responsible for scheduling deliveries.
// It depends on various repositories for accessing car, employee, address, and order data,
// as well as IConfiguration for retrieving the Detrack API key.
// The ScheduleDeliveries method schedules deliveries by assigning pending orders to available cars and drivers.
// It retrieves all pending orders, then iterates through available cars and drivers, assigning up to 10 orders to each.
// The AssignDeliveriesToDriver method assigns deliveries to a specific driver by sending requests to the Detrack API.
// It constructs the necessary payload for each delivery and sends a POST request to the Detrack API to create the delivery.
// Upon successful creation of a delivery, it updates the order status to "Assigned" in the database.

namespace Application.Services
{
    public class DeliveryService
    {
        private readonly ICarRepository _carRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _detrackApiKey;

        public DeliveryService(ICarRepository carRepository, IEmployeeRepository employeeRepository,
                               IAddressRepository addressRepository, IOrderRepository orderRepository,
                               IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _carRepository = carRepository;
            _employeeRepository = employeeRepository;
            _addressRepository = addressRepository;
            _orderRepository = orderRepository;
            _httpClientFactory = httpClientFactory;
            _detrackApiKey = configuration["DetrackApiKey"];
        }

        public async Task ScheduleDeliveries()
        {
            // Retrieve all cars and employees
            var cars = await _carRepository.GetAllCars();
            var employees = await _employeeRepository.GetEmployeesAsync();

            // Get all pending orders
            var orders = await _orderRepository.GetAllOrdersAsync();
            var pendingOrders = orders.Where(o => o.OrderStatus == "Pending").ToList();

            // Assign deliveries to available cars and drivers
            foreach (var car in cars)
            {
                if (car.Availability == "Available" && car.DriverId != null)
                {
                    var driver = await _employeeRepository.GetEmployeeByIdAsync(car.DriverId.Value);
                    if (driver != null)
                    {
                        var deliveries = pendingOrders.Take(10).ToList(); // Assign up to 10 orders to each car
                        pendingOrders.RemoveAll(d => deliveries.Contains(d));

                        await AssignDeliveriesToDriver(car, driver, deliveries);
                    }
                }
            }
        }

        private async Task AssignDeliveriesToDriver(CarModel car, EmployeeModel driver, List<OrderModel> deliveries)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Add("X-Api-Key", _detrackApiKey);

            var tasks = deliveries.Select(async order =>
            {
                var address = await _addressRepository.GetAddressByIdAsync(order.AddressId.Value);
                if (address != null)
                {
                    var detrackRequest = new
                    {
                        job_id = order.OrderId,
                        date = order.OrderDate.ToString("yyyy-MM-dd"),
                        vehicle_number = car.CarId.ToString(),
                        driver_name = driver.FirstName + " " + driver.LastName,
                        address = $"{address.StreetNumber} {address.StreetName}, {address.City}, {address.State} {address.ZipCode}",
                        latitude = address.Latitude,
                        longitude = address.Longitude
                    };

                    var response = await client.PostAsJsonAsync("https://app.detrack.com/api/v2/deliveries", detrackRequest);
                    response.EnsureSuccessStatusCode();

                    // Update order status
                    order.OrderStatus = "Assigned";
                    await _orderRepository.UpdateOrderAsync(order);
                }
            });

            await Task.WhenAll(tasks);
        }
    }

}