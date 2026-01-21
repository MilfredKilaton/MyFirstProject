using System;
using System.Drawing;

namespace TaskMaster
{
    //// Made by: Milfred Kilaton
    class Program
    {
        static void Main(string[] args)
        {
            int taskCount;

            while (true)
            {
                Console.WriteLine("---Task Master---");
                Console.Write("How many tasks would you like to enter? ");
                if (!int.TryParse(Console.ReadLine(), out taskCount) || taskCount <= 0)
                {
                    Console.WriteLine("Please enter a valid number.\nPress any key to continue.");
                    Console.ReadKey();
                    Console.Clear();
                }
                else { break; }
            }

            UserTask[] taskList = new UserTask[taskCount];

            for (int i = 0; i < taskCount; i++)
            {
                Console.WriteLine($"\nRegistering Task #{i + 1}: ");

                taskList[i] = new UserTask();

                Console.Write("\nTask Name: ");
                taskList[i].TaskName = Console.ReadLine() ?? string.Empty;

                Console.Write("\nTask Description: ");
                taskList[i].TaskDescription = Console.ReadLine() ?? string.Empty;

                Console.Write("Task Category:\n(1 - Work, 2 - Home, 3 - Personal) - ");
                while (!int.TryParse(Console.ReadLine(), out int catInput) && Enum.IsDefined(typeof(Category), catInput))
                {
                    taskList[i].TaskCategory = (Category)catInput;
                }

                Console.Write("\nTask Deadline: ");
                taskList[i].TaskDeadline = Console.ReadLine() ?? string.Empty;

                Console.Write("\nPriority Level; 1 as the highest to 10 as the lowest priority.\n(1 - 10) - ");
                while (!int.TryParse(Console.ReadLine(), out taskList[i].Priority.TaskPriorityLevel) || taskList[i].Priority.TaskPriorityLevel < 1 || taskList[i].Priority.TaskPriorityLevel > 10)
                {
                    Console.Write("Please enter a number from 1 to 10.\n - ");
                }

                Console.Write("\nIs task completed? (Y/N) - ");
                while (true)
                {
                    char taskInput = char.ToLower(Console.ReadKey().KeyChar);
                    if (taskInput == 'y')
                    {
                        taskList[i].IsTaskCompleted = true;
                        break;
                    }
                    else if (taskInput == 'n')
                    {
                        taskList[i].IsTaskCompleted = false;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Is the task completed or not? Please choose between Y for Yes, N for No.\n(Y/N) - ");
                    }
                }
            }

            var highPriorityTasks = taskList.Where(t => t.Priority.TaskPriorityLevel <= 10).ToList();
            var workTasks = taskList.Where(t => t.TaskCategory == Category.Work).ToList();

            Console.WriteLine("\n\n------------- Current Tasks -------------");
            int count = 1;
            foreach (var task in highPriorityTasks)
            {
                string taskStatus = task.IsTaskCompleted switch
                {
                    true => "Task Finished", // 13
                    false => "Task Pending", // 12
                };

                Console.WriteLine($"\n-------------- Task #{count} -----------------");
                Console.WriteLine($" * Task Name: {task.TaskName}");
                Console.WriteLine($" * Task Description: {task.TaskDescription}");
                Console.WriteLine($" * Task Deadline: {task.TaskDeadline}");
                Console.WriteLine($" * Task Priority: {task.Priority.TaskPriorityLevel}");
                Console.WriteLine($"-------- STATUS : {taskStatus} --------");
                Console.ResetColor();
                count++;
            }

            Console.WriteLine("Filter by: (High Priority) (Lowest Priority) (Finished)");
        }
    }

    public enum Category
    {
        None = 0,
        Work = 1,
        Home = 2,
        Personal = 3
    }

    struct TaskPriority
    {
        public int TaskPriorityLevel;
    }

    class UserTask
    {
        public string TaskName { get; set; } = string.Empty;
        public string TaskDescription { get; set; } = string.Empty;
        public Category TaskCategory { get; set; } = Category.None;
        public string TaskDeadline { get; set; } = string.Empty;
        public bool IsTaskCompleted { get; set; } = false;
        public TaskPriority Priority;
    }

    public static class Functions
    {
        public static void gotoxy(int x, int y) // ---- Manipulating the cursor position
        {
            Console.SetCursorPosition(x, y);
        }

        public static void color(ConsoleColor textColor, ConsoleColor bgColor) // ---- Manipulating the foreground and background
        {
            Console.ForegroundColor = textColor;
            Console.BackgroundColor = bgColor;
        }
    }
}