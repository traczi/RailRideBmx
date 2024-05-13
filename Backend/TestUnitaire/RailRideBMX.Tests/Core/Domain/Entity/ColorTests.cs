
using Core.Domain.Entity;

namespace TestUnitaire.RailRideBMX.Tests.Core.Domain.Entity;

[TestFixture]
public class ColorTests
{
    [Test]
    public void Color_Should_Have_Default_Values()
    {
        var color = new Color();
        Assert.AreEqual(Guid.Empty, color.ColorId);
        Assert.IsNull(color.ColorName);
    }
}