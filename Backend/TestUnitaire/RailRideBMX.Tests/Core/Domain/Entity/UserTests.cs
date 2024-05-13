using Core.Domain.Entity;

namespace TestUnitaire.RailRideBMX.Tests.Core.Domain.Entity
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void User_Should_Have_Default_Values()
        {
            var user = new User();
            Assert.AreEqual(Guid.Empty, user.Id);
            Assert.IsNull(user.Firstname);
            Assert.IsNull(user.Lastname);
            Assert.IsNull(user.Email);
            Assert.IsNull(user.Password);
            Assert.IsNull(user.Role);
            Assert.IsNotNull(user.Like);
            Assert.IsEmpty(user.Like);
            Assert.IsNotNull(user.Configurations);
            Assert.IsEmpty(user.Configurations);
            Assert.IsNull(user.ResetPassWordToken);
            Assert.IsNull(user.ResetPasswordTokenExpiration);
        }
    }
}