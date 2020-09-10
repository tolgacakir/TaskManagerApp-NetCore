using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace TaskManagerApp.Core.Utilities.Security
{
    public class HashingHelper
    {
        public static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            switch (password)
            {
                case "":
                    throw new ArgumentException("Invalid parameter.", "password");
                
                case null:
                    throw new ArgumentNullException("password");
                
                default:
                    using (var hmac = new HMACSHA512())
                    {
                        passwordSalt = hmac.Key;
                        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    }
                    break;
            }
        }


        public static bool VerifyPasswordHash(string password, in byte[] passwordHash, in byte[] passwordSalt)
        {
            bool result = true;
                
            if (CheckPassword(password) && CheckPasswordHash(passwordHash) && CheckPasswordSalt(passwordSalt))
            {
                using (var hmac = new HMACSHA512(passwordSalt))
                {
                    var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                    for (int i = 0; i < computedHash.Length; i++)
                    {
                        if (computedHash[i] != passwordHash[i])
                        {
                            result = false;
                        }
                    }
                }
            }
            else
            {
                result = false;
            }

            return result;
        }

        private static bool CheckPassword(string password)
        {
            return !string.IsNullOrEmpty(password);
        }

        private static bool CheckPasswordHash(in byte[] passwordHash)
        {
            return (passwordHash != null && passwordHash.Length != 0);
        }

        private static bool CheckPasswordSalt(in byte[] passwordSalt)
        {
            return (passwordSalt != null && passwordSalt.Length != 0);
        }
    }
}
