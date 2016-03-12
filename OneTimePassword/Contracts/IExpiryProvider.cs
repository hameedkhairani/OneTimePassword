namespace OneTimePassword.Contracts
{
    public interface IExpiryProvider
    {
        int GetExpiryInSeconds();
    }
}
