using System;

namespace TaskMaster
{
    //// Made by: Milfred Kilaton
    struct TaskPriority
    {
        public int TaskPriorityLevel;
    }
    class UserTask
    {
        public string taskName;
        public TaskPriority Priority;
    }

    class Program
    {
        static void Main(string[] args)
        {
            TaskPriority taskPriority;
            taskPriority.TaskPriorityLevel = 10;

            UserTask userTask = new UserTask();
            userTask.taskName = "Second Semester Task 1";
            userTask.Priority = taskPriority;

            Console.WriteLine($"You have now completed the: {userTask.taskName}");
        }
    }
}