namespace LogShift
{
    internal class LogShift
    {
        static void Print(string message)
        {
            Console.Write(message);
        }

        static void PrintLine(string message)
        {
            Console.WriteLine(message);
        }

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

        static bool CreateNewWorkEntry(HourTracker tracker)
        {
            string username = AskInput("Enter username: ");
            if (!tracker.CheckIfUserExists(username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("User not found");
                return false;
            }

            User selectedUser = tracker.GetUser(username);

            string id = AskInput("Enter project id: ");
            if (!tracker.CheckIfProjectExists(id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("Project not found");
                return false;
            }

            Project selectedProject = tracker.GetProject(id);

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

        static void CreateNewUser(HourTracker tracker)
        {
            string username = AskInput("Give username: ");

            User user = new User(username);
            if (tracker.AddUser(user))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintLine($"User {user.Username} created");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine($"Failed to create user {username}");
            }
        }

        static bool CreateNewProject(HourTracker tracker)
        {
            string projectId = AskInput("Give project id: ");

            string projectName = AskInput("Give project name: ");

            Project project = new Project(projectId, projectName);
            if (tracker.AddProject(project))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                PrintLine($"Project {project.Name} created");
                return true;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine($"Failed to create project {projectName} with id {projectId}");
                return false;
            }
        }

        static void ShowWorkingHoursByUser(HourTracker tracker)
        {
            string username = AskInput("Enter username: ");

            if (!tracker.CheckIfUserExists(username))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("User not found");
            }
            else
            {
                User selectedUser = tracker.GetUser(username);
                Console.ForegroundColor = ConsoleColor.Green;
                PrintLine($"Total hours worked by {username}: {tracker.GetTotalHoursByEmployee(selectedUser)}");
            }
        }

        static void ShowWorkingHoursByProject(HourTracker tracker)
        {
            string id = AskInput("Enter project id: ");

            if (!tracker.CheckIfProjectExists(id))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine("User not found");
            }
            else
            {
                Project selectedProject = tracker.GetProject(id);
                Console.ForegroundColor = ConsoleColor.Green;
                PrintLine($"Total hours worked on {selectedProject.Name}: {tracker.GetTotalHoursByProject(selectedProject)}");
            }
        }

        static void ShowWorkingHoursByWeek(HourTracker tracker)
        {
            DateTime startDate = DateTime.Now.AddDays(-7);
            DateTime endDate = DateTime.Now;

            PrintLine($"Total hours worked from {startDate.ToShortDateString()} to {endDate.ToShortDateString()}: {tracker.GetTotalHoursByWeek(startDate, endDate)}");
        }

        static void Main()
        {
            HourTracker tracker = new HourTracker();

            Console.ForegroundColor = ConsoleColor.Yellow;
            PrintLine(banner);
            Console.ForegroundColor = ConsoleColor.Green;
            PrintLine(options);

            bool done = false;

            while (!done)
            {

                Console.ForegroundColor = ConsoleColor.White;
                Print("Input options ([7] help): ");
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        if (tracker.Users.Count <= 0 && tracker.Projects.Count <= 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            PrintLine("Users or projects not found");
                            break;
                        }

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
                        Console.ForegroundColor = ConsoleColor.Green;
                        ShowWorkingHoursByUser(tracker);
                        break;
                    case "5":
                        ShowWorkingHoursByProject(tracker);
                        break;
                    case "6":
                        ShowWorkingHoursByWeek(tracker);
                        break;
                    case "7":
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
[7] Help    
[0] Quit                              
----------------------------------------
";
    }
}