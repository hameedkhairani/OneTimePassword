using System;
using Moq;
using NUnit.Framework;
using OneTimePassword.Contracts;
using OneTimePassword.Domain;

namespace OneTimePassword.Tests.Unit
{
    public class PasswordGeneratorTests
    {
        private IPasswordGenerator _passwordGenerator;
        private Mock<IKeyProvider> _mockKeyProvider;
        private Mock<IExpiryProvider> _mockExpiryProvider;
        private Mock<IHashGenerator> _mockHashGenerator;
        private const string TestUserName = "testUser";
        private const string ExpectedException = "some_exception";
        private const string ExpectedPassword = "some_valid_password";
        private const string ExpectedKey = "some_valid_key";

        [SetUp]
        public void SetUp()
        {
            _mockKeyProvider = new Mock<IKeyProvider>();
            _mockExpiryProvider = new Mock<IExpiryProvider>();
            _mockHashGenerator = new Mock<IHashGenerator>();
            _passwordGenerator = new TimeBasedPasswordGenerator(_mockKeyProvider.Object, _mockExpiryProvider.Object, _mockHashGenerator.Object);
        }


        [Test]
        public void GivenValidKeyAndValidHash_WhenPasswordIsGenerated_ThenReturnsExpectedPassword()
        {
            _mockKeyProvider.Setup(p => p.GetHashKey()).Returns(ExpectedKey);
            _mockHashGenerator.Setup(p => p.Generate(It.IsAny<string>(), It.IsAny<string>())).Returns(ExpectedPassword);

            var actualPassword = _passwordGenerator.Generate(TestUserName);
            
            _mockKeyProvider.Verify(p => p.GetHashKey());
            _mockExpiryProvider.Verify(p => p.GetExpiryInSeconds());
            _mockHashGenerator.Verify(p => p.Generate(It.IsAny<string>(), It.IsAny<string>()));
            Assert.That(actualPassword, Is.EqualTo(ExpectedPassword));
        }

        [Test]
        public void GivenKeyProviderThrowsException_WhenPasswordIsGenerated_ThenBubblesUpOriginalException()
        {
            _mockKeyProvider.Setup(p => p.GetHashKey()).Throws(new Exception(ExpectedException));
            _mockHashGenerator.Setup(p => p.Generate(It.IsAny<string>(), It.IsAny<string>())).Returns(ExpectedPassword);

            var actualException = Assert.Throws<Exception>(() => _passwordGenerator.Generate(TestUserName));
            Assert.That(actualException.Message, Is.EqualTo(ExpectedException));
        }

        [Test]
        public void GivenhashGeneratorThrowsException_WhenPasswordIsGenerated_ThenBubblesUpOriginalException()
        {
            _mockKeyProvider.Setup(p => p.GetHashKey()).Returns(ExpectedKey);
            _mockHashGenerator.Setup(p => p.Generate(It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception(ExpectedException));

            var actualException = Assert.Throws<Exception>(() => _passwordGenerator.Generate(TestUserName));
            Assert.That(actualException.Message, Is.EqualTo(ExpectedException));
        }

    }
}
