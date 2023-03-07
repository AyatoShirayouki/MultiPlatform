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
        public async Task<int> RunCountriesRegionsCitiesScripts()
        {
            string CS = GlobalVariables.Users_DB_CN;
            string FP = @"E:\Projects\MultiPlatformProject\MultiPlatform\ScriptExecutor\Executors\Scripts\MSSql\CountriesRegionsCities";
            return await ExecuteScripts(CS, FP);
        }
        private async Task<int> ExecuteScripts(string ConnectionString, string FilePath)
        {
            bool dropDatabase = false;

            var result =  await Task.Run(() => RunDatabaseUpdate(ConnectionString, dropDatabase, FilePath));

            return !result.Successful ? ShowError(result.Error) : ShowSuccess();
        }
        private DatabaseUpgradeResult RunDatabaseUpdate(string connectionString, bool dropDatabase, string SRC_PATH)
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
