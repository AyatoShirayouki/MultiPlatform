using DbUp;
using System.Reflection;

internal class Program
{
    public static int Main(string[] args)
    {
        var connectionString =
        args.FirstOrDefault()
        ?? "Data Source=AKIHIRO\\SQLEXPRESS;initial catalog=CarCareCom;user id=alex;password=rexibexi1";

        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader =
            DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();

        var result = upgrader.PerformUpgrade();

        if (!result.Successful)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(result.Error);
            Console.ResetColor();
#if DEBUG
            Console.ReadLine();
#endif
            return -1;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();
        return 0;
    }
}