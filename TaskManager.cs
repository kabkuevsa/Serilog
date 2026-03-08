using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serilog
{

    public class TaskManager
    {
        private List<TaskItem> tasks = new List<TaskItem>();

        public void AddTask(string title)
        {
            Log.Debug("Начало операции AddTask с параметром: {TaskTitle}", title ?? "null");

            if (string.IsNullOrWhiteSpace(title))
            {
                Log.Warning("Попытка добавить задачу с пустым названием");
                Console.WriteLine("Ошибка: название задачи не может быть пустым!");
                return;
            }

            var task = new TaskItem(title);
            tasks.Add(task);

            Log.Information("Задача \"{TaskTitle}\" успешно добавлена. Всего задач: {TaskCount}", title, tasks.Count);
            Console.WriteLine($"Задача \"{title}\" добавлена.");
        }

        public void RemoveTask(string title)
        {
            Log.Debug("Начало операции RemoveTask для задачи: {TaskTitle}", title ?? "null");

            var task = tasks.FirstOrDefault(t => t.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            if (task == null)
            {
                Log.Error("Задача \"{TaskTitle}\" не найдена. Доступные задачи: {@AvailableTasks}",
                    title, tasks.Select(t => t.Title).ToList());
                Console.WriteLine($"Задача \"{title}\" не найдена.");
                return;
            }

            tasks.Remove(task);
            Log.Information("Задача \"{TaskTitle}\" успешно удалена. Осталось задач: {TaskCount}", title, tasks.Count);
            Console.WriteLine($"Задача \"{title}\" удалена.");
        }

        public void ListTasks()
        {
            Log.Debug("Начало операции ListTasks");

            if (tasks.Count == 0)
            {
                Log.Information("Список задач пуст");
                Console.WriteLine("Список задач пуст.");
                return;
            }

            Log.Information("Отображение списка задач. Всего задач: {TaskCount}", tasks.Count);
            Console.WriteLine($"Всего задач: {tasks.Count}");

            for (int i = 0; i < tasks.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {tasks[i].Title}");
                Log.Debug("Задача #{Index}: {TaskTitle} (создана: {CreatedDate})",
                    i + 1, tasks[i].Title, tasks[i].CreatedDate);
            }
        }

        public int GetTaskCount()
        {
            return tasks.Count;
        }
    }
}
