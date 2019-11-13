using System;

namespace TourMarket.Helpers
{
    public static class SecurityHelper
    {
        private static readonly byte[] salt = new byte[] { 0x82, 0xd8, 0xa0, 0xb8, 0x29, 0x58, 0xa9, 0x49, 0x01, 0xdf, 0x4c, 0x5d, 0xbf, 0x32, 0xe6, 0xbb, 
                                                 0xcc, 0xea, 0x6b, 0x38, 0xf7, 0xca, 0xf9, 0xfb, 0x86, 0x56, 0x0e, 0x24, 0x38, 0x7b, 0x7d, 0xcd,
                                                 0x4a, 0x05, 0x73, 0x36, 0xc1, 0xab, 0x1c, 0xbc, 0x0b, 0x88, 0x6d, 0xec, 0xb6, 0xbc, 0xdb, 0xfc, 
                                                 0xba, 0x0e, 0xa3, 0x56, 0xeb, 0x45, 0xda, 0xa8, 0x9c, 0xdb, 0xb5, 0x83, 0xc8, 0xb3, 0x60, 0xd1 };
        
        public static bool VerifyPasswordHash(string password, byte[] storedHash)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            

            using (var hmac = new System.Security.Cryptography.HMACSHA256(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) 
                        return false;
                }
            }

            return true;
        }

        public static void CreatePasswordHash(string password, out byte[] passwordHash)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {                
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
