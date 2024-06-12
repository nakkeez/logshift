namespace LogShift
{
    /// <summary>
    /// Represents a single work entry record.
    /// This class holds information about a work session, including the user,
    /// project, date, hours worked, and a description of the work done.
    /// </summary>
    internal class WorkEntry
    {
        /// <summary>
        /// Gets or sets the unique identifier for the work entry.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user who performed the work.
        /// </summary>
        public required User User { get; set; }

        /// <summary>
        /// Gets or sets the date when the work was performed.
        /// </summary>
        public required DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the project for which the work was performed.
        /// </summary>
        public required Project Project { get; set; }

        /// <summary>
        /// Gets or sets the number of hours worked.
        /// </summary>
        public required double HoursWorked { get; set; }

        /// <summary>
        /// Gets or sets the description of the work performed.
        /// </summary>
        public required string Description { get; set; }
    }
}
