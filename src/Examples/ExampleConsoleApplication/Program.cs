using System;
using FeatureToggle;

namespace ExampleConsoleApplication
{
    // Define a new toggle
    // SimpleFeatureToggle get it's value from appSettings by default.
    //
    // <appSettings>
    //  <add key="FeatureToggle.ShowMessageToggle" value="true"/>
    //</appSettings>
    // Note the "FeatureToggle." prefix on the key - this is required
    class ShowMessageToggle : SimpleFeatureToggle
    {
    }

    class Program
    {
        static void Main(string[] args)
        {
            var toggle = new ShowMessageToggle();

            if (toggle.FeatureEnabled)
            {
                Console.WriteLine("This feature is enabled");
            }
            else
            {
                // to disable the feature:
                //  <add key="FeatureToggle.ShowMessageToggle" value="false"/>
                Console.WriteLine("This feature is disabled");
            }

            Console.ReadLine();
        }
    }
}
