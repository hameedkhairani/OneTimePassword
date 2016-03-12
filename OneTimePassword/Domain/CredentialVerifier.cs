using System;
using OneTimePassword.Contracts;

namespace OneTimePassword.Domain
{
    public class CredentialVerifier : ICredentialVerifier
    {
        private readonly IPasswordGenerator _passwordGenerator;

        public CredentialVerifier(IPasswordGenerator passwordGenerator)
        {
            _passwordGenerator = passwordGenerator;
        }

        public bool Verify(string userName, string password)
        {
            var generatedPassword = _passwordGenerator.Generate(userName);
            return generatedPassword.Equals(password, StringComparison.CurrentCultureIgnoreCase);
        }
    }
}