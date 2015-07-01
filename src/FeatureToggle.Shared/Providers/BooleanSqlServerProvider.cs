#if (FEATURETOGGLE_FULL)

using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using FeatureToggle.Core;

namespace FeatureToggle.Providers
{
    public class BooleanSqlServerProvider : IBooleanToggleValueProvider
    {
        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var connectionString = GetConnectionStringFromConfig(toggle);
            var sqlCommandText = GetCommandTextFromAppConfig(toggle);
            

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new SqlCommand(sqlCommandText, connection))
                {
                    return (bool) cmd.ExecuteScalar();                    
                }
            }
        }

        private string GetConnectionStringFromConfig(IFeatureToggle toggle)
        {
            var prefixedToggleConfigName = ToggleConfigurationSettings.Prefix + toggle.GetType().Name;
            var appSettingsKey = prefixedToggleConfigName + ".ConnectionString";            


            var isConnectionConfiguredViaAppSettings = ConfigurationManager.AppSettings.AllKeys.Contains(appSettingsKey);
            var isConnectionConfiguredViaConnectionStrings = ConfigurationManager.ConnectionStrings[prefixedToggleConfigName] != null;

            if (isConnectionConfiguredViaAppSettings && isConnectionConfiguredViaConnectionStrings)
            {
                throw new ToggleConfigurationError(
                    string.Format(
                        "The connection string for '{0}' is specified in both <appSettings> and <connectionStrings>.",
                        prefixedToggleConfigName));

            }

            if (!isConnectionConfiguredViaAppSettings && !isConnectionConfiguredViaConnectionStrings)
            {
                throw new ToggleConfigurationError(
                    string.Format(
                        "The connection string was not found in <appSettings> with a key of '{0}' or in <connectionStrings> with a name of '{1}'.",
                        appSettingsKey, prefixedToggleConfigName));
            }

            string configuredConnectionString;

            if (isConnectionConfiguredViaAppSettings)
            {
                configuredConnectionString = ConfigurationManager.AppSettings[appSettingsKey];

                if (string.IsNullOrWhiteSpace(configuredConnectionString))
                {
                    throw new ToggleConfigurationError(string.Format("The <appSettings> value for key '{0}' is empty.", appSettingsKey));                    
                }

                return configuredConnectionString;
            }

            configuredConnectionString = ConfigurationManager.ConnectionStrings[prefixedToggleConfigName].ConnectionString;

            if (string.IsNullOrWhiteSpace(configuredConnectionString))
            {
                throw new ToggleConfigurationError(string.Format("The <connectionStrings> value for connected named '{0}' is empty.", prefixedToggleConfigName));
            }

            return configuredConnectionString; 
        }

        private string GetCommandTextFromAppConfig(IFeatureToggle toggle)
        {
            var sqlCommandKey = ToggleConfigurationSettings.Prefix + toggle.GetType().Name + ".SqlStatement";

            var sqlCommandIsMissing = !ConfigurationManager.AppSettings.AllKeys.Contains(sqlCommandKey);

            if (sqlCommandIsMissing)
            {
                throw new ToggleConfigurationError(string.Format("The appSettings key '{0}' was not found.",
                    sqlCommandKey));
            }

            var configuredSqlCommand = ConfigurationManager.AppSettings[sqlCommandKey];

            if (string.IsNullOrWhiteSpace(configuredSqlCommand))
            {
                throw new ToggleConfigurationError(string.Format("The <appSettings> value for key '{0}' is empty.", sqlCommandKey));                
            }

            return configuredSqlCommand;
        }
    }
}


#endif