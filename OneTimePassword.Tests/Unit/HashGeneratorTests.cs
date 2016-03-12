using System;
using NUnit.Framework;
using OneTimePassword.Contracts;
using OneTimePassword.Domain;

namespace OneTimePassword.Tests.Unit
{
    public class HashGeneratorTests
    {

        private IHashGenerator _hashGenerator;
        private const string ValidTestKey = "some_key";
        private const string ValidTestToken = "some_token";

        [SetUp]
        public void SetUp()
        {
            _hashGenerator = new HmacSha1HashGenerator();
        }

        [Test]
        public void GivenValidKeyAndToken_WhenHashIsGenerated_ThenReturnsValidHash()
        {
            var hash = _hashGenerator.Generate(ValidTestKey,ValidTestToken);
            Assert.That(hash, Is.Not.Null);
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void GivenInvalidKey_WhenHashIsGenerated_ThenThrowsException(string key)
        {
            var exception = Assert.Throws<Exception>(() => _hashGenerator.Generate(key, ValidTestToken));
            Assert.That(exception.Message, Is.EqualTo("Invalid Key"));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("  ")]
        public void GivenInvalidToken_WhenHashIsGenerated_ThenThrowsException(string token)
        {
            var exception = Assert.Throws<Exception>(() => _hashGenerator.Generate(ValidTestKey, token));
            Assert.That(exception.Message, Is.EqualTo("Invalid Token"));
        }

    }
}
