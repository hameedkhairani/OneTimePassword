using System;
using System.Security.Cryptography;
using System.Text;
using OneTimePassword.App.Contracts;

namespace OneTimePassword.App.Domain
{
    public class HmacSha1HashGenerator : IHashGenerator
    {
        public string Generate(string key, string token)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new Exception("Invalid Key");

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("Invalid Token");

            var encoding = new ASCIIEncoding();
            var keyBytes = encoding.GetBytes(key);
            var tokenBytes = encoding.GetBytes(token);
            var hmac = new HMACSHA1(keyBytes);
            var passwordBytes = hmac.ComputeHash(tokenBytes);
            return ByteToString(passwordBytes);
        }

        private static string ByteToString(byte[] data)
        {
            //Assumption: Some implementations use 6 digit passwords, however as there is no requirement for this at this point, 
            //            I am assuming its ok to return a full hash representation as password
            //            Besides longer password would be more secure. Also, most password managers nowadays allow you to copy 
            //            password on clipboard anyway rather thaan forcing you to type.
            var hash = "";
            for (var i = 0; i < data.Length; i++)
            {
                hash += data[i].ToString("X2"); 
            }
            return hash;
        }
    }
}