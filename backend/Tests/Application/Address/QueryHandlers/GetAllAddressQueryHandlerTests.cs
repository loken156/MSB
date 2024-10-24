﻿using Application.Queries.Address.GetAll;
using Domain.Models.Address;
using Infrastructure.Repositories.AddressRepo;
using Moq;

namespace Tests.Application.Address.QueryHandlers
{
    public class GetAllAddressQueryHandlerTests
    {
        // Test to verify that GetAllAddresses calls GetAllAddressesAsync on repository
        [Fact]
        public async Task Handle_GivenValidQuery_CallsGetAllAddressesAsyncOnRepository()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            mockAddressRepository.Setup(repo => repo.GetAllAddressesAsync()).ReturnsAsync(new List<AddressModel>());
            var handler = new GetAllAddressesQueryHandler(mockAddressRepository.Object);
            var query = new GetAllAddressesQuery();

            // Act
            await handler.Handle(query, new CancellationToken());

            // Assert
            mockAddressRepository.Verify(repo => repo.GetAllAddressesAsync(), Times.Once);
        }

        // Test to verify that GetAllAddresses throws an exception
        [Fact]
        public async Task Handle_GivenInvalidQuery_ThrowsException()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            mockAddressRepository.Setup(repo => repo.GetAllAddressesAsync()).Throws<Exception>();
            var handler = new GetAllAddressesQueryHandler(mockAddressRepository.Object);
            var query = new GetAllAddressesQuery();

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => handler.Handle(query, new CancellationToken()));
        }

        // Test to verify that GetAllAddresses returns correct data
        [Fact]
        public async Task Handle_GivenValidQuery_ReturnsCorrectData()
        {
            // Arrange
            var mockAddressRepository = new Mock<IAddressRepository>();
            var addresses = new List<AddressModel> { new AddressModel { AddressId = Guid.NewGuid(), StreetName = "Street1", UnitNumber="#01-02", City = "City1", Country = "Country1", ZipCode = "ZipCode1" } };
            mockAddressRepository.Setup(repo => repo.GetAllAddressesAsync()).ReturnsAsync(addresses);
            var handler = new GetAllAddressesQueryHandler(mockAddressRepository.Object);
            var query = new GetAllAddressesQuery();

            // Act
            var result = await handler.Handle(query, new CancellationToken());

            // Assert
            Assert.Equal(addresses, result);
        }
    }
}