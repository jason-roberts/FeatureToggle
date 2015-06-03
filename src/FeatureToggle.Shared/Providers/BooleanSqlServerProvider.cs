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
            var cmdText = GetCommandTextFromAppConfig(toggle);

            var cnString = GetConnectionStringFromConfig(toggle);

            using (var cn = new SqlConnection(cnString))
            {
                cn.Open();

                using (var cmd = new SqlCommand(cmdText, cn))
                {
                    return (bool) cmd.ExecuteScalar();                    
                }
            }
        }

        private string GetConnectionStringFromConfig(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ToggleConfigurationSettings.Prefix + toggle.GetType().Name + ".ConnectionString";
            var toggleConnectionNameInConfig = ToggleConfigurationSettings.Prefix + toggle.GetType().Name + ".ConnectionStringName";

            //Check if either a ConnectionString or a ConnectionStringName exists
            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig) && !ConfigurationManager.AppSettings.AllKeys.Contains(toggleConnectionNameInConfig))
                throw new ToggleConfigurationError(string.Format("The key '{0}' or '{1}' was not found in AppSettings",
                                                                     toggleNameInConfig, 
                                                                     toggleConnectionNameInConfig));

            string connectionString;
            //Use the ConnectionStringName if it's given
            if(ConfigurationManager.AppSettings.AllKeys.Contains(toggleConnectionNameInConfig))
            {
                string connectionStringName = ConfigurationManager.AppSettings[toggleConnectionNameInConfig];
                ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings[connectionStringName];
                if (connectionStringSettings == null || string.IsNullOrEmpty(connectionStringSettings.ConnectionString))
                    throw new ToggleConfigurationError(string.Format("The connectionString with Key '{0}' was not found or is empty in the web.config file.",
                                                                     connectionStringName));
                connectionString = connectionStringSettings.ConnectionString;
            }
            else
            {
                connectionString = ConfigurationManager.AppSettings[toggleNameInConfig];
            }


            return connectionString;
        }

        private string GetCommandTextFromAppConfig(IFeatureToggle toggle)
        {
            var toggleNameInConfig = ToggleConfigurationSettings.Prefix + toggle.GetType().Name + ".SqlStatement";

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ToggleConfigurationError(string.Format("The key '{0}' was not found in AppSettings",
                                                                     toggleNameInConfig));

            return ConfigurationManager.AppSettings[toggleNameInConfig];
        }
    }
}


#endif