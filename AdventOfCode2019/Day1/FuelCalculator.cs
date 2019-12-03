using System;
using System.Linq;
using static System.Environment;
using static System.Math;

namespace AdventOfCodeDay1
{
    public class FuelCalculator
    {

        static void Main(string[] args)
        {

            var filePath = CurrentDirectory.ToString() + "/Day1/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var fuels = lines.Select(line => GetFuel(float.Parse(line))).ToList();
            var sum = fuels.Sum();
            Console.WriteLine("Problem 1 Answer: " + sum);
            var compoundedFuels = lines.Select(line => GetCompoundedFuel(float.Parse(line)));            
            Console.WriteLine("Problem 2 Answer: " + compoundedFuels.Sum());
            

        }


        public static int GetFuel(float mass) => (int)(Floor(mass / 3) - 2);

        public static int GetCompoundedFuel(float mass) {
            var fuel = GetFuel(mass);
            var previousFuel = fuel;
            while (GetFuel(previousFuel) > 0)
            {
                previousFuel = GetFuel(previousFuel);
                fuel += previousFuel;
            }

            return fuel;
       
        }
    }


}