using System.ComponentModel.DataAnnotations;
using static AcademyIO.Auth.API.Models.UserViewModel;

namespace AcademyIO.Tests.Unit.Auth;

public class UserViewModelTests
{
    #region RegisterUserViewModel Tests

    [Fact]
    public void RegisterUserViewModel_ValidData_ShouldPassValidation()
    {
        // Arrange
        var model = new RegisterUserViewModel
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Password = "Password123!",
            ConfirmPassword = "Password123!",
            IsAdmin = false
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().BeEmpty();
    }

    [Fact]
    public void RegisterUserViewModel_InvalidEmail_ShouldFailValidation()
    {
        // Arrange
        var model = new RegisterUserViewModel
        {
            Email = "invalid-email",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().Contain(r => r.MemberNames.Contains("Email"));
    }

    [Fact]
    public void RegisterUserViewModel_EmptyEmail_ShouldFailValidation()
    {
        // Arrange
        var model = new RegisterUserViewModel
        {
            Email = "",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().Contain(r => r.MemberNames.Contains("Email"));
    }

    [Fact]
    public void RegisterUserViewModel_ShortPassword_ShouldFailValidation()
    {
        // Arrange
        var model = new RegisterUserViewModel
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Password = "123",
            ConfirmPassword = "123"
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().Contain(r => r.MemberNames.Contains("Password"));
    }

    [Fact]
    public void RegisterUserViewModel_MismatchedPasswords_ShouldFailValidation()
    {
        // Arrange
        var model = new RegisterUserViewModel
        {
            Email = "test@example.com",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Password = "Password123!",
            ConfirmPassword = "DifferentPassword!"
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().Contain(r => r.MemberNames.Contains("ConfirmPassword"));
    }

    [Theory]
    [InlineData("")]
    [InlineData("A")]
    public void RegisterUserViewModel_ShortFirstName_ShouldFailValidation(string firstName)
    {
        // Arrange
        var model = new RegisterUserViewModel
        {
            Email = "test@example.com",
            FirstName = firstName,
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Password = "Password123!",
            ConfirmPassword = "Password123!"
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().Contain(r => r.MemberNames.Contains("FirstName"));
    }

    #endregion

    #region LoginUserViewModel Tests

    [Fact]
    public void LoginUserViewModel_ValidData_ShouldPassValidation()
    {
        // Arrange
        var model = new LoginUserViewModel
        {
            Email = "test@example.com",
            Password = "Password123!"
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().BeEmpty();
    }

    [Fact]
    public void LoginUserViewModel_InvalidEmail_ShouldFailValidation()
    {
        // Arrange
        var model = new LoginUserViewModel
        {
            Email = "not-an-email",
            Password = "Password123!"
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().Contain(r => r.MemberNames.Contains("Email"));
    }

    [Fact]
    public void LoginUserViewModel_EmptyPassword_ShouldFailValidation()
    {
        // Arrange
        var model = new LoginUserViewModel
        {
            Email = "test@example.com",
            Password = ""
        };

        // Act
        var results = ValidateModel(model);

        // Assert
        results.Should().Contain(r => r.MemberNames.Contains("Password"));
    }

    #endregion

    #region Response ViewModels Tests

    [Fact]
    public void LoginResponseViewModel_ShouldHoldTokenData()
    {
        // Arrange & Act
        var response = new LoginResponseViewModel
        {
            AccessToken = "test-token",
            ExpiresIn = 3600,
            UserToken = new UserTokenViewModel
            {
                Id = "user-123",
                Email = "test@example.com"
            }
        };

        // Assert
        response.AccessToken.Should().Be("test-token");
        response.ExpiresIn.Should().Be(3600);
        response.UserToken.Id.Should().Be("user-123");
        response.UserToken.Email.Should().Be("test@example.com");
    }

    [Fact]
    public void ClaimViewModel_ShouldHoldClaimData()
    {
        // Arrange & Act
        var claim = new ClaimViewModel
        {
            Type = "role",
            Value = "admin"
        };

        // Assert
        claim.Type.Should().Be("role");
        claim.Value.Should().Be("admin");
    }

    [Fact]
    public void LoginResponseTestViewModel_DefaultData_ShouldNotBeNull()
    {
        // Arrange & Act
        var response = new LoginResponseTestViewModel();

        // Assert
        response.Data.Should().NotBeNull();
    }

    #endregion

    #region Helper Methods

    private static List<ValidationResult> ValidateModel(object model)
    {
        var context = new ValidationContext(model, null, null);
        var results = new List<ValidationResult>();
        Validator.TryValidateObject(model, context, results, true);
        return results;
    }

    #endregion
}
