using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogShift
{
    internal class User(string username)
    {
        public int Id { get; set; }
        public string Username { get; set; } = username;
    }
}
