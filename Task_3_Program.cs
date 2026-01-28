using System;
using System.Drawing;
using System.Linq;

namespace Task_3;

//// Made by: Milfred Kilaton
class Program
{
    static void Main(string[] args)
    {
        int taskCount;
        char filter;
        List<UserTask> workTasks = [];
        List<UserTask> highPriorityTasks = [];
        char choice;

        Console.WriteLine("------------- Task Master -------------");
        Console.Write("How many tasks would you like to enter? ");
        while (!int.TryParse(Console.ReadLine(), out taskCount) || taskCount <= 0)
        {
            Console.WriteLine("Please enter a valid number.\n - ");
        }

        UserTask[] taskList = new UserTask[taskCount];

        for (int i = 0; i < taskCount; i++)
        {
            Console.Clear();
            Console.WriteLine($"Registering Task #{i + 1}");

            taskList[i] = new UserTask();

            Console.Write("Task Name: ");
            taskList[i].TaskName = Console.ReadLine() ?? string.Empty;

            Console.Write("Task Description: ");
            taskList[i].TaskDescription = Console.ReadLine() ?? string.Empty;

            Console.Write("Task Category:\n(1 - Work, 2 - Home, 3 - Personal) - ");
            int catInput;
            while (!int.TryParse(Console.ReadLine(), out catInput) || !Enum.IsDefined(typeof(Category), catInput))
            {
                Console.Write("Invalid selection. Please choose 1, 2, or 3: ");
            }
            taskList[i].TaskCategory = (Category)catInput;

            Console.Write("Task Deadline: ");
            taskList[i].TaskDeadline = Console.ReadLine() ?? string.Empty;

            Console.Write("\n\nPriority Level; 1 as the highest to 10 as the lowest priority.\n(1 - 10) - ");
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
        highPriorityTasks = taskList.Where(t => t.Priority.TaskPriorityLevel <= 3).ToList();
        workTasks = taskList.Where(t => t.TaskCategory == Category.Work).ToList();

        while (true)
        {
            Console.Clear();


            Console.Write("(W - Work category tasks only) (H - Tasks prioritized from 1 to 3 only)\nFilter by: ");
            while (!char.TryParse(Console.ReadLine(), out filter) && !(char.ToLower(filter) == 'w' || char.ToLower(filter) == 'h'))
            {
                Console.WriteLine("Please choose between W or H.\n - ");
            }

            Console.WriteLine("\n\n------------- Current Tasks -------------");
            if (char.ToLower(filter) == 'w')
            {
                foreach (var task in workTasks)
                {
                    string taskStatus = task.IsTaskCompleted switch
                    {
                        true => "Task Finished", // 13
                        false => "Task Pending ", // 12
                    };

                    Console.WriteLine($"\n------- Category : {task.TaskCategory} --------");
                    Console.WriteLine($" * Name: {task.TaskName}");
                    Console.WriteLine($" * Description: {task.TaskDescription}");
                    Console.WriteLine($" * Deadline: {task.TaskDeadline}");
                    Console.WriteLine($" * Priority: {task.Priority.TaskPriorityLevel}");
                    Console.WriteLine($"---- STATUS : {taskStatus} ----");
                }

            }
            if (char.ToLower(filter) == 'h')
            {
                foreach (var task in highPriorityTasks)
                {
                    string taskStatus = task.IsTaskCompleted switch
                    {
                        true => "Task Finished", // 13
                        false => "Task Pending ", // 12
                    };

                    Console.WriteLine($"\n----- Priority Level {task.Priority.TaskPriorityLevel} ------");
                    Console.WriteLine($" * Task Name: {task.TaskName}");
                    Console.WriteLine($" * Description: {task.TaskDescription}");
                    Console.WriteLine($" * Deadline: {task.TaskDeadline}");
                    Console.WriteLine($" * Category: {task.TaskCategory}");
                    Console.WriteLine($"--- STATUS : {taskStatus} ---");
                }

            }
            while (true)
            {
                Console.Write("\n\nTry again?\n(Y/N) - ");
                choice = char.ToLower(Console.ReadKey().KeyChar);
                if (choice == 'y' || choice == 'n') { break; }

                Console.Clear();
                Console.WriteLine("Invalid input.");
            }

            if (choice == 'n') { break; }
        }

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
    public static void GotoXY(int x, int y) // ---- Manipulating the cursor position
    {
        Console.SetCursorPosition(x, y);
    }

    public static void Color(ConsoleColor textColor, ConsoleColor bgColor) // ---- Manipulating the foreground and background
    {
        Console.ForegroundColor = textColor;
        Console.BackgroundColor = bgColor;
    }
}