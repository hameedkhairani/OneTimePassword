using System;
using System.Configuration;
using OneTimePassword.App.Contracts;

namespace OneTimePassword.App.Domain
{
    public class AppConfigKeyProvider : IKeyProvider
    {
        public string GetHashKey()
        {
            if (string.IsNullOrWhiteSpace(ConfigurationManager.AppSettings["HashKey"]))
            {
                throw new Exception("HashKey not found");
            }
            return ConfigurationManager.AppSettings["HashKey"];
        }
    }
}