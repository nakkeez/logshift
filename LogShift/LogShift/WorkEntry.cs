namespace LogShift
{
    internal class WorkEntry(User user, DateTime date, Project project, double hoursWorked, string description)
    {
        public User User { get; set; } = user;
        public DateTime Date { get; set; } = date;
        public Project Project { get; set; } = project;
        public double HoursWorked { get; set; } = hoursWorked;
        public string Description { get; set; } = description;
    }
}
