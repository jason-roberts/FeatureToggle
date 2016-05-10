using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.SampleFeatureToggles;
using FeatureToggle.Core.Fluent;

namespace ConsoleApplication1
{
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
