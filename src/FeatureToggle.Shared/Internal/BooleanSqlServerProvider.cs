#if NETFULL

using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using FeatureToggle;

namespace FeatureToggle.Internal
{
    public class BooleanSqlServerProvider : IBooleanToggleValueProvider
    {
        private const string AppSettingsKeyValuesIsEmptyErrorMessage = "The <appSettings> value for key '{0}' is empty.";
        private const string NamedConnectionStringsValueIsEmptyErrorMessage = "The <connectionStrings> value for connection named '{0}' is empty.";

        public bool EvaluateBooleanToggleValue(IFeatureToggle toggle)
        {
            var connectionString = GetConnectionStringFromConfig(toggle);
            var sqlCommandText = GetCommandTextFromAppConfig(toggle);

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (var cmd = new SqlCommand(sqlCommandText, connection))
                {
                    return (bool)cmd.ExecuteScalar();
                }
            }
        }

        private string GetConnectionStringFromConfig(IFeatureToggle toggle)
        {
            var prefixedToggleConfigName = ToggleConfigurationSettings.Prefix + toggle.GetType().Name;
            var appSettingsConnectionStringKey = prefixedToggleConfigName + ".ConnectionString";
            var appSettingsConnectionStringNameKey = prefixedToggleConfigName + ".ConnectionStringName";

            var isConfiguredViaAppSettingsConnectionString = ConfigurationManager.AppSettings.AllKeys.Contains(appSettingsConnectionStringKey);
            var isConfiguredViaAppSettingsConnectionStringName = ConfigurationManager.AppSettings.AllKeys.Contains(appSettingsConnectionStringNameKey);
            var isConfiguredViaConnectionStrings = ConfigurationManager.ConnectionStrings[prefixedToggleConfigName] != null;

            ValidateConnectionStringSpecifiedOnlyOnce(isConfiguredViaAppSettingsConnectionString, isConfiguredViaAppSettingsConnectionStringName, isConfiguredViaConnectionStrings, prefixedToggleConfigName);
            ValidateConnectionStringNotMissing(isConfiguredViaAppSettingsConnectionString, isConfiguredViaAppSettingsConnectionStringName, isConfiguredViaConnectionStrings, appSettingsConnectionStringKey, appSettingsConnectionStringNameKey, prefixedToggleConfigName);


            if (isConfiguredViaAppSettingsConnectionString)
            {
                return GetAppSettingsConnectionString(appSettingsConnectionStringKey);
            }
            
            if (isConfiguredViaAppSettingsConnectionStringName)
            {
                return GetConnectionStringFromAppSettingsValueThatPointsToNamedConnectionString(appSettingsConnectionStringNameKey);
            }

            return GetConnectionStringDirectlyFromNamedConnectionStrings(prefixedToggleConfigName);
        }

        private static void ValidateConnectionStringNotMissing(bool isConfiguredViaAppSettingsConnectionString,
            bool isConfiguredViaAppSettingsConnectionStringName, bool isConfiguredViaConnectionStrings,
            string appSettingsConnectionStringKey, string appSettingsConnectionStringNameKey, string prefixedToggleConfigName)
        {
            var noConnectionStringConfigured = !isConfiguredViaAppSettingsConnectionString &&
                                               !isConfiguredViaAppSettingsConnectionStringName &&
                                               !isConfiguredViaConnectionStrings;

            if (noConnectionStringConfigured)
            {
                throw new ToggleConfigurationError(
                    string.Format(
                        "The connection string was not configured in <appSettings> with a key of '{0}' or '{1}' nor in <connectionStrings> with a name of '{2}'.",
                        appSettingsConnectionStringKey, appSettingsConnectionStringNameKey, prefixedToggleConfigName));
            }
        }

        private void ValidateConnectionStringSpecifiedOnlyOnce(bool isConfiguredViaAppSettingsConnectionString,
            bool isConfiguredViaAppSettingsConnectionStringName, bool isConfiguredViaConnectionStrings,
            string prefixedToggleConfigName)
        {
            var connectionStringConfiguredInMultiplePlaces = new[]
                                                             {
                                                                 isConfiguredViaAppSettingsConnectionString,
                                                                 isConfiguredViaAppSettingsConnectionStringName,
                                                                 isConfiguredViaConnectionStrings
                                                             }.Count(x => x == true) > 1;


            if (connectionStringConfiguredInMultiplePlaces)
            {
                throw new ToggleConfigurationError(
                    string.Format(
                        "The connection string for '{0}' is configured multiple times.",
                        prefixedToggleConfigName));
            }
        }


        private static string GetConnectionStringDirectlyFromNamedConnectionStrings(string prefixedToggleConfigName)
        {
            var configuredConnectionString = ConfigurationManager.ConnectionStrings[prefixedToggleConfigName].ConnectionString;

            if (string.IsNullOrWhiteSpace(configuredConnectionString))
            {
                throw new ToggleConfigurationError(string.Format(NamedConnectionStringsValueIsEmptyErrorMessage, prefixedToggleConfigName));
            }

            return configuredConnectionString;
        }

        private static string GetConnectionStringFromAppSettingsValueThatPointsToNamedConnectionString(string appSettingsConnectionStringNameKey)
        {
            var connectionStringName = ConfigurationManager.AppSettings[appSettingsConnectionStringNameKey];

            if (string.IsNullOrWhiteSpace(connectionStringName))
            {
                throw new ToggleConfigurationError(string.Format(AppSettingsKeyValuesIsEmptyErrorMessage,
                    appSettingsConnectionStringNameKey));
            }

            var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];

            if (connectionStringSettings == null)
            {
                throw new ToggleConfigurationError(string.Format("No entry named '{0}' exists in <connectionStrings>.",
                    connectionStringName));
            }

            var configuredConnectionString = connectionStringSettings.ConnectionString;

            if (string.IsNullOrWhiteSpace(configuredConnectionString))
            {
                throw new ToggleConfigurationError(string.Format(NamedConnectionStringsValueIsEmptyErrorMessage,
                    connectionStringName));
            }

            return configuredConnectionString;
        }

        private static string GetAppSettingsConnectionString(string appSettingsConnectionStringKey)
        {
            var configuredConnectionString = ConfigurationManager.AppSettings[appSettingsConnectionStringKey];

            if (string.IsNullOrWhiteSpace(configuredConnectionString))
            {
                throw new ToggleConfigurationError(string.Format(AppSettingsKeyValuesIsEmptyErrorMessage,
                    appSettingsConnectionStringKey));
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