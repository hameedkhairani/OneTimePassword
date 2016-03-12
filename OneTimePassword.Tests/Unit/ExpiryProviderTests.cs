using System.Configuration;
using NUnit.Framework;
using OneTimePassword.Contracts;
using OneTimePassword.Domain;

namespace OneTimePassword.Tests.Unit
{
    public class ExpiryProviderTests
    {
        private IExpiryProvider _expiryProvider;
        private const int ExpectedExpiry = 10;
        private const int DefaultExpiry = 30;

        [SetUp]
        public void SetUp()
        {
            _expiryProvider = new AppConfigExpiryProvider();
        }

        [Test]
        public void GivenValidAppSettings_WhenExpiryIsRequested_ThenReturnsValidExpiry()
        {
            ConfigurationManager.AppSettings["PasswordExpiryInSeconds"] = ExpectedExpiry.ToString();
            var actualExpiry = _expiryProvider.GetExpiryInSeconds();
            Assert.That(actualExpiry, Is.EqualTo(ExpectedExpiry));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void GivenInvalidAppSettings_WhenExpiryIsRequested_ThenReturnsDefaultExpiry(string expiry)
        {
            ConfigurationManager.AppSettings["PasswordExpiryInSeconds"] = expiry;
            var actualExpiry = _expiryProvider.GetExpiryInSeconds();
            Assert.That(actualExpiry, Is.EqualTo(DefaultExpiry));
        }
    }
}
