using System;
using System.Threading;
using OneTimePassword.Contracts;
using OneTimePassword.Domain;

namespace OneTimePassword.App
{
    public class Program
    {
        private static ITimeProvider _timeProvider;
        private static IKeyProvider _keyProvider;
        private static IExpiryProvider _expiryProvider;

        private static IHashGenerator _hashGenerator;
        private static IPasswordGenerator _passwordGenerator;
        private static ICredentialVerifier _credentialVerifier;
        private static string _userName;

        static void Main(string[] args)
        {
            ReadInput(args);
            BootstrapDependencies();
            RunApp();
        }

        private static void RunApp()
        {
            try
            {
                Console.WriteLine("generating one-time password...");
                var password = _passwordGenerator.Generate(_userName);
                Console.WriteLine("UserName:{0}\nGenerated password:{1}", _userName, password);

                Console.WriteLine("verifying credentials...");
                var isVerified = _credentialVerifier.Verify(_userName, password);
                Console.WriteLine("Password verified:{0}", isVerified);

                Console.WriteLine("waiting till passowrd expires...");
                Thread.Sleep(1000*35);

                Console.WriteLine("verifying same credentials again...");
                isVerified = _credentialVerifier.Verify(_userName, password);
                Console.WriteLine("Password verified:{0}", isVerified);

                Console.WriteLine("press a key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine("message:{0}\nStackTrack:{1}", ex.Message, ex.StackTrace);
            }
        }

        private static void ReadInput(string[] args)
        {
            _userName = args[0];
        }

        private static void BootstrapDependencies()
        {
            _timeProvider = new TimeProvider();
            _keyProvider = new AppConfigKeyProvider();
            _expiryProvider = new AppConfigExpiryProvider();
            _hashGenerator = new HmacSha1HashGenerator();
            _passwordGenerator = new TimeBasedPasswordGenerator(_keyProvider, _expiryProvider, _hashGenerator, _timeProvider);
            _credentialVerifier = new CredentialVerifier(_passwordGenerator);
        }
    }
}
