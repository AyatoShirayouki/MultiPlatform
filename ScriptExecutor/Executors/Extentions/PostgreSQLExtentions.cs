using DbUp;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptExecutor.Executors.Extentions
{
    public static class PostgreSQLExtentions
    {
        public static void PostgresqlDatabase(this SupportedDatabasesForDropDatabase supportedDatabases, string connectionString)
        {
            NpgsqlConnectionStringBuilder builder = new NpgsqlConnectionStringBuilder(connectionString);

            string databaseName = builder.Database;
            builder.Database = "postgres";

            using (NpgsqlConnection connection = new NpgsqlConnection(builder.ToString()))
            {
                connection.Open();

                using (NpgsqlCommand command = new NpgsqlCommand($"SELECT pg_terminate_backend(pg_stat_activity.pid) FROM pg_stat_activity WHERE pg_stat_activity.datname = \'{databaseName}\'; DROP DATABASE IF EXISTS \"{databaseName}\"", connection))
                {
                    command.ExecuteNonQuery();
                }

                connection.Close();
            }
        }
    }
}
