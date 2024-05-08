namespace LogShift
{
    internal class WorkEntry
    {
        public int Id { get; set; }
        public required User User { get; set; }
        public required DateTime Date { get; set; }
        public required Project Project { get; set; }
        public required double HoursWorked { get; set; }
        public required string Description { get; set; }
    }
}
