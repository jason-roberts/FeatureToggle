using System;

namespace ConsoleAppDotNetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Printing();
            var s = new Saving();
            var t = new Tweeting();

            Console.WriteLine($"Printing is {(p.FeatureEnabled ? "on" : "off")}");
            Console.WriteLine($"Saving is {(s.FeatureEnabled ? "on" : "off")}");
            Console.WriteLine($"Tweeting is {(t.FeatureEnabled ? "on" : "off")}");
            

            Console.ReadLine();
        }
    }
}