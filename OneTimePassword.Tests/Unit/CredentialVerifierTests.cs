using Moq;
using NUnit.Framework;
using OneTimePassword.Contracts;
using OneTimePassword.Domain;

namespace OneTimePassword.Tests.Unit
{
    public class CredentialVerifierTests
    {
        private Mock<IPasswordGenerator> _mockPasswordGenerator;
        private ICredentialVerifier _credentialVerifier;
        private const string ExpectedPassword = "some_valid_password";
        private const string TestUserName = "testUser";

        [SetUp]
        public void SetUp()
        {
            _mockPasswordGenerator = new Mock<IPasswordGenerator>();
            _credentialVerifier = new CredentialVerifier(_mockPasswordGenerator.Object);
            _mockPasswordGenerator.Setup(p => p.Generate(TestUserName)).Returns(ExpectedPassword);
        }


        [Test]
        public void GivenCorrectPassword_WhenCredentialsAreVerified_ThenReturnsTrue()
        {
            var verified = _credentialVerifier.Verify(TestUserName, ExpectedPassword);
            _mockPasswordGenerator.Verify(p => p.Generate(TestUserName));
            Assert.That(verified, Is.True);
        }

        [Test]
        public void GivenIncorrectPassword_WhenCredentialsAreVerified_ThenReturnsFalse()
        {
            var verified = _credentialVerifier.Verify(TestUserName, "some_invalid_password");
            _mockPasswordGenerator.Verify(p => p.Generate(TestUserName));
            Assert.That(verified, Is.False);
        }

    }
}
