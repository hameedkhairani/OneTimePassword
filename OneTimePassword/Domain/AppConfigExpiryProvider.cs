using System;
using System.Configuration;
using OneTimePassword.App.Contracts;

namespace OneTimePassword.App.Domain
{
    public class AppConfigExpiryProvider : IExpiryProvider
    {
        public int GetExpiryInSeconds()
        {
            var expiry = ConfigurationManager.AppSettings["PasswordExpiryInSeconds"];
            return string.IsNullOrWhiteSpace(expiry) ? 30 : Convert.ToInt16(expiry);
        }
    }
}