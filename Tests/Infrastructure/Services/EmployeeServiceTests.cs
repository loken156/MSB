using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace Tests.Infrastructure.Services
{
    public class EmployeeServiceTests
    {
        private readonly Mock<UserManager<ApplicationUser>> mockUserManager;
        private readonly EmployeeService employeeService;

        // Constructor with mock setup for UserManager which is used in EmployeeService
        public EmployeeServiceTests()
        {
            mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(),
                new Mock<IOptions<IdentityOptions>>().Object,
                new Mock<IPasswordHasher<ApplicationUser>>().Object,
                Array.Empty<IUserValidator<ApplicationUser>>(),
                Array.Empty<IPasswordValidator<ApplicationUser>>(),
                new Mock<ILookupNormalizer>().Object,
                new Mock<IdentityErrorDescriber>().Object,
                new Mock<IServiceProvider>().Object,
                new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
            employeeService = new EmployeeService(mockUserManager.Object);
        }

        // Test to verify that FindByIdAsync returns null when user does not exist
        [Fact]
        public async Task FindByIdAsync_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "non-existing-user-id";
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser?)null);

            // Act
            var result = await employeeService.FindByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        // Test to verify that ChangePasswordAsync returns failed when user does not exist
        [Fact]
        public async Task ChangePasswordAsync_ReturnsFailed_WhenUserDoesNotExist()
        {
            // Arrange
            var userId = "non-existing-user-id";
            var currentPassword = "CurrentPassword123";
            var newPassword = "NewPassword123";
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser?)null);

            // Act
            var result = await employeeService.ChangePasswordAsync(userId, currentPassword, newPassword);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("User not found", result.Errors);
        }

        // Test to verify that ChangePasswordAsync returns failed when password change fails
        [Fact]
        public async Task ChangePasswordAsync_ReturnsFailed_WhenPasswordChangeFails()
        {
            // Arrange
            var userId = "test-user-id";
            var currentPassword = "CurrentPassword123";
            var newPassword = "NewPassword123";
            var expectedUser = new ApplicationUser { Id = userId };
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(expectedUser);
            mockUserManager.Setup(x => x.ChangePasswordAsync(expectedUser, currentPassword, newPassword))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password change failed" }));

            // Act
            var result = await employeeService.ChangePasswordAsync(userId, currentPassword, newPassword);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Password change failed", result.Errors);
        }

        // Test to verify that ChangePasswordAsync returns failed when current password is incorrect
        [Fact]
        public async Task ChangePasswordAsync_ReturnsFailed_WhenCurrentPasswordIsIncorrect()
        {
            // Arrange
            var userId = "test-user-id";
            var incorrectCurrentPassword = "IncorrectPassword123";
            var newPassword = "NewPassword123";
            var expectedUser = new ApplicationUser { Id = userId };
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(expectedUser);
            mockUserManager.Setup(x => x.ChangePasswordAsync(expectedUser, incorrectCurrentPassword, newPassword))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Incorrect current password" }));

            // Act
            var result = await employeeService.ChangePasswordAsync(userId, incorrectCurrentPassword, newPassword);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Incorrect current password", result.Errors);
        }

        // Test to verify that ChangePasswordAsync returns failed when new password is invalid
        [Fact]
        public async Task ChangePasswordAsync_ReturnsFailed_WhenNewPasswordIsInvalid()
        {
            // Arrange
            var userId = "test-user-id";
            var currentPassword = "CurrentPassword123";
            var invalidNewPassword = "invalid";
            var expectedUser = new ApplicationUser { Id = userId };
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(expectedUser);
            mockUserManager.Setup(x => x.ChangePasswordAsync(expectedUser, currentPassword, invalidNewPassword))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "New password is invalid" }));

            // Act
            var result = await employeeService.ChangePasswordAsync(userId, currentPassword, invalidNewPassword);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("New password is invalid", result.Errors);
        }
    }
}