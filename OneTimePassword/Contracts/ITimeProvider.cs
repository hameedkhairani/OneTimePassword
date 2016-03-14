using System;

namespace OneTimePassword.Contracts
{
    public interface ITimeProvider
    {
        DateTime GetUtcNow();
    }
}