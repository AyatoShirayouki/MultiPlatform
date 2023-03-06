using DbUp;
using DbUp.Engine;
using DbUp.ScriptProviders;
using ScriptExecutor.Executors.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils.Global;

namespace ScriptExecutor.Executors
{
    public class PostgreSQLScriptExecutor
    {
        public async Task<int> RunCountriesRegionsCitiesScripts()
        {
            string CS = GlobalVariables.Users_DB_CN;
            string FP = @"Scripts\MSSql\CountriesRegionsCities";
            return await ExecuteScripts(CS, FP);
        }
        private async Task<int> ExecuteScripts(string ConnectionString, string FilePath)
        {
            bool dropDatabase = false;

            DatabaseUpgradeResult result = await Task.Run(() => RunDatabaseUpdate(ConnectionString, dropDatabase, FilePath));

            return !result.Successful ? ShowError(result.Error) : ShowSuccess();
        }

        private DatabaseUpgradeResult RunDatabaseUpdate(string connectionString, bool dropDatabase, string SRC_PATH)
        {
            if (dropDatabase)
            {
                DropDatabase.For.PostgresqlDatabase(connectionString);
            }

            EnsureDatabase.For.PostgresqlDatabase(connectionString);

            UpgradeEngine upgradeEngine = DeployChanges.To.PostgresqlDatabase(connectionString)
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
