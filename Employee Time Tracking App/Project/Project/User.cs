using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    public abstract class User
    {
        // Represents a user in the system with a username and password.
        public string UserName { get; set; }
        public string Password { get; set; }

        
        public User(string username, string password) // Constructor to initialize a User object with a username and password.
        {
            UserName = username;
            Password = password;
        }
    }
}
