using System;
using OneTimePassword.Contracts;

namespace OneTimePassword.Domain
{
    public class TimeBasedPasswordGenerator : IPasswordGenerator
    {
        private readonly IKeyProvider _keyProvider;
        private readonly IExpiryProvider _expiryProvider;
        private readonly IHashGenerator _hashGenerator;

        public TimeBasedPasswordGenerator(IKeyProvider keyProvider, IExpiryProvider expiryProvider, IHashGenerator hashGenerator)
        {
            _keyProvider = keyProvider;
            _expiryProvider = expiryProvider;
            _hashGenerator = hashGenerator;
        }

        public string Generate(string userName)
        {
            var key = _keyProvider.GetHashKey();
            var expiry = _expiryProvider.GetExpiryInSeconds();
            var token = BuildToken(userName, expiry);
            var password = _hashGenerator.Generate(key, token);

            return password;
        }

        private static string BuildToken(string username, int expiryInSeconds)
        {
            var steps = GetTimeSteps(expiryInSeconds);
            return string.Format("{0}_{1}", username.ToLower(), steps);
        }

        private static int GetTimeSteps(int expiryInSeconds)
        {
            var currentTime = DateTime.UtcNow;
            var unixEpochTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var span = currentTime - unixEpochTime;
            var steps = (int)(span.TotalSeconds / expiryInSeconds);
            return steps;
        }

    }
}