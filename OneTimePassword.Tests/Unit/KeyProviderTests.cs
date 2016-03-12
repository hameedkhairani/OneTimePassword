using System;
using System.Configuration;
using NUnit.Framework;
using OneTimePassword.App.Contracts;
using OneTimePassword.App.Domain;

namespace OneTimePassword.Tests.Unit
{
    public class KeyProviderTests
    {
        private IKeyProvider _keyProvider;
        private const string ExpectedKey = "some_valid_key";

        [SetUp]
        public void SetUp()
        {
            _keyProvider = new AppConfigKeyProvider();
        }

        [Test]
        public void GivenValidAppSettings_WhenHashKeyIsRequested_ThenReturnsValidKey()
        {
            ConfigurationManager.AppSettings["HashKey"] = ExpectedKey;
            var actualKey = _keyProvider.GetHashKey();
            Assert.That(actualKey, Is.EqualTo(ExpectedKey));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void GivenInvalidAppSettings_WhenHashKeyIsRequested_ThenThrowsException(string hashKey)
        {
            ConfigurationManager.AppSettings["HashKey"] = hashKey;
            var exception = Assert.Throws<Exception>(() => _keyProvider.GetHashKey());
            Assert.That(exception.Message, Is.EqualTo("HashKey not found"));
        }
    }
}
