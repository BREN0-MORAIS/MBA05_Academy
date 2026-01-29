using AcademyIO.Core.DomainObjects;
using AcademyIO.Core.Messages;

namespace AcademyIO.Tests.Unit.Core;

public class EntityTests
{
    private class TestEntity : Entity
    {
        public TestEntity() : base() { }
        public TestEntity(Guid id) : base(id) { }
    }

    private class TestEvent : Event { }

    [Fact]
    public void Entity_NewEntity_ShouldHaveNewGuid()
    {
        // Arrange & Act
        var entity = new TestEntity();

        // Assert
        entity.Id.Should().NotBeEmpty();
    }

    [Fact]
    public void Entity_WithSpecificId_ShouldHaveThatId()
    {
        // Arrange
        var expectedId = Guid.NewGuid();

        // Act
        var entity = new TestEntity(expectedId);

        // Assert
        entity.Id.Should().Be(expectedId);
    }

    [Fact]
    public void Entity_AddEvent_ShouldAddEventToNotifications()
    {
        // Arrange
        var entity = new TestEntity();
        var testEvent = new TestEvent();

        // Act
        entity.AddEvent(testEvent);

        // Assert
        entity.Notifications.Should().NotBeNull();
        entity.Notifications.Should().Contain(testEvent);
    }

    [Fact]
    public void Entity_RemoveEvent_ShouldRemoveEventFromNotifications()
    {
        // Arrange
        var entity = new TestEntity();
        var testEvent = new TestEvent();
        entity.AddEvent(testEvent);

        // Act
        entity.RemoveEvent(testEvent);

        // Assert
        entity.Notifications.Should().NotContain(testEvent);
    }

    [Fact]
    public void Entity_CleanEvents_ShouldRemoveAllEvents()
    {
        // Arrange
        var entity = new TestEntity();
        entity.AddEvent(new TestEvent());
        entity.AddEvent(new TestEvent());

        // Act
        entity.CleanEvents();

        // Assert
        entity.Notifications.Should().BeEmpty();
    }

    [Fact]
    public void Entity_Equals_SameId_ShouldBeEqual()
    {
        // Arrange
        var id = Guid.NewGuid();
        var entity1 = new TestEntity(id);
        var entity2 = new TestEntity(id);

        // Act & Assert
        entity1.Equals(entity2).Should().BeTrue();
        (entity1 == entity2).Should().BeTrue();
    }

    [Fact]
    public void Entity_Equals_DifferentId_ShouldNotBeEqual()
    {
        // Arrange
        var entity1 = new TestEntity();
        var entity2 = new TestEntity();

        // Act & Assert
        entity1.Equals(entity2).Should().BeFalse();
        (entity1 != entity2).Should().BeTrue();
    }

    [Fact]
    public void Entity_ToString_ShouldContainTypeNameAndId()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        var result = entity.ToString();

        // Assert
        result.Should().Contain("TestEntity");
        result.Should().Contain(entity.Id.ToString());
    }

    [Fact]
    public void Entity_GetHashCode_ShouldBeConsistent()
    {
        // Arrange
        var entity = new TestEntity();

        // Act
        var hash1 = entity.GetHashCode();
        var hash2 = entity.GetHashCode();

        // Assert
        hash1.Should().Be(hash2);
    }

    [Fact]
    public void Entity_Equals_WithNull_ShouldReturnFalse()
    {
        // Arrange
        var entity = new TestEntity();

        // Act & Assert
        entity.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void Entity_EqualityOperator_BothNull_ShouldReturnTrue()
    {
        // Arrange
        TestEntity entity1 = null;
        TestEntity entity2 = null;

        // Act & Assert
        (entity1 == entity2).Should().BeTrue();
    }

    [Fact]
    public void Entity_EqualityOperator_OneNull_ShouldReturnFalse()
    {
        // Arrange
        var entity1 = new TestEntity();
        TestEntity entity2 = null;

        // Act & Assert
        (entity1 == entity2).Should().BeFalse();
        (entity2 == entity1).Should().BeFalse();
    }
}
