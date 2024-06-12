using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogShift
{
    /// <summary>
    /// Represents a single user record.
    /// This class holds information about user's username.
    /// </summary>
    internal class User(string username)
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the username for the user.
        /// </summary>
        public string Username { get; set; } = username;
    }
}
