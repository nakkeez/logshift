using System.ComponentModel.DataAnnotations;

namespace LogShift
{
    /// <summary>
    /// Represents a single user record.
    /// This class holds information about user's username.
    /// </summary>
    internal class User(string username)
    {
        public int Id { get; init; }
        
        [MaxLength(50)]
        public string Username { get; set; } = username;
    }
}
