#if NETFULL // sql toggle not currently supported in code

using FeatureToggle;
using FeatureToggle.Internal;
using Shouldly;
using Xunit;

namespace FeatureToggle.Shared.Tests.Integration
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


        [Fact]
        public void ErrorWhenConnectionStringIsNotConfigured()
        {
            var sut = new MissingConnectionStringSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The connection string was not configured in <appSettings> with a key of 'FeatureToggle.MissingConnectionStringSqlServerToggle.ConnectionString' or 'FeatureToggle.MissingConnectionStringSqlServerToggle.ConnectionStringName' nor in <connectionStrings> with a name of 'FeatureToggle.MissingConnectionStringSqlServerToggle'.");
        }


        [Fact]
        public void ErrorWhenAppSettingsConnectionStringValueIsEmpty()
        {
            var sut = new EmptyAppSettingsConnectionStringValueSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The <appSettings> value for key 'FeatureToggle.EmptyAppSettingsConnectionStringValueSqlServerToggle.ConnectionString' is empty.");
        }


        [Fact]
        public void ErrorWhenAppSettingsConnectionStringNameValueIsEmpty()
        {
            var sut = new EmptyAppSettingsConnectionStringNameValueSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The <appSettings> value for key 'FeatureToggle.EmptyAppSettingsConnectionStringNameValueSqlServerToggle.ConnectionStringName' is empty.");
        }


        [Fact]
        public void ErrorWhenConnectionStringsConnectionStringIsEmpty()
        {
            var sut = new EmptyConnectionStringsConnectionStringValueSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The <connectionStrings> value for connection named 'FeatureToggle.EmptyConnectionStringsConnectionStringValueSqlServerToggle' is empty.");
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

            ex.Message.ShouldBe("The connection string for 'FeatureToggle.DuplicatedConfigSqlServerToggle' is configured multiple times.");
        }


        [Fact]
        public void ErrorWhenConnectionStringNameConfiguredInAppSettingsButNoEntryInConnectionStrings()
        {
            var sut = new NameDoesNotExistInConnectionStringsSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("No entry named 'MissingName' exists in <connectionStrings>.");
        }


        [Fact]
        public void ErrorWhenEntryInConnectionStringsButConnectionStringIsWhitespace()
        {
            var sut = new ConnectionStringConfiguredByNameSqlServerToggle();

            var ex = Assert.Throws<ToggleConfigurationError>(() => sut.FeatureEnabled);

            ex.Message.ShouldBe("The <connectionStrings> value for connection named 'EmptyConnectionString' is empty.");
        }


        private class MySqlServerToggleTrue : SqlFeatureToggle { }
        private class MySqlServerToggleFalse : SqlFeatureToggle { }

        private class MissingConnectionStringSqlServerToggle : SqlFeatureToggle { }
        private class MissingAppSettingsSqlStatementKeySqlServerToggle : SqlFeatureToggle { }

        private class EmptyAppSettingsConnectionStringValueSqlServerToggle : SqlFeatureToggle { }
        private class EmptyAppSettingsConnectionStringNameValueSqlServerToggle : SqlFeatureToggle { }
        private class EmptyConnectionStringsConnectionStringValueSqlServerToggle : SqlFeatureToggle { }
        private class EmptyAppSettingsSqlStatementValueSqlServerToggle : SqlFeatureToggle { }

        private class DuplicatedConfigSqlServerToggle : SqlFeatureToggle { }

        private class NameDoesNotExistInConnectionStringsSqlServerToggle : SqlFeatureToggle { }

        private class ConnectionStringConfiguredByNameSqlServerToggle : SqlFeatureToggle { }
    }
}

#endif