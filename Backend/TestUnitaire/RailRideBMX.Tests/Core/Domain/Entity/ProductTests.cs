using System.Collections;
using Core.Domain.Entity;
using NUnit.Framework;

namespace TestUnitaire.RailRideBMX.Tests.Core.Domain.Entity;

[TestFixture]
public class ProductTests
{
    [Test]
    public void Product_Should_Have_Default_Values()
    {
        // Arrange
        var product = new Product();

        // Assert
        Assert.AreEqual(default(Guid), product.Id);
        Assert.IsNull(product.Title);
        Assert.IsNull(product.Description);
        Assert.IsNull(product.Image);
        Assert.AreEqual(0f, product.Price);
        Assert.AreEqual(0, product.Quantity);
        Assert.AreEqual(default(Guid), product.CategoryId);
        Assert.IsNull(product.Category);
        Assert.AreEqual(default(Guid), product.ColorId);
        Assert.IsNull(product.Color);
        Assert.AreEqual(default(Guid), product.BrandId);
        Assert.IsNull(product.Brand);
        Assert.IsNull(product.SubCategory);
        Assert.IsNull(product.ConfigCategory);
        Assert.IsNull(product.Geometry);
        Assert.IsNull(product.FrameSize);
        Assert.IsNull(product.HandlebarSize);
        Assert.IsNull(product.WheelSize);
        Assert.IsNull(product.ProductCarts);
        Assert.IsNull(product.Like);
        Assert.IsEmpty(product.Comment);
    }
}