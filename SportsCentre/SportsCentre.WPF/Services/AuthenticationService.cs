using Microsoft.AspNetCore.Identity;
using SportsCentre.Data;
using SportsCentre.Domain.Interfaces;
using SportsCentre.Domain.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SportsCentre.WPF.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public User Login(string username, string password)
        {
            using (var context = new SportsCentreDbContext())
            {
                User user = context.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                {
                    return null;
                }

                bool verifyPassword = VerifyHashedPassword(user.PasswordHash, password);

                if (verifyPassword)
                {
                    return user;
                }

                return null;
            }
        }

        public bool Register(string name, string username, string password, string confirmPassword, string role)
        {
            if (userExists(username))
            {
                return false;
            }

            if (password != confirmPassword)
            {
                return false;
            }

            string passwordHash = HashPassword(password);

            User user = new User
            {
                Name = name,
                Username = username,
                PasswordHash = passwordHash,
                Role = role
            };

            using (var context = new SportsCentreDbContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

            return true;
        }

        private bool userExists(string username)
        {
            using (var context = new SportsCentreDbContext())
            {
                User user = context.Users.FirstOrDefault(x => x.Username == username);

                if (user == null)
                {
                    return false;
                }

                return true;
            }
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;

            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArrayCompare(buffer3, buffer4);
        }

        static bool ByteArrayCompare(byte[] a1, byte[] a2)
        {
            return StructuralComparisons.StructuralEqualityComparer.Equals(a1, a2);
        }
    }
}
