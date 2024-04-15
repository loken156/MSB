using Application.Queries.Driver.GetAll;
using Domain.Models.Driver;
using Infrastructure.Repositories.DriverRepo;
using Moq;

namespace Tests.Application.Driver.QueryHandlers
{
    public class GetAllDriversQueryHandlerTests
    {
        private readonly Mock<IDriverRepository> _mockDriverRepository;
        private readonly GetAllDriversQueryHandler _handler;

        public GetAllDriversQueryHandlerTests()
        {
            _mockDriverRepository = new Mock<IDriverRepository>();
            _handler = new GetAllDriversQueryHandler(_mockDriverRepository.Object);
        }

        [Fact]
        public async Task Handle_ReturnsAllDrivers()
        {
            // Arrange
            var drivers = new List<DriverModel>
            {
                new DriverModel { Id = Guid.NewGuid().ToString(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", LicenseNumber = "123456" },
                new DriverModel { Id = Guid.NewGuid().ToString(), FirstName = "Jane", LastName = "Doe", Email = "jane.doe@example.com", LicenseNumber = "654321" }
            };
            _mockDriverRepository.Setup(m => m.GetAllDrivers()).ReturnsAsync(drivers);

            // Act
            var result = await _handler.Handle(new GetAllDriversQuery());

            // Assert
            Assert.Equal(drivers.Count, result.Count());
            _mockDriverRepository.Verify(m => m.GetAllDrivers(), Times.Once());
        }

        [Fact]
        public async Task Handle_MapsDriverModelToDriverDetailDtoCorrectly()
        {
            // Arrange
            var driver = new DriverModel { Id = Guid.NewGuid().ToString(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", LicenseNumber = "123456" };
            var drivers = new List<DriverModel> { driver };
            _mockDriverRepository.Setup(m => m.GetAllDrivers()).ReturnsAsync(drivers);

            // Act
            var result = await _handler.Handle(new GetAllDriversQuery());
            var driverDto = result.First();

            // Assert
            Assert.Equal(driver.Id, driverDto.DriverId.ToString());
            Assert.Equal(driver.FirstName, driverDto.Employee.FirstName);
            Assert.Equal(driver.LastName, driverDto.Employee.LastName);
            Assert.Equal(driver.Email, driverDto.Employee.Email);
            Assert.Equal(driver.LicenseNumber, driverDto.LicenseNumber);
        }
    }
}
