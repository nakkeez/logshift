namespace LogShift
{
    /// <summary>
    /// Represents a single project record.
    /// This class holds information about project's name.
    /// </summary>
    internal class Project(string projectId, string name)
    {
        /// <summary>
        /// Gets or sets the unique identifier for the project.
        /// </summary>
        public string ProjectId { get; set; } = projectId;

        /// <summary>
        /// Gets or sets the name for the project.
        /// </summary>
        public string Name { get; set; } = name;
    }
}
