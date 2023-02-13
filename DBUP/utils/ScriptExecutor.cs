using DbUp.Engine;
using DbUp.ScriptProviders;
using DbUp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBUP.Utils
{
    public class ScriptExecutor
    {
        private string? cs;
        private string? SRC_PATH;

        private static string? connection_String;
        private static string? SRC_PATH_Create_Scripts;

        public ScriptExecutor(string connectionString, string SRC_PATH_CreateScripts)
        {
            this.cs = connectionString;
            this.SRC_PATH = SRC_PATH_CreateScripts;

            connection_String = cs;
            SRC_PATH_Create_Scripts = SRC_PATH;
        }

        public int CreateScripts()
        {
            bool dropDatabase = false;

            DatabaseUpgradeResult result = RunDatabaseUpdate(connection_String, dropDatabase, SRC_PATH_Create_Scripts);

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
}
