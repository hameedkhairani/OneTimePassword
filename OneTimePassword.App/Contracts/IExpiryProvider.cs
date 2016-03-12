namespace OneTimePassword.App.Contracts
{
    public interface IExpiryProvider
    {
        int GetExpiryInSeconds();
    }
}
