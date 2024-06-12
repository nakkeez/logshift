namespace LogShift
{
    /// <summary>
    /// The main class for the LogShift application, providing a console interface
    /// for tracking work hours.
    /// </summary>
    internal class LogShift
    {
        /// <summary>
        /// Prints a message to the console without a newline.
        /// </summary>
        /// <param name="message">The message to print.</param>
        static void Print(string message)
        {
            Console.Write(message);
        }

        /// <summary>
        /// Prints a message to the console followed by a newline.
        /// </summary>
        /// <param name="message">The message to print.</param>
        static void PrintLine(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Prompts the user for input with a message and returns the input.
        /// </summary>
        /// <param name="message">The message to display as a prompt.</param>
        /// <returns>The user's input as a string.</returns>
        static string AskInput(string message)
        {
            Print(message);
            string? input = Console.ReadLine();
            while (input == null)
            {
                Print(message);
                input = Console.ReadLine();
            }
            return input;
        }

        /// <summary>
        /// Guides the user through creating a new work entry.
        /// Asks user to input username, project id, date, hours worked, and description
        /// for the new work entry.
        /// </summary>
        /// <param name="tracker">The HourTracker instance to use for data operations.</param>
        /// <returns>True if the work entry was successfully created, false otherwise.</returns>
        static bool CreateNewWorkEntry(HourTracker tracker)
        {
            string username = AskInput("Enter username: ");
            User? selectedUser = tracker.GetUser(username);
            if (selectedUser == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("User not found");
                return false;
            }

            string id = AskInput("Enter project id: ");
            Project? selectedProject = tracker.GetProject(id);
            if (selectedProject == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("Project not found");
                return false;
            }

            Print("Enter date (YYYY-MM-DD): ");
            if (!DateTime.TryParse(Console.ReadLine(), out DateTime date))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("Invalid date format");
                return false;
            }

            Print("Enter hours worked: ");
            if (!double.TryParse(Console.ReadLine(), out double hoursWorked) || hoursWorked < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("Invalid hours worked");
                return false;
            }

            string description = AskInput("Enter description of the work done: ");

            tracker.AddWorkEntry(selectedUser, date, selectedProject, hoursWorked, description);
            return true;
        }

        /// <summary>
        /// Guides the user through creating a new user.
        /// Asks user to input username for the new user.
        /// </summary>
        /// <param name="tracker">The HourTracker instance to use for data operations.</param>
        static void CreateNewUser(HourTracker tracker)
        {
            string username = AskInput("Give username: ");

            if (tracker.AddUser(username))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintLine($"User {username} created");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine($"Failed to create user {username}");
            }
        }

        /// <summary>
        /// Guides the user through creating a new project.
        /// Asks user to input project id and project name for the new project.
        /// </summary>
        /// <param name="tracker">The HourTracker instance to use for data operations.</param>
        /// <returns>True if the project was successfully created, false otherwise.</returns>
        static bool CreateNewProject(HourTracker tracker)
        {
            string projectId = AskInput("Give project id: ");

            string projectName = AskInput("Give project name: ");

            if (tracker.AddProject(projectId, projectName))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintLine($"Project {projectName} created");
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine($"Failed to create project {projectName} with id {projectId}");
                return false;
            }
        }

        /// <summary>
        /// Displays the total working hours by a specific user.
        /// Asks user to input username for the user to display hours for.
        /// </summary>
        /// <param name="tracker">The HourTracker instance to use for data operations.</param>
        static void ShowWorkingHoursByUser(HourTracker tracker)
        {
            string username = AskInput("Enter username: ");
            User? selectedUser = tracker.GetUser(username);

            if (selectedUser == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("User not found");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            PrintLine($"Total hours worked by {username}: {tracker.GetTotalHoursByUser(selectedUser)}");
        }

        /// <summary>
        /// Displays the total working hours for a specific project.
        /// Asks user to input project id for the project to display hours for.
        /// </summary>
        /// <param name="tracker">The HourTracker instance to use for data operations.</param>
        static void ShowWorkingHoursByProject(HourTracker tracker)
        {
            string id = AskInput("Enter project id: ");
            Project? selectedProject = tracker.GetProject(id);

            if (selectedProject == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("Project not found");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            PrintLine($"Total hours worked on {selectedProject.Name}: {tracker.GetTotalHoursByProject(selectedProject)}");
        }

        /// <summary>
        /// Displays the total working hours for the current week.
        /// </summary>
        /// <param name="tracker">The HourTracker instance to use for data operations.</param>
        static void ShowWorkingHoursByWeek(HourTracker tracker)
        {
            DateTime startDate = DateTime.Now.AddDays(-7);
            DateTime endDate = DateTime.Now;

            Console.ForegroundColor = ConsoleColor.Green;
            PrintLine($"Total hours worked from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}: {tracker.GetTotalHoursByWeek(startDate, endDate)}");
        }

        /// <summary>
        /// Displays a list of all users and projects.
        /// </summary>
        /// <param name="tracker">The HourTracker instance to use for data operations.</param>
        static void ShowUsersAndProjects(HourTracker tracker)
        {
            User[] users = tracker.GetUsers();
            Project[] projects = tracker.GetProjects();

            Console.ForegroundColor = ConsoleColor.Green;

            string listedUsers = "";
            foreach (var user in users)
            {
                listedUsers += $"{user.Username}, ";
            }
            listedUsers = listedUsers.TrimEnd(',', ' ');

            string listedProjects = "";
            foreach (var project in projects)
            {
                listedProjects += $"{project.ProjectId} {project.Name}\n";
            }
            listedProjects = listedProjects.TrimEnd('\n');

            PrintLine($"Users:");
            Print(listedUsers);

            PrintLine("\nProjects:");
            Print(listedProjects);
            PrintLine("");
        }

        /// <summary>
        /// Displays all work entries for a specific project.
        /// Asks user to input project id for the project to display work entries for.
        /// </summary>
        /// <param name="tracker">The HourTracker instance to use for data operations.</param>
        static void ShowWorkEntriesByProject(HourTracker tracker)
        {
            string id = AskInput("Enter project id: ");
            Project? selectedProject = tracker.GetProject(id);

            if (selectedProject == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("Project not found");
                return;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            WorkEntry[] entries = tracker.GetWorkEntriesByProject(id);
            PrintLine($"Work entries for project {selectedProject.ProjectId} {selectedProject.Name}:");
            foreach (var entry in entries)
            {
                PrintLine($"User: {entry.User.Username}, Date: {entry.Date.ToShortDateString()}, Hours: {entry.HoursWorked}, Description: {entry.Description}");
            }
        }

        /// <summary>
        /// The main entry point for the application, providing a command-line interface
        /// for interacting with the HourTracker.
        /// Runs in infinite loop until user chooses to quit the application.
        /// </summary>
        static void Main()
        {
            HourTracker tracker = new HourTracker();

            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintLine(banner);
            Console.ForegroundColor = ConsoleColor.Green;
            PrintLine(options);

            while (true)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Print("Input options ([9] help): ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        bool success = CreateNewWorkEntry(tracker);
                        if (success)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            PrintLine("New entry succesfully add");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            PrintLine("Failed to add new entry");
                        }
                        break;
                    case "2":
                        CreateNewProject(tracker);
                        break;
                    case "3":
                        CreateNewUser(tracker);
                        break;
                    case "4":

                        ShowWorkingHoursByUser(tracker);
                        break;
                    case "5":
                        ShowWorkingHoursByProject(tracker);
                        break;
                    case "6":
                        ShowWorkingHoursByWeek(tracker);
                        break;
                    case "7":
                        ShowUsersAndProjects(tracker);
                        break;
                    case "8":
                        ShowWorkEntriesByProject(tracker);
                        break;
                    case "9":               
                        Console.ForegroundColor = ConsoleColor.Green;
                        PrintLine(options);
                        break;
                    case "0":
                        Console.ForegroundColor = ConsoleColor.Green;
                        PrintLine("Goodbye!");
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        PrintLine("Invalid input");
                        break;
                }
            }
        }

        static string banner = @"
 ___       ________  ________  ________  ___  ___  ___  ________ _________   
|\  \     |\   __  \|\   ____\|\   ____\|\  \|\  \|\  \|\  _____\\___   ___\ 
\ \  \    \ \  \|\  \ \  \___|\ \  \___|\ \  \\\  \ \  \ \  \__/\|___ \  \_| 
 \ \  \    \ \  \\\  \ \  \  __\ \_____  \ \   __  \ \  \ \   __\    \ \  \  
  \ \  \____\ \  \\\  \ \  \|\  \|____|\  \ \  \ \  \ \  \ \  \_|     \ \  \ 
   \ \_______\ \_______\ \_______\____\_\  \ \__\ \__\ \__\ \__\       \ \__\
    \|_______|\|_______|\|_______|\_________\|__|\|__|\|__|\|__|        \|__|
                                 \|_________|                                
                                                                             
                                                                             
";

        static string options = @"
----------------------------------------
[1] Add new work entry  
[2] Create new project      
[3] Create new user
[4] Show working hours by user     
[5] Show working hours by project      
[6] Show working hours this week    
[7] Show all users and projects 
[8] Show work entries by project  
[9] Help    
[0] Quit                              
----------------------------------------
";
    }
}