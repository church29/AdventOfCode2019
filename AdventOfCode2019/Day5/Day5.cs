using System;
using System.Linq;
using static System.Environment;

namespace AdventOfCode2019.Day5 {
    public class Day5 {
        public static void Day5Problem() {
            var filePath = CurrentDirectory.ToString() + "/Day5/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var intCodes = lines.Select(line => line.Split(",").Select(int.Parse).ToList());

            Console.WriteLine("Day 5: Problem 1: ");
            foreach (var intCode in intCodes) {
                Day2.IntcodeComputer.processIntCode(intCode, 0, 1);
            }

            Console.WriteLine("Day 5: Problem 2: ");
            foreach (var intCode in intCodes) {
                Day2.IntcodeComputer.processIntCode(intCode, 0, 5);
            }

        }
        
    }
}
