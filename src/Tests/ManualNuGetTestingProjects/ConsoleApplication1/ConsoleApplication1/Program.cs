using System;
using FeatureToggle.Core;
using FeatureToggle.Core.Fluent;

namespace ConsoleApplication1
{

    class SampleFeatureToggle : AlwaysOnFeatureToggle { }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new SampleFeatureToggle().FeatureEnabled);
            Console.WriteLine(Is<SampleFeatureToggle>.Enabled);

            Console.ReadLine();
        }
    }
}
