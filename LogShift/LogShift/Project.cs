namespace LogShift
{
    internal class Project
    {
        public string ProjectId { get; set; }
        public string Name { get; set; }

        public Project(string projectId, string name)
        {
            ProjectId = projectId;
            Name = name;
        }
    }
}
