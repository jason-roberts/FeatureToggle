using System.Configuration;
using FeatureToggle.Core;
using FeatureToggle.Providers;
using FeatureToggle.Toggles;
using Shouldly;
using Xunit;

namespace FeatureToggle.Integration.Tests
{    
    public class BooleanSqlServerProviderShould
    {
        [Fact]
        [Trait("category", "LocalIntegrationResourcesRequired")]                
        public void ReadBooleanTrueFromSqlServer()
        {            
            var sut = new BooleanSqlServerProvider();

            var toggle = new MySqlServerToggleTrue();

            Assert.True(sut.EvaluateBooleanToggleValue(toggle));
        }


        [Fact]
        [Trait("category", "LocalIntegrationResourcesRequired")]
        public void ReadBooleanFalseFromSqlServer()
        {
            var sut = new BooleanSqlServerProvider();

            var toggle = new MySqlServerToggleFalse();

            Assert.False(sut.EvaluateBooleanToggleValue(toggle));
        }


        // appsettings
        //  - connection string val empty
        //  - sql statement val empty



        [Fact]
        public void ErrorWhenConnectionStringDoesntExistInAppSettingsNorConnectionStringsSections()
        {
            var sut = new MissingConnectionStringSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The connection string was not found in <appSettings> with a key of 'FeatureToggle.MissingConnectionStringSqlServerToggle.ConnectionString' or in <connectionStrings> with a name of 'FeatureToggle.MissingConnectionStringSqlServerToggle'.");
        }


        [Fact]
        public void ErrorWhenAppSettingsConnectionStringValueIsEmpty()
        {
            var sut = new EmptyAppSettingsConnectionStringValueSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The <appSettings> value for key 'FeatureToggle.EmptyAppSettingsConnectionStringValueSqlServerToggle.ConnectionString' is empty.");
        }


        [Fact]
        public void ErrorWhenConnectionStringsConnectionStringIsEmpty()
        {
            var sut = new EmptyConnectionStringsConnectionStringValueSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The <connectionStrings> value for connected named 'FeatureToggle.EmptyConnectionStringsConnectionStringValueSqlServerToggle' is empty.");
        }


        [Fact]
        public void ErrorWhenAppSettingsSqlStatementKeyNotInConfig()
        {
            var sut = new MissingAppSettingsSqlStatementKeySqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The appSettings key 'FeatureToggle.MissingAppSettingsSqlStatementKeySqlServerToggle.SqlStatement' was not found.");
        }


        [Fact]
        public void ErrorWhenAppSettingsSqlStatementIsEmpty()
        {
            var sut = new EmptyAppSettingsSqlStatementValueSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The <appSettings> value for key 'FeatureToggle.EmptyAppSettingsSqlStatementValueSqlServerToggle.SqlStatement' is empty.");
        }


        [Fact]
        public void ErrorWhenConnectionStringIsSpecifiedInBothAppSettingsAndConnectionStrings()
        {
            var sut = new DuplicatedConfigSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The connection string for 'FeatureToggle.DuplicatedConfigSqlServerToggle' is specified in both <appSettings> and <connectionStrings>.");
        }


        
   

        private class MySqlServerToggleTrue : SqlFeatureToggle{}
        private class MySqlServerToggleFalse : SqlFeatureToggle { }

        private class MissingConnectionStringSqlServerToggle : SqlFeatureToggle { }
        private class MissingAppSettingsSqlStatementKeySqlServerToggle : SqlFeatureToggle { }

        private class EmptyAppSettingsConnectionStringValueSqlServerToggle : SqlFeatureToggle { }
        private class EmptyConnectionStringsConnectionStringValueSqlServerToggle : SqlFeatureToggle { }
        private class EmptyAppSettingsSqlStatementValueSqlServerToggle : SqlFeatureToggle { }
        
        private class DuplicatedConfigSqlServerToggle : SqlFeatureToggle { }
    }
}
