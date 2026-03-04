using SmartPole.Inventory.Domain.Common;
using Xunit;

namespace SmartPole.Inventory.UnitTests.Domain.Common;

public class EntityTests {
  private class TestEntity : Entity<Guid> {
    public TestEntity(Guid id) : base(id) { }
  }

  [Fact]
  public void EntitiesWithSameIdShouldBeEqual() {
    var id = Guid.NewGuid();
    var entity1 = new TestEntity(id);
    var entity2 = new TestEntity(id);

    Assert.Equal(entity1, entity2);
    Assert.True(entity1 == entity2);
  }

  [Fact]
  public void EntitiesWithDifferentIdShouldNotBeEqual() {
    var entity1 = new TestEntity(Guid.NewGuid());
    var entity2 = new TestEntity(Guid.NewGuid());

    Assert.NotEqual(entity1, entity2);
    Assert.True(entity1 != entity2);
  }
}

public class ValueObjectTests {
  private class TestValueObject : ValueObject {
    public string Prop1 { get; }
    public int Prop2 { get; }

    public TestValueObject(string prop1, int prop2) {
      Prop1 = prop1;
      Prop2 = prop2;
    }

    protected override IEnumerable<object> GetEqualityComponents() {
      yield return Prop1;
      yield return Prop2;
    }
  }

  [Fact]
  public void ValueObjectsWithSamePropertiesShouldBeEqual() {
    var vo1 = new TestValueObject("test", 1);
    var vo2 = new TestValueObject("test", 1);

    Assert.Equal(vo1, vo2);
    Assert.True(vo1 == vo2);
  }

  [Fact]
  public void ValueObjectsWithDifferentPropertiesShouldNotBeEqual() {
    var vo1 = new TestValueObject("test", 1);
    var vo2 = new TestValueObject("test", 2);

    Assert.NotEqual(vo1, vo2);
    Assert.True(vo1 != vo2);
  }
}
