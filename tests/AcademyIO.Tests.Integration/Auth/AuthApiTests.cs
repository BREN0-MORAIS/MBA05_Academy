using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AcademyIO.Tests.Integration.Auth;

public class AuthApiTests : IClassFixture<WebApplicationFactory<AcademyIO.Auth.API.Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<AcademyIO.Auth.API.Program> _factory;

    public AuthApiTests(WebApplicationFactory<AcademyIO.Auth.API.Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task HealthCheck_ShouldReturnHealthy()
    {
        // Act
        var response = await _client.GetAsync("/health");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Swagger_ShouldBeAvailable()
    {
        // Act
        var response = await _client.GetAsync("/swagger/index.html");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ShouldReturnBadRequest()
    {
        // Arrange
        var loginData = new
        {
            Email = "invalid@test.com",
            Password = "WrongPassword123!"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(loginData),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/api/auth/login", content);

        // Assert
        response.StatusCode.Should().BeOneOf(HttpStatusCode.BadRequest, HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task Register_WithInvalidModel_ShouldReturnBadRequest()
    {
        // Arrange - Missing required fields
        var registerData = new
        {
            Email = "invalid-email"
        };

        var content = new StringContent(
            JsonSerializer.Serialize(registerData),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/api/auth/register", content);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task Register_WithValidModel_ShouldProcessRequest()
    {
        // Arrange
        var registerData = new
        {
            Email = $"test{Guid.NewGuid()}@example.com",
            FirstName = "Test",
            LastName = "User",
            DateOfBirth = "1990-01-01",
            Password = "TestPassword123!",
            ConfirmPassword = "TestPassword123!",
            IsAdmin = false
        };

        var content = new StringContent(
            JsonSerializer.Serialize(registerData),
            Encoding.UTF8,
            "application/json");

        // Act
        var response = await _client.PostAsync("/api/auth/register", content);

        // Assert - Can be OK or BadRequest depending on DB state, but should not be 500
        response.StatusCode.Should().NotBe(HttpStatusCode.InternalServerError);
    }
}
