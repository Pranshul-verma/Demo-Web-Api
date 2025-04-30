using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1.Model
{
    public class Users
    {
        private string id;

        public string Id { get => id; set => id = value; }

        private string password;

        public string Password { get => password; set => password = value; }

        private string token;

        public string Token { get => token; set => token = value; }

    }
}
