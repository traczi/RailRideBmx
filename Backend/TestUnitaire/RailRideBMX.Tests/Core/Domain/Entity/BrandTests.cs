using Core.Domain.Entity;

namespace TestUnitaire.RailRideBMX.Tests.Core.Domain.Entity
{
    [TestFixture]
    public class BrandTests
    {
        [Test]
        public void Brand_Should_Have_Default_Values()
        {
            var brand = new Brand();
            Assert.AreEqual(Guid.Empty, brand.BrandId);
            Assert.IsNull(brand.BrandName);
        }
    }
}