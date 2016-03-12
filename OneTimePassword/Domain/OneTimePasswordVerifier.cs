using System;
using OneTimePassword.App.Contracts;

namespace OneTimePassword.App.Domain
{
    public class OneTimePasswordVerifier : IPasswordVerifier
    {
        private readonly IPasswordGenerator _passwordGenerator;

        public OneTimePasswordVerifier(IPasswordGenerator passwordGenerator)
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