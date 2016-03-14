using System;
using OneTimePassword.Contracts;

namespace OneTimePassword.Domain
{
    public class TimeProvider : ITimeProvider
    {
        public DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}