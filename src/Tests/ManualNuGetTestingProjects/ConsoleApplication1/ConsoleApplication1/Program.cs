using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApplication1.SampleFeatureToggles;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var x = new SampleFeatureToggle().FeatureEnabled;

            Console.ReadLine();
        }
    }
}
