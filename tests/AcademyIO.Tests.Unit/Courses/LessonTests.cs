using AcademyIO.Courses.API.Models;

namespace AcademyIO.Tests.Unit.Courses;

public class LessonTests
{
    [Fact]
    public void Lesson_NewLesson_ShouldHaveCorrectProperties()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var name = "Introducao ao C#";
        var subject = "Conceitos basicos de C#";
        var totalHours = 2.5;

        // Act
        var lesson = new Lesson(name, subject, totalHours, courseId);

        // Assert
        lesson.Name.Should().Be(name);
        lesson.Subject.Should().Be(subject);
        lesson.TotalHours.Should().Be(totalHours);
        lesson.CourseId.Should().Be(courseId);
    }

    [Fact]
    public void Lesson_NewLesson_ShouldHaveNewGuid()
    {
        // Arrange & Act
        var lesson = new Lesson("Test", "Test Subject", 1.0, Guid.NewGuid());

        // Assert
        lesson.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Lesson_UpdateProperties_ShouldRetainNewValues()
    {
        // Arrange
        var lesson = new Lesson("Original", "Original Subject", 1.0, Guid.NewGuid());

        // Act
        lesson.Name = "Updated";
        lesson.Subject = "Updated Subject";
        lesson.TotalHours = 3.0;

        // Assert
        lesson.Name.Should().Be("Updated");
        lesson.Subject.Should().Be("Updated Subject");
        lesson.TotalHours.Should().Be(3.0);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(0.5)]
    [InlineData(1)]
    [InlineData(4)]
    [InlineData(8)]
    public void Lesson_TotalHours_ShouldAcceptVariousValues(double hours)
    {
        // Arrange & Act
        var lesson = new Lesson("Test", "Test", hours, Guid.NewGuid());

        // Assert
        lesson.TotalHours.Should().Be(hours);
    }

    [Fact]
    public void Lesson_TwoLessons_ShouldHaveDifferentIds()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var lesson1 = new Lesson("Lesson 1", "Subject 1", 1.0, courseId);
        var lesson2 = new Lesson("Lesson 2", "Subject 2", 2.0, courseId);

        // Assert
        lesson1.Id.Should().NotBe(lesson2.Id);
    }

    [Fact]
    public void Lesson_SameCourseId_ShouldBeAssociatedToSameCourse()
    {
        // Arrange
        var courseId = Guid.NewGuid();
        var lesson1 = new Lesson("Lesson 1", "Subject 1", 1.0, courseId);
        var lesson2 = new Lesson("Lesson 2", "Subject 2", 2.0, courseId);

        // Assert
        lesson1.CourseId.Should().Be(lesson2.CourseId);
    }
}
