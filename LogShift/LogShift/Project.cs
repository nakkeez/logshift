namespace LogShift
{
    internal class Project(string projectId, string name)
    {
        public string ProjectId { get; set; } = projectId;
        public string Name { get; set; } = name;
    }
}
