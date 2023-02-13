using DbUp;
using DbUp.Engine;
using DbUp.ScriptProviders;
using System.Reflection;
using Utils.Global;

internal class Program
{
    public static int Main(string[] args)
    {
        var connectionString =
        args.FirstOrDefault()
        ?? GlobalVariables.Users_DB_CN;

        var SRC_PATH_Create_Scripts = @"Scripts\CountriesAndCities";

        /*
        EnsureDatabase.For.SqlDatabase(connectionString);

        var upgrader =
            DeployChanges.To
                .SqlDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .LogToConsole()
                .Build();
        */

        bool dropDatabase = false;

        var result = RunDatabaseUpdate(connectionString, dropDatabase, SRC_PATH_Create_Scripts); //upgrader.PerformUpgrade();

        /*
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
        */

        return !result.Successful ? ShowError(result.Error) : ShowSuccess();
    }

    private static DatabaseUpgradeResult RunDatabaseUpdate(string connectionString, bool dropDatabase, string SRC_PATH)
    {
        if (dropDatabase)
        {
            DropDatabase.For.SqlDatabase(connectionString);
        }

        EnsureDatabase.For.SqlDatabase(connectionString);

        UpgradeEngine upgradeEngine = DeployChanges.To.SqlDatabase(connectionString)
            .WithScriptsFromFileSystem
            (
                SRC_PATH, new FileSystemScriptOptions
                {
                    IncludeSubDirectories = true
                }
            )
            .WithTransactionPerScript()
            .WithVariablesDisabled()
            .Build();

        return upgradeEngine.PerformUpgrade();
    }

    private static int ShowError(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(ex);
        Console.ResetColor();

        return -1;
    }

    private static int ShowSuccess()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Success!");
        Console.ResetColor();

        return 0;
    }
}