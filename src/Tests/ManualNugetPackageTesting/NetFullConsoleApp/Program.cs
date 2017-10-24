using System;

namespace NetFullConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var p = new Printing();
            var s = new Saving();


            Console.WriteLine($"Printing is {(p.FeatureEnabled ? "on" : "off")}");
            Console.WriteLine($"Saving is {(s.FeatureEnabled ? "on" : "off")}");

            

            Console.ReadLine();
        }
    }
}