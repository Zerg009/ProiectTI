using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BCrypt.Net;
namespace WebApplication1
{


    public class PasswordHelper
    {
        // Method to hash a password
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        // Method to verify a password against a stored hash
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}