using DbUp;
using DbUp.Engine;
using DbUp.ScriptProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Global;

namespace ScriptExecutor.Executors
{
    public class MSSqlScriptExecutor
    {
        /*
        private static string _connectionString; //E:\Projects\MultiPlatformProject\MultiPlatform\ScriptExecutor\Scripts\CountriesAndCities
        private static string _filePath;// = @"Scripts\MSSql\CountriesAndCities"; //= GlobalVariables.Users_DB_CN;
        public MSSqlScriptExecutor(string connectionString, string filePath)
        {
            _connectionString = connectionString;
            _filePath = filePath;
        }
        */

        public static async Task<int> RunCountriesRegionsCitiesScripts()
        {
            string CS = GlobalVariables.Users_DB_CN;
            string FP = @"Scripts\MSSql\CountriesRegionsCities";
            return await ExecuteScripts(CS, FP);
        }
        private static async Task<int> ExecuteScripts(string ConnectionString, string FilePath)
        {
            bool dropDatabase = false;

            var result =  await Task.Run(() => RunDatabaseUpdate(ConnectionString, dropDatabase, FilePath));

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
            return -1;
        }
        private static int ShowSuccess()
        {
            return 0;
        }
    }
}
