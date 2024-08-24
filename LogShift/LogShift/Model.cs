using Microsoft.EntityFrameworkCore;

namespace LogShift
{
    /// <summary>
    /// Represents the database context for the LogShift application, 
    /// managing entities and their persistence to the database.
    /// </summary>
    internal class LogShiftContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for users in the application.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for projects in the application.
        /// </summary>
        public DbSet<Project> Projects { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for work entries in the application.
        /// </summary>
        public DbSet<WorkEntry> WorkEntries { get; set; }

        /// <summary>
        /// Gets the path to the database file.
        /// </summary>
        public string DbPath { get; }

        /// <summary>
        /// Initializes a new instance of the LogShiftContext class, setting up
        /// the database path for the SQLite database in the user's local application data folder.
        /// </summary>
        public LogShiftContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var appDataPath = Environment.GetFolderPath(folder);
            var path = Path.Combine(appDataPath, "LogShift");
            // Create the directory if it does not exist, do nothing if it does
            Directory.CreateDirectory(path);

            DbPath = Path.Combine(path, "logshift.db");
        }

        /// <summary>
        /// Configures the model relationships and properties using Fluent API configurations.
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();
            modelBuilder.Entity<Project>().HasIndex(p => p.Name).IsUnique();
        }

        /// <summary>
        /// Configures the database to be used for this context.
        /// This method is called for each instance of the context that is created.
        /// </summary>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
