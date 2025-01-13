using CsvHelper;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace LogShift
{
    /// <summary>
    /// Represents a work hour tracking system.
    /// Manages user, project, and work entry data, providing functionality to
    /// add and retrieve information from the database.
    /// </summary>
    internal class HourTracker
    {
        private readonly LogShiftContext _db;

        /// <summary>
        /// Initializes a new instance of the HourTracker class, creating a connection to the database.
        /// </summary>
        public HourTracker()
        {
            _db = new LogShiftContext();
        }

        /// <summary>
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="username">The username of the user to add.</param>
        /// <returns>True if the user was added successfully, false otherwise.</returns>
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

        /// <summary>
        /// Retrieves a user from the database by username.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <returns>The user object if found, null otherwise.</returns>
        public User? GetUser(string username)
        {
            var user = _db.Users
                .FirstOrDefault(u => u.Username == username);
            return user;
        }

        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>An array of all users in the database.</returns>
        public User[] GetUsers()
        {
            return [.. _db.Users];
        }

        /// <summary>
        /// Adds a new project to the database.
        /// </summary>
        /// <param name="projectId">The unique identifier of the project.</param>
        /// <param name="projectName">The name of the project.</param>
        /// <returns>True if the project was added successfully, false otherwise.</returns>
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

        /// <summary>
        /// Retrieves a project from the database by its unique identifier.
        /// </summary>
        /// <param name="projectId">The unique identifier of the project to retrieve.</param>
        /// <returns>The project object if found, null otherwise.</returns>
        public Project? GetProject(string projectId)
        {
            var project = _db.Projects
                .FirstOrDefault(p => p.Id == projectId);
            return project;
        }

        /// <summary>
        /// Retrieves all projects from the database.
        /// </summary>
        /// <returns>An array of all projects in the database.</returns>
        public Project[] GetProjects()
        {
            return [.. _db.Projects];
        }

        /// <summary>
        /// Adds a new work entry to the database.
        /// </summary>
        /// <param name="user">The user who performed the work.</param>
        /// <param name="date">The date when the work was performed.</param>
        /// <param name="project">The project for which the work was performed.</param>
        /// <param name="hoursWorked">The number of hours worked.</param>
        /// <param name="description">A description of the work performed.</param>
        /// <returns>True if the work entry was added successfully, false otherwise.</returns>
        public bool AddWorkEntry(User user, DateTime date, Project project, double hoursWorked, string description)
        {
            try
            {
                _db.Add(new WorkEntry()
                {
                    User = user, Date = date, Project = project, HoursWorked = hoursWorked, Description = description
                });
                _db.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Calculates the total hours worked by a user.
        /// </summary>
        /// <param name="user">The user for whom to calculate total hours worked.</param>
        /// <returns>The total hours worked by the user.</returns>
        public double GetTotalHoursByUser(User user)
        {
            WorkEntry[] workEntries = [.. _db.WorkEntries.Where(e => e.User == user)];
            return workEntries.Sum(entry => entry.HoursWorked);
        }

        /// <summary>
        /// Calculates the total hours worked on a project.
        /// </summary>
        /// <param name="project">The project for which to calculate total hours worked.</param>
        /// <returns>The total hours worked on the project.</returns>
        public double GetTotalHoursByProject(Project project)
        {
            WorkEntry[] workEntries = [.. _db.WorkEntries.Where(e => e.Project == project)];
            return workEntries.Sum(entry => entry.HoursWorked);
        }

        /// <summary>
        /// Calculates the total hours worked within a specified date range.
        /// </summary>
        /// <param name="startDate">The start date of the range.</param>
        /// <param name="endDate">The end date of the range.</param>
        /// <returns>The total hours worked within the specified date range.</returns>
        public double GetTotalHoursByWeek(DateTime startDate, DateTime endDate)
        {
            WorkEntry[] workEntries = [.. _db.WorkEntries.Where(e => e.Date >= startDate && e.Date <= endDate)];
            return workEntries.Sum(entry => entry.HoursWorked);
        }

        /// <summary>
        /// Retrieves all work entries for a specific project.
        /// </summary>
        /// <param name="user">User whose work entries are to be retrieved.</param>
        /// <returns>An array of work entries for the specified user.</returns>
        public WorkEntry[] GetWorkEntriesByUser(User user)
        {
            return
            [
                .. _db.WorkEntries
                    .Include(e => e.Project)
                    .Where(e => e.User == user)
            ];
        }

        /// <summary>
        /// Retrieves all work entries for a specific project.
        /// </summary>
        /// <param name="project">Project that's work entries are to be retrieved.</param>
        /// <returns>An array of work entries for the specified project.</returns>
        public WorkEntry[] GetWorkEntriesByProject(Project project)
        {
            return
            [
                .. _db.WorkEntries
                    .Include(e => e.User)
                    .Where(e => e.Project == project)
            ];
        }

        /// <summary>
        /// Retrieves the work entries of a specific user and then saves them to a CSV file.
        /// </summary>
        /// <param name="user">User whose work entries are to be saved.</param>
        /// <returns>True if the work entries were saved successfully, false otherwise.</returns>
        public bool SaveWorkEntriesToCsv(User user)
        {
            WorkEntry[] workEntries = [.. _db.WorkEntries.Where(e => e.User == user)];

            try
            {
                var dir = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                const string filename = "logshift_work_entries.csv";
                var path = Path.Combine(dir, filename);

                using var writer = new StreamWriter(path);
                using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
                csv.WriteRecords(workEntries);
                return true;
            }
            catch (CsvHelperException)
            {
                return false;
            }
        }
    }
}