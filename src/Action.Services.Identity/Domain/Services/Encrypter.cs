﻿using System;
using System.Security.Cryptography;

namespace Action.Services.Identity.Domain.Services
{
    public class Encrypter : IEncrypter
    {
        private static int SaltSize = 40;
        private static readonly int DeriveBytesInterationsCount = 10000;

        public string GetHash(string value, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesInterationsCount);
            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        public string GetSalt()
        {
            var random = new Random();
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
    }
}