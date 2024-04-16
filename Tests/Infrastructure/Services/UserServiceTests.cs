using Infrastructure.Entities;
using Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Infrastructure.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task FindByIdAsync_ReturnsNull_WhenUserDoesNotExist()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var userId = "non-existing-user-id";
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);
            var userService = new UserService(mockUserManager.Object);

            // Act
            var result = await userService.FindByIdAsync(userId);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnsFailed_WhenUserDoesNotExist()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var userId = "non-existing-user-id";
            var currentPassword = "CurrentPassword123";
            var newPassword = "NewPassword123";
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync((ApplicationUser)null);
            var userService = new UserService(mockUserManager.Object);

            // Act
            var result = await userService.ChangePasswordAsync(userId, currentPassword, newPassword);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("User not found", result.Errors);
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnsFailed_WhenPasswordChangeFails()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var userId = "test-user-id";
            var currentPassword = "CurrentPassword123";
            var newPassword = "NewPassword123";
            var expectedUser = new ApplicationUser { Id = userId };
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(expectedUser);
            mockUserManager.Setup(x => x.ChangePasswordAsync(expectedUser, currentPassword, newPassword))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password change failed" }));
            var userService = new UserService(mockUserManager.Object);

            // Act
            var result = await userService.ChangePasswordAsync(userId, currentPassword, newPassword);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Password change failed", result.Errors);
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnsFailed_WhenCurrentPasswordIsIncorrect()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var userId = "test-user-id";
            var incorrectCurrentPassword = "IncorrectPassword123";
            var newPassword = "NewPassword123";
            var expectedUser = new ApplicationUser { Id = userId };
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(expectedUser);
            mockUserManager.Setup(x => x.ChangePasswordAsync(expectedUser, incorrectCurrentPassword, newPassword))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Incorrect current password" }));
            var userService = new UserService(mockUserManager.Object);

            // Act
            var result = await userService.ChangePasswordAsync(userId, incorrectCurrentPassword, newPassword);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("Incorrect current password", result.Errors);
        }

        [Fact]
        public async Task ChangePasswordAsync_ReturnsFailed_WhenNewPasswordIsInvalid()
        {
            // Arrange
            var mockUserManager = new Mock<UserManager<ApplicationUser>>(
                Mock.Of<IUserStore<ApplicationUser>>(), null, null, null, null, null, null, null, null);
            var userId = "test-user-id";
            var currentPassword = "CurrentPassword123";
            var invalidNewPassword = "invalid";
            var expectedUser = new ApplicationUser { Id = userId };
            mockUserManager.Setup(x => x.FindByIdAsync(userId)).ReturnsAsync(expectedUser);
            mockUserManager.Setup(x => x.ChangePasswordAsync(expectedUser, currentPassword, invalidNewPassword))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "New password is invalid" }));
            var userService = new UserService(mockUserManager.Object);

            // Act
            var result = await userService.ChangePasswordAsync(userId, currentPassword, invalidNewPassword);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Contains("New password is invalid", result.Errors);
        }


    }
}
