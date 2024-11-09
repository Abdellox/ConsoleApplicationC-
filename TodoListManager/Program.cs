using System;
using System.Collections.Generic;

        TodoList todoList = new TodoList();
        while (true)
        {
            Console.WriteLine("\nTodo List Manager");
            Console.WriteLine("1. Add Task");
            Console.WriteLine("2. View Tasks");
            Console.WriteLine("3. Complete Task");
            Console.WriteLine("4. Remove Task");
            Console.WriteLine("5. Exit");
            Console.Write("Choose an option: ");

            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("Enter task title: ");
                    string? title = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(title))
                    {
                        Console.WriteLine("Title cannot be empty.");
                        break;
                    }

                    Console.Write("Enter task description: ");
                    string? description = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(description))
                    {
                        Console.WriteLine("Description cannot be empty.");
                        break;
                    }

                    Console.Write("Enter due date (yyyy-mm-dd): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
                    {
                        todoList.AddTask(new Task(title, description, dueDate));
                    }
                    else
                    {
                        Console.WriteLine("Invalid date format. Please try again.");
                    }
                    break;

                case "2":
                    todoList.ViewTasks();
                    break;

                case "3":
                    Console.Write("Enter task number to complete: ");
                    if (int.TryParse(Console.ReadLine(), out int completeIndex) && completeIndex > 0)
                    {
                        todoList.CompleteTask(completeIndex - 1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid task number.");
                    }
                    break;

                case "4":
                    Console.Write("Enter task number to remove: ");
                    if (int.TryParse(Console.ReadLine(), out int removeIndex) && removeIndex > 0)
                    {
                        todoList.RemoveTask(removeIndex - 1);
                    }
                    else
                    {
                        Console.WriteLine("Invalid task number.");
                    }
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }

//classes
public class Task
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsCompleted { get; set; }

    public Task(string title, string description, DateTime dueDate)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        IsCompleted = false;
    }

    public override string ToString()
    {
        return $"{Title} - Due: {DueDate.ToShortDateString()} | Completed: {IsCompleted}";
    }
}

public class TodoList
{
    private List<Task> tasks;

    public TodoList()
    {
        tasks = new List<Task>();
    }

    public void AddTask(Task task)
    {
        tasks.Add(task);
        Console.WriteLine("Task added.");
    }

    public void ViewTasks()
    {
        if (tasks.Count == 0)
        {
            Console.WriteLine("No tasks available.");
            return;
        }

        for (int i = 0; i < tasks.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {tasks[i]}");
        }
    }

    public void CompleteTask(int index)
    {
        if (index < 0 || index >= tasks.Count)
        {
            Console.WriteLine("Invalid task number.");
            return;
        }

        tasks[index].IsCompleted = true;
        Console.WriteLine("Task marked as completed.");
    }

    public void RemoveTask(int index)
    {
        if (index < 0 || index >= tasks.Count)
        {
            Console.WriteLine("Invalid task number.");
            return;
        }

        tasks.RemoveAt(index);
        Console.WriteLine("Task removed.");
    }
}
