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
            var toggleNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name + ".ConnectionString";

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ToggleConfigurationError(string.Format("The key '{0}' was not found in AppSettings",
                                                                     toggleNameInConfig));

            return ConfigurationManager.AppSettings[toggleNameInConfig];
        }

        private string GetCommandTextFromAppConfig(IFeatureToggle toggle)
        {
            var toggleNameInConfig = AppSettingsKeys.Prefix + "." + toggle.GetType().Name + ".SqlStatement";

            if (!ConfigurationManager.AppSettings.AllKeys.Contains(toggleNameInConfig))
                throw new ToggleConfigurationError(string.Format("The key '{0}' was not found in AppSettings",
                                                                     toggleNameInConfig));

            return ConfigurationManager.AppSettings[toggleNameInConfig];
        }
    }
}


#endif