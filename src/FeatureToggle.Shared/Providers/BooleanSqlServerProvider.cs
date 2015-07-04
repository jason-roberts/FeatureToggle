#if (FEATURETOGGLE_FULL)

using System;
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
                    return (bool)cmd.ExecuteScalar();
                }
            }
        }

        private string GetConnectionStringFromConfig(IFeatureToggle toggle)
        {
            const string emptyConnectionStringsValueError = "The <connectionStrings> value for connection named '{0}' is empty.";
            const string emptyValueForAppSettingsKeyError = "The <appSettings> value for key '{0}' is empty.";

            var prefixedToggleConfigName = ToggleConfigurationSettings.Prefix + toggle.GetType().Name;
            var appSettingsConnectionStringKey = prefixedToggleConfigName + ".ConnectionString";
            var appSettingsConnectionStringNameKey = prefixedToggleConfigName + ".ConnectionStringName";

            var isConfiguredViaAppSettingsConnectionString = ConfigurationManager.AppSettings.AllKeys.Contains(appSettingsConnectionStringKey);
            var isConfiguredViaAppSettingsConnectionStringName = ConfigurationManager.AppSettings.AllKeys.Contains(appSettingsConnectionStringNameKey);
            var isConfiguredViaConnectionStrings = ConfigurationManager.ConnectionStrings[prefixedToggleConfigName] != null;

            if (CountConnectionStringConfigurations(isConfiguredViaAppSettingsConnectionString, 
                isConfiguredViaAppSettingsConnectionStringName, isConfiguredViaConnectionStrings) > 1)
            {
                throw new ToggleConfigurationError(
                    string.Format(
                        "The connection string for '{0}' is configured multiple times.",
                        prefixedToggleConfigName));
            }

            if (CountConnectionStringConfigurations(isConfiguredViaAppSettingsConnectionString, isConfiguredViaAppSettingsConnectionStringName, isConfiguredViaConnectionStrings) == 0)
            {
                throw new ToggleConfigurationError(
                    string.Format(
                        "The connection string was not configured in <appSettings> with a key of '{0}' or '{1}' nor in <connectionStrings> with a name of '{2}'.",
                        appSettingsConnectionStringKey, appSettingsConnectionStringNameKey, prefixedToggleConfigName));
            }

            string configuredConnectionString = String.Empty;

            if (isConfiguredViaAppSettingsConnectionString)
            {
                configuredConnectionString = ConfigurationManager.AppSettings[appSettingsConnectionStringKey];

                if (string.IsNullOrWhiteSpace(configuredConnectionString))
                {
                    throw new ToggleConfigurationError(string.Format(emptyValueForAppSettingsKeyError, appSettingsConnectionStringKey));
                }
            }
            else if (isConfiguredViaAppSettingsConnectionStringName)
            {
                var connectionStringName = ConfigurationManager.AppSettings[appSettingsConnectionStringNameKey];

                if (string.IsNullOrWhiteSpace(connectionStringName))
                {
                    throw new ToggleConfigurationError(string.Format(emptyValueForAppSettingsKeyError, appSettingsConnectionStringNameKey));
                }

                var connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];

                if (connectionStringSettings == null)
                {
                    throw new ToggleConfigurationError(string.Format("No entry named '{0}' exists in <connectionStrings>.", connectionStringName));
                }

                configuredConnectionString = connectionStringSettings.ConnectionString;

                if (string.IsNullOrWhiteSpace(configuredConnectionString))
                {
                    throw new ToggleConfigurationError(string.Format(emptyConnectionStringsValueError, connectionStringName));
                }
            }
            else
            {
                configuredConnectionString = ConfigurationManager.ConnectionStrings[prefixedToggleConfigName].ConnectionString;

                if (string.IsNullOrWhiteSpace(configuredConnectionString))
                {
                    throw new ToggleConfigurationError(string.Format(emptyConnectionStringsValueError, prefixedToggleConfigName));
                }
            }

            return configuredConnectionString;
        }

        private int CountConnectionStringConfigurations(params bool[] configurations)
        {
            return configurations.Count(c => c);
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