using Application.Queries.Driver.GetById;
using Domain.Models.Driver;
using Infrastructure.Repositories.DriverRepo;
using Moq;

namespace Tests.Application.Driver.QueryHandlers
{
    public class GetDriverByIdQueryHandlerTests
    {
        private readonly Mock<IDriverRepository> _mockDriverRepository;
        private readonly GetDriverByIdQueryHandler _handler;

        public GetDriverByIdQueryHandlerTests()
        {
            _mockDriverRepository = new Mock<IDriverRepository>();
            _handler = new GetDriverByIdQueryHandler(_mockDriverRepository.Object);
        }

        [Fact]
        public async Task Handle_GivenValidId_ReturnsDriver()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            var driver = new DriverModel { Id = driverId.ToString(), FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", LicenseNumber = "123456" };
            _mockDriverRepository.Setup(m => m.GetDriverByIdAsync(driverId)).ReturnsAsync(driver);

            // Act
            var result = await _handler.Handle(new GetDriverByIdQuery(driverId));

            // Assert
            Assert.Equal(driverId, result.DriverId);
            Assert.Equal(driver.FirstName, result.Employee.FirstName);
            Assert.Equal(driver.LastName, result.Employee.LastName);
            Assert.Equal(driver.Email, result.Employee.Email);
            Assert.Equal(driver.LicenseNumber, result.LicenseNumber);
        }

        [Fact]
        public async Task Handle_GivenInvalidId_ThrowsException()
        {
            // Arrange
            var driverId = Guid.NewGuid();
            _mockDriverRepository.Setup(m => m.GetDriverByIdAsync(driverId)).ReturnsAsync((DriverModel?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _handler.Handle(new GetDriverByIdQuery(driverId)));
        }
    }
}
