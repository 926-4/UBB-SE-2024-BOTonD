using Moderation.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moderation.Entities
{
    public class User : IHasID
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public User(string username)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = GetRandomlyGeneratedPassword();
        }
        public User(string username, string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
        }
        private string GetRandomlyGeneratedPassword()
        {
            Random random = new Random();

            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";

            int length = random.Next(8, 24);

            char[] chars = new char[length];
            
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }

            return new string(chars);
        }
    }
}