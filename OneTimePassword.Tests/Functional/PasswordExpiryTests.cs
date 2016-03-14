using System;
using System.Threading;
using Moq;
using NUnit.Framework;
using OneTimePassword.Contracts;
using OneTimePassword.Domain;

namespace OneTimePassword.Tests.Functional
{
    public class PasswordExpiryTests
    {
        private Mock<ITimeProvider> _timeProvider;
        private IKeyProvider _keyProvider;
        private IExpiryProvider _expiryProvider;
        private IHashGenerator _hashGenerator;
        private IPasswordGenerator _passwordGenerator;
        private ICredentialVerifier _credentialVerifier;
        private const string TestUserName = "TestUserName";

        [SetUp]
        public void SetUp()
        {
            _timeProvider = new Mock<ITimeProvider>();
            _keyProvider = new AppConfigKeyProvider();
            _expiryProvider = new AppConfigExpiryProvider();
            _hashGenerator = new HmacSha1HashGenerator();
            _passwordGenerator = new TimeBasedPasswordGenerator(_keyProvider, _expiryProvider, _hashGenerator, _timeProvider.Object);
            _credentialVerifier = new CredentialVerifier(_passwordGenerator);
        }

        [Test]
        public void GivenAGeneratedPassword_WhenVerifiedWithinExpiryPeriod_ThenReturnsTrue()
        {
            var password = _passwordGenerator.Generate(TestUserName);
            var isVerified = _credentialVerifier.Verify(TestUserName, password);

            Assert.That(isVerified, Is.True);
        }

        [Test]
        public void GivenAGeneratedPassword_WhenVerifiedAfterExpiryPeriod_ThenReturnsFalse()
        {
            var currentTime = DateTime.UtcNow;
            _timeProvider.Setup(p => p.GetUtcNow()).Returns(currentTime);
            var password = _passwordGenerator.Generate(TestUserName);

            _timeProvider.Setup(p => p.GetUtcNow()).Returns(currentTime.AddSeconds(35));
            var isVerified = _credentialVerifier.Verify(TestUserName, password);

            Assert.That(isVerified, Is.False);
        }
    }
}
