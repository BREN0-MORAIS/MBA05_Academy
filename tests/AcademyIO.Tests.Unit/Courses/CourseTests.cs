using AcademyIO.Courses.API.Models;

namespace AcademyIO.Tests.Unit.Courses;

public class CourseTests
{
    [Fact]
    public void Course_NewCourse_ShouldHaveNewGuid()
    {
        // Arrange & Act
        var course = new Course();

        // Assert
        course.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Course_SetProperties_ShouldRetainValues()
    {
        // Arrange
        var course = new Course();

        // Act
        course.Name = "Curso de .NET";
        course.Description = "Aprenda .NET do zero";
        course.Price = 199.90;

        // Assert
        course.Name.Should().Be("Curso de .NET");
        course.Description.Should().Be("Aprenda .NET do zero");
        course.Price.Should().Be(199.90);
    }

    [Fact]
    public void Course_Price_ShouldBePositive()
    {
        // Arrange
        var course = new Course
        {
            Price = 100.00
        };

        // Assert
        course.Price.Should().BePositive();
    }

    [Fact]
    public void Course_Lessons_ShouldBeNullInitially()
    {
        // Arrange & Act
        var course = new Course();

        // Assert
        course.Lessons.Should().BeNull();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Course_EmptyOrNullName_ShouldBeAllowed(string name)
    {
        // Arrange & Act
        var course = new Course
        {
            Name = name
        };

        // Assert - Model allows empty/null (validation is done at API level)
        course.Name.Should().Be(name);
    }

    [Fact]
    public void Course_TwoCourses_ShouldHaveDifferentIds()
    {
        // Arrange
        var course1 = new Course();
        var course2 = new Course();

        // Assert
        course1.Id.Should().NotBe(course2.Id);
    }
}
