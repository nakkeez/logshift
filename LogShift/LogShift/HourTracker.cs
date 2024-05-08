namespace LogShift
{
    internal class HourTracker
    {
        private readonly List<WorkEntry> _workEntries = [];

        public List<User> Users;
        public List<Project> Projects;

        public HourTracker()
        {
            Users = [];
            Projects = [];
        }

        public bool AddUser(User user)
        {
            if (!CheckIfUserExists(user.Username))
            {
                Users.Add(user);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfUserExists(string username)
        {
            foreach (User user in Users)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }
            return false;
        }

        public User GetUser(string username)
        {
            return Users.Find(x => username == x.Username);
        }

        public bool AddProject(Project project)
        {
            if (!CheckIfProjectExists(project.ProjectId))
            {
                Projects.Add(project);
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool CheckIfProjectExists(string projectId)
        {
            foreach (Project project in Projects)
            {
                if (project.ProjectId == projectId)
                {
                    return true;
                }
            }
            return false;
        }

        public Project GetProject(string projectId)
        {
            return Projects.Find(x => projectId == x.ProjectId);
        }

        public void AddWorkEntry(User user, DateTime date, Project project, double hoursWorked, string description)
        {
            _workEntries.Add(new WorkEntry(user, date, project, hoursWorked, description));
        }

        public double GetTotalHoursByEmployee(User user)
        {

            double totalHours = 0;
            foreach (WorkEntry entry in _workEntries)
            {
                if (entry.User == user)
                {
                    totalHours += entry.HoursWorked;
                }
            }
            return totalHours;
        }

        public double GetTotalHoursByProject(Project project)
        {
            double totalHours = 0;
            foreach (WorkEntry entry in _workEntries)
            {
                if (entry.Project == project)
                {
                    totalHours += entry.HoursWorked;
                }
            }
            return totalHours;
        }

        public double GetTotalHoursByWeek(DateTime startDate, DateTime endDate)
        {
            double totalHours = 0;
            foreach (WorkEntry entry in _workEntries)
            {
                if (entry.Date >= startDate && entry.Date <= endDate)
                {
                    totalHours += entry.HoursWorked;
                }
            }
            return totalHours;
        }
    }
}
