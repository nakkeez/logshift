using System.ComponentModel.DataAnnotations;

namespace LogShift
{
    /// <summary>
    /// Represents a single project record.
    /// This class holds information about project's name.
    /// </summary>
    internal class Project(string id, string name)
    {
        [MaxLength(20)]
        public string Id { get; init; } = id;
        
        [MaxLength(50)]
        public string Name { get; set; } = name;
    }
}
