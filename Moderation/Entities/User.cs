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

        public User(string username,string password)
        {
            Id = Guid.NewGuid();
            Username = username;
            Password = password;
        }
        public User(Guid id, string username, string password)
        {
            Id = id;
            Username = username;
            Password = password;
        }
    }
}
