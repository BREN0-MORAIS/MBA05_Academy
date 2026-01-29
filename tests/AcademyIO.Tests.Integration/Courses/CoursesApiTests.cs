using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;

namespace AcademyIO.Tests.Integration.Courses;

public class CoursesApiTests : IClassFixture<WebApplicationFactory<AcademyIO.Courses.API.Program>>
{
    private readonly HttpClient _client;
    private readonly WebApplicationFactory<AcademyIO.Courses.API.Program> _factory;

    public CoursesApiTests(WebApplicationFactory<AcademyIO.Courses.API.Program> factory)
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
    public async Task GetCourses_WithoutAuth_ShouldReturnUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/api/courses");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }

    [Fact]
    public async Task GetLessons_WithoutAuth_ShouldReturnUnauthorized()
    {
        // Act
        var response = await _client.GetAsync("/api/lessons");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
    }
}
