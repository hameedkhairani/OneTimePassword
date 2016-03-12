namespace OneTimePassword.App.Contracts
{
    public interface IKeyProvider
    {
        string GetHashKey();
    }
}