using System;
using Serilog;


namespace Serilog
{

    class Program
    {
        static void Main(string[] args)
        {
            // Настройка Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug() // Логируем все уровни начиная с Debug
                .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
                .WriteTo.File(
                    path: "logs/taskmanager-.log",
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
                    rollingInterval: RollingInterval.Day, // Новый файл каждый день
                    retainedFileCountLimit: 7 // Хранить логи за 7 дней
                )
                .CreateLogger();

            try
            {
                Log.Debug("Инициализация TaskManager...");
                Log.Information("Программа TaskManager запущена.");

                Console.WriteLine("=== Task Manager (с логированием Serilog) ===");
                Console.WriteLine("Доступные команды:");
                Console.WriteLine("add    - добавить задачу");
                Console.WriteLine("remove - удалить задачу");
                Console.WriteLine("list   - показать список задач");
                Console.WriteLine("exit   - выход из программы");
                Console.WriteLine("help   - показать справку");
                Console.WriteLine(new string('=', 40));

                var taskManager = new TaskManager();
                bool isRunning = true;

                while (isRunning)
                {
                    Console.Write("\nВведите команду: ");
                    string command = Console.ReadLine()?.Trim().ToLower();

                    switch (command)
                    {
                        case "add":
                            Console.Write("Введите название задачи: ");
                            string taskTitle = Console.ReadLine()?.Trim();
                            taskManager.AddTask(taskTitle);
                            break;

                        case "remove":
                            Console.Write("Введите название задачи для удаления: ");
                            string removeTitle = Console.ReadLine()?.Trim();
                            taskManager.RemoveTask(removeTitle);
                            break;

                        case "list":
                            taskManager.ListTasks();
                            break;

                        case "exit":
                            Log.Information("Завершение работы программы");
                            Console.WriteLine("Завершение работы...");
                            isRunning = false;
                            break;

                        case "help":
                            ShowHelp();
                            break;

                        default:
                            Log.Warning("Введена неизвестная команда: {Command}", command);
                            Console.WriteLine($"Неизвестная команда: {command}. Введите 'help' для списка команд.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Критическая ошибка в программе");
                Console.WriteLine("Произошла критическая ошибка. Подробности в логах.");
            }
            finally
            {
                Log.Debug("Завершение работы логгера...");
                Log.CloseAndFlush();

                Console.WriteLine("Нажмите любую клавишу для выхода...");
                Console.ReadKey();
            }
        }

        static void ShowHelp()
        {
            Log.Debug("Пользователь запросил справку");
            Console.WriteLine("\n=== Справка по командам ===");
            Console.WriteLine("add    - Добавить новую задачу");
            Console.WriteLine("remove - Удалить задачу по названию");
            Console.WriteLine("list   - Показать все задачи");
            Console.WriteLine("exit   - Выйти из программы");
            Console.WriteLine("help   - Показать эту справку");
            Console.WriteLine(new string('=', 40));
        }
    }
}
