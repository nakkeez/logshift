using Microsoft.EntityFrameworkCore;

namespace LogShift
{
    internal class HourTracker
    {
        private readonly LogShiftContext _db;

        public HourTracker()
        {
            _db = new LogShiftContext();
        }

        public bool AddUser(string username)
        {
            try
            {
                _db.Add(new User(username));
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public User? GetUser(string username)
        {
            User? user = _db.Users
                    .Where(u => u.Username == username)
                    .FirstOrDefault();
            return user;
        }

        public User[] GetUsers()
        {
            return [.. _db.Users];
        }

        public bool AddProject(string projectId, string projectName)
        {
            try
            {
                _db.Add(new Project(projectId, projectName));
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Project? GetProject(string projectId)
        {
            Project? project = _db.Projects
                    .Where(p => p.ProjectId == projectId)
                    .FirstOrDefault();
            return project;
        }

        public Project[] GetProjects()
        {
            return [.. _db.Projects];
        }

        public bool AddWorkEntry(User user, DateTime date, Project project, double hoursWorked, string description)
        {
            try
            {
                _db.Add(new WorkEntry() { User=user, Date=date, Project=project, HoursWorked=hoursWorked, Description=description});
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public double GetTotalHoursByUser(User user)
        {
            WorkEntry[]? workEntries = _db.WorkEntries
                .Where(e => e.User == user)
                .ToArray();

            double totalHours = 0;
            if (workEntries != null)
            {
                foreach (WorkEntry entry in workEntries)
                {
                    totalHours += entry.HoursWorked;
                }
            }
            return totalHours;
        }

        public double GetTotalHoursByProject(Project project)
        {
            WorkEntry[]? workEntries = _db.WorkEntries
                .Where(e => e.Project == project)
                .ToArray();

            double totalHours = 0;
            if (workEntries != null)
            {
                foreach (WorkEntry entry in workEntries)
                {
                    totalHours += entry.HoursWorked;
                }
            }
            return totalHours;
        }

        public double GetTotalHoursByWeek(DateTime startDate, DateTime endDate)
        {
            WorkEntry[]? workEntries = _db.WorkEntries
                .Where(e => e.Date >= startDate && e.Date <= endDate)
                .ToArray();

            double totalHours = 0;
            if (workEntries != null)
            {
                foreach (WorkEntry entry in workEntries)
                {
                    totalHours += entry.HoursWorked;
                }
            }
            return totalHours;
        }

        public WorkEntry[] GetWorkEntriesByProject(string projectId)
        {
            var project = GetProject(projectId);
            if (project == null)
            {
                return Array.Empty<WorkEntry>();
            }

            return _db.WorkEntries
                .Include(e => e.User)
                .Where(e => e.Project == project)
                .ToArray();
        }
    }
}
