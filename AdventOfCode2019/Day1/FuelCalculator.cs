using System;
using System.Linq;
using static System.Environment;
using static System.Math;

namespace AdventOfCode2019.Day1 {
    public class FuelCalculator {

        public static void Day1() {
            var filePath = CurrentDirectory.ToString() + "/Day1/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var fuels = lines.Select(line => GetFuel(float.Parse(line))).ToList();
            var sum = fuels.Sum();
            Console.WriteLine("Day 1: Problem 1: " + sum);
            var compoundedFuels = lines.Select(line => GetCompoundedFuel(float.Parse(line)));
            Console.WriteLine("Day 1: Problem 2: " + compoundedFuels.Sum());
        }

        public static int GetFuel(float mass) => (int)(Floor(mass / 3) - 2);

        public static int GetCompoundedFuel(float mass) {
            var fuel = GetFuel(mass);
            var previousFuel = fuel;
            while (GetFuel(previousFuel) > 0) {
                previousFuel = GetFuel(previousFuel);
                fuel += previousFuel;
            }

            return fuel;
        }
    }
}