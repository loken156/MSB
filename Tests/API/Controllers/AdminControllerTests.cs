using API.Controllers;
using Application.Dto.Admin;
using Domain.Models.Admin;
using Infrastructure.Repositories.AdminRepo;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Tests.API.Controllers
{
    public class AdminControllerTests
    {
        private readonly Mock<IAdminRepository> _adminRepositoryMock;
        private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
        private readonly AdminController _controller;

        public AdminControllerTests()
        {
            _adminRepositoryMock = new Mock<IAdminRepository>();
            _userManagerMock = new Mock<UserManager<IdentityUser>>(Mock.Of<IUserStore<IdentityUser>>(), null, null, null, null, null, null, null, null);
            _controller = new AdminController(_adminRepositoryMock.Object, _userManagerMock.Object);
        }

        [Fact]
        public async Task GetAdmins_ReturnsOk_WhenAdminsExist()
        {
            // Arrange
            var admins = new List<AdminModel> { new AdminModel(), new AdminModel() };
            _adminRepositoryMock.Setup(m => m.GetAdminsAsync()).ReturnsAsync(admins);

            // Act
            var result = await _controller.GetAdmins();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<AdminModel>>(okResult.Value);
            Assert.Equal(admins, returnValue);
        }

        [Fact]
        public async Task GetAdmin_ReturnsOk_WhenAdminExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var admin = new AdminModel { Id = id.ToString() };
            _adminRepositoryMock.Setup(m => m.GetAdminAsync(id)).ReturnsAsync(admin);

            // Act
            var result = await _controller.GetAdmin(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<AdminModel>(okResult.Value);
            Assert.Equal(admin, returnValue);
        }

        [Fact]
        public async Task GetAdmin_ReturnsNotFound_WhenAdminDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _adminRepositoryMock.Setup(m => m.GetAdminAsync(id)).ReturnsAsync((AdminModel)null);

            // Act
            var result = await _controller.GetAdmin(id);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateAdmin_ReturnsCreatedAtAction_WhenAdminIsCreated()
        {
            // Arrange
            var adminDto = new AdminDto { AdminId = Guid.NewGuid() };
            var admin = new AdminModel { Id = adminDto.AdminId.ToString() };
            _adminRepositoryMock.Setup(m => m.CreateAdminAsync(It.IsAny<AdminModel>())).ReturnsAsync(admin);
            _userManagerMock.Setup(m => m.FindByIdAsync(admin.Id)).ReturnsAsync(new IdentityUser { Id = admin.Id });
            _userManagerMock.Setup(m => m.AddToRoleAsync(It.IsAny<IdentityUser>(), "Admin")).ReturnsAsync(IdentityResult.Success);

            // Act
            var result = await _controller.CreateAdmin(adminDto);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var returnValue = Assert.IsType<AdminModel>(createdAtActionResult.Value);
            Assert.Equal(admin, returnValue);
        }

        [Fact]
        public async Task UpdateAdmin_ReturnsNoContent_WhenAdminIsUpdated()
        {
            // Arrange
            var id = Guid.NewGuid();
            var adminDto = new AdminDto { AdminId = id };
            var admin = new AdminModel { Id = adminDto.AdminId.ToString() };
            _adminRepositoryMock.Setup(m => m.UpdateAdminAsync(id, It.IsAny<AdminModel>())).ReturnsAsync(admin);

            // Act
            var result = await _controller.UpdateAdmin(id, adminDto);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteAdmin_ReturnsNoContent_WhenAdminIsDeleted()
        {
            // Arrange
            var id = Guid.NewGuid();
            _adminRepositoryMock.Setup(m => m.DeleteAdminAsync(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeleteAdmin(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
