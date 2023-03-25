using DbUp;
using DbUp.Engine;
using DbUp.ScriptProviders;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users_Data.Entities.AddressInfo;
using Utils.Global;

namespace ScriptExecutor.Executors
{
    public class MSSqlScriptExecutor
    {
        public async Task<int> RunCountriesRegionsCitiesScripts()
        {
			var result = 0;

            string CS = GlobalVariables.Users_DB_CN;
            string countriesFP = @"\Scripts\CountriesRegionsCities\countries.json";
			string regionsFP = @"\Scripts\CountriesRegionsCities\regions.json";
			string citiesFP = @"\Scripts\CountriesRegionsCities\cities.json";

			int CountriesResult = await ExecuteCountriesJson(CS, countriesFP);
			int RegionsResult = await ExecuteRegionsJson(CS, regionsFP);
			int CitiesResult = await ExecuteCitiesJson(CS, citiesFP);

			if (CountriesResult == 1 && RegionsResult == 1 && CitiesResult == 1)
			{
				result = 1;
			}
			else
			{
				result = -1;
			}

			return result;
        }
        public async Task<int> RunCategoriesScripts()
        {
            string CS = GlobalVariables.Freelance_DB_CN;
            string FP = @"\Scripts\MSSql\Categories";
            return await ExecuteScripts(CS, FP);
        }
        public async Task<int> RunPricingPlansAndFeaturesScripts()
        {
            string CS = GlobalVariables.Freelance_DB_CN;
            string FP = @"\Scripts\MSSql\PricingPlans";
            return await ExecuteScripts(CS, FP);
        }

		public async Task<int> ExecuteCitiesJson(string ConnectionString, string FilePath)
		{
			var result = 0;

			// Read the JSON file
			string jsonText = File.ReadAllText(FilePath);

			// Deserialize the JSON object into a C# object
			var jsonObject = JsonConvert.DeserializeObject<List<City>>(jsonText);

			await Task.Run(() => 
			{
				try
				{
					// Create a SQL connection
					using (SqlConnection connection = new SqlConnection(ConnectionString))
					{
						connection.Open();

						for (int i = 0; i < jsonObject.Count; i++)
						{
							// Create a SQL command
							string sqlCommandText = "SET IDENTITY_INSERT[dbo].[Cities] ON" + "\n" +
								"INSERT INTO [dbo].[Cities] (id, name, region_id, region_code, country_id, country_code,  latitude, longitude, wikiDataId) " +
													"VALUES (@id, @name, @region_id, @region_code, @country_id, @country_code, @latitude, @longitude, @wikiDataId)";
							using (SqlCommand command = new SqlCommand(sqlCommandText, connection))
							{
								// Set the parameter values
								command.Parameters.AddWithValue("@id", jsonObject[i].Id);
								command.Parameters.AddWithValue("@name", jsonObject[i].name);
								command.Parameters.AddWithValue("@region_id", jsonObject[i].region_id);
								command.Parameters.AddWithValue("@region_code", jsonObject[i].region_code);
								command.Parameters.AddWithValue("@country_id", jsonObject[i].country_id);
								command.Parameters.AddWithValue("@country_code", jsonObject[i].country_code);
								command.Parameters.AddWithValue("@latitude", jsonObject[i].latitude);
								command.Parameters.AddWithValue("@longitude", jsonObject[i].longitude);
								command.Parameters.AddWithValue("@wikiDataId", jsonObject[i].wikiDataId);

								// Execute the SQL command
								command.ExecuteNonQuery();
							}
						}

						var closingCommand = new SqlCommand("SET IDENTITY_INSERT [dbo].[Cities] OFF");
						closingCommand.ExecuteNonQuery();

						// Close the connection
						connection.Close();
					}

					result = 1;
				}
				catch (Exception)
				{
					result = -1;
				}
			});

			return result;
		}

		public async Task<int> ExecuteRegionsJson(string ConnectionString, string FilePath)
		{
			var result = 0;

			// Read the JSON file
			string jsonData = File.ReadAllText(FilePath);

			// Deserialize the JSON data into a C# object
			var data = JsonConvert.DeserializeObject<List<Region>>(jsonData);

			await Task.Run(() =>
			{
				try
				{
					// Connect to the database
					using (SqlConnection connection = new SqlConnection(ConnectionString))
					{
						connection.Open();
						for (int i = 0; i < data.Count; i++)
						{
							// Define the SQL command
							string sql = "SET IDENTITY_INSERT [dbo].[Regions] ON" + "\n" +
								"INSERT INTO [dbo].[Regions] (id, name, country_id, country_code, type, latitude, longitude) " +
										 "VALUES (@id, @name, @country_id, @country_code, @type, @latitude, @longitude)";
							SqlCommand command = new SqlCommand(sql, connection);

							// Add parameters to the SQL command
							command.Parameters.AddWithValue("@id", data[i].Id);
							command.Parameters.AddWithValue("@name", data[i].name);
							command.Parameters.AddWithValue("@country_id", data[i].country_id);
							command.Parameters.AddWithValue("@country_code", data[i].country_code);
							command.Parameters.AddWithValue("@type", data[i].type);
							command.Parameters.AddWithValue("@latitude", data[i].latitude);
							command.Parameters.AddWithValue("@longitude", data[i].longitude);

							// Execute the SQL command
							command.ExecuteNonQuery();
						}

						var closingCommand = new SqlCommand("SET IDENTITY_INSERT [dbo].[Regions] OFF");
						closingCommand.ExecuteNonQuery();

						// Close the connection
						connection.Close();
					}

					result = 1;
				}
				catch (Exception)
				{
					result = -1;
				}
			});
			
			return result;
		}


		public async Task<int> ExecuteCountriesJson(string ConnectionString, string FilePath)
        {
			var result = 0;

            // Read the JSON file into a string
            string json = File.ReadAllText(FilePath);

            // Deserialize the JSON into a C# object
            var country = JsonConvert.DeserializeObject<List<Country>>(json);

            // Create a connection to the MSSQL database
            var connectionString = ConnectionString;

			await Task.Run(() =>
			{
				try
				{
					using (var connection = new SqlConnection(connectionString))
					{
						// Open the connection
						connection.Open();

						for (int i = 0; i < country.Count; i++)
						{
							// Create a command to insert the data into the database
							var command = new SqlCommand("SET IDENTITY_INSERT [dbo].[Countries] ON" + "\n" +
								"INSERT INTO countries (id, name, iso3, iso2, numeric_code, phonecode, capital, currency, currency_name, currency_symbol, tld, native, region, subregion, timezones, translations, latitude, longitude, emoji, emojiU) VALUES (@id, @name, @iso3, @iso2, @numeric_code, @phonecode, @capital, @currency, @currency_name, @currency_symbol, @tld, @native, @region, @subregion, @timezones, @translations, @latitude, @longitude, @emoji, @emojiU)", connection);

							// Set the parameters of the command
							command.Parameters.AddWithValue("@id", country[i].Id);
							command.Parameters.AddWithValue("@name", country[i].name);
							command.Parameters.AddWithValue("@iso3", country[i].iso3);
							command.Parameters.AddWithValue("@iso2", country[i].iso2);
							command.Parameters.AddWithValue("@numeric_code", country[i].numeric_code);
							command.Parameters.AddWithValue("@phonecode", country[i].phonecode);
							command.Parameters.AddWithValue("@capital", country[i].capital);
							command.Parameters.AddWithValue("@currency", country[i].currency);
							command.Parameters.AddWithValue("@currency_name", country[i].currency_name);
							command.Parameters.AddWithValue("@currency_symbol", country[i].currency_symbol);
							command.Parameters.AddWithValue("@tld", country[i].tld);
							command.Parameters.AddWithValue("@native", country[i].native);
							command.Parameters.AddWithValue("@region", country[i].region);
							command.Parameters.AddWithValue("@subregion", country[i].subregion);
							command.Parameters.AddWithValue("@timezones", country[i].timezones);
							command.Parameters.AddWithValue("@translations", country[i].translations);
							command.Parameters.AddWithValue("@latitude", country[i].latitude);
							command.Parameters.AddWithValue("@longitude", country[i].longitude);
							command.Parameters.AddWithValue("@emoji", country[i].emoji);
							command.Parameters.AddWithValue("@emojiU", country[i].emojiU);

							// Execute the command
							command.ExecuteNonQuery();
						}

						var closingCommand = new SqlCommand("SET IDENTITY_INSERT [dbo].[Countries] OFF");
						closingCommand.ExecuteNonQuery();

						// Close the connection
						connection.Close();
					}

					result = 1;
				}
				catch (Exception)
				{
					result = -1;
				}
			});

			return result;
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

	public class Timezones
	{
		public string zoneName { get; set; }
		public int gmtOffset { get; set; }
		public string gmtOffsetName { get; set; }
		public string abbreviation { get; set; }
		public string tzName { get; set; }
	}

	public class Translations
	{
		public string kr { get; set; }
		public string pt_BR { get; set; }
		public string pt { get; set; }
		public string nl { get; set; }
		public string hr { get; set; }
		public string fa { get; set; }
		public string de { get; set; }
		public string es { get; set; }
		public string fr { get; set; }
		public string ja { get; set; }
		public string it { get; set; }
		public string cn { get; set; }
		public string tr { get; set; }
	}
}
