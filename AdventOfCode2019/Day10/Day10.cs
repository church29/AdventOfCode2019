using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;

namespace AdventOfCode2019.Day10 {
    public class MonitoringStation {
        public static void Day10() {
            var filePath = CurrentDirectory.ToString() + "/Day10/resources/input2.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var astroidMap = GetAstroidMap(lines.ToList(), 0, 0, lines.First().Count(), lines.Count());
            var optimalPosition = GetOptimalPosition(astroidMap);

            Console.WriteLine("Day 10: Problem 1: " + optimalPosition);
        }


        public static Dictionary<(int, int), Boolean> GetAstroidMap(
                List<string> instructions,
                int yStart = 0,
                int xStart = 0,
                int width = 0,
                int height = 0

            ) {
            var astroidMap = new Dictionary<(int, int), Boolean>();
            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    astroidMap.Add((x, y), instructions[y][x] == '#');

                }
            }

            return astroidMap;


        }

        public static (int, int) GetOptimalPosition(Dictionary<(int, int), Boolean> astroidMap) {
            var availableLocations = astroidMap.Where(entry => entry.Value == true).Select(entry => entry.Key).ToList();
            var positionMap = new Dictionary<(int, int), int>();
            foreach (var location in availableLocations) {
                positionMap.Add(
                    location,
                    availableLocations.Where(target => CanSeePosition(availableLocations, location, target)).Count());
            }
            Console.WriteLine("Day 10: Problem 1: " + positionMap.Values.Max());
            return positionMap.Where(entry => entry.Value == positionMap.Values.Max()).First().Key;


        }

        public static Boolean CanSeePosition(List<(int, int)> astroidLocations, (int, int) position, (int, int) target) {
            if (position == target) {
                return false;
            }

            // horizontal adjacent
            if (position.Item2 == target.Item2 && (position.Item1 == target.Item1 + 1 || position.Item1 == target.Item1 - 1)) {
                return true;
            }

            // vertical adjacent
            if (position.Item1 == target.Item1 && (position.Item2 == target.Item2 + 1 || position.Item2 == target.Item2 - 1)) {
                return true;
            }

            var lineOfSight = false;
            var x = position.Item1;
            var y = position.Item2;


            var positions = GetPositionsOnGridFromLine(position, target);
            return astroidLocations.Where(location => positions.Contains(location)).Count() == 0;







        }
        // 0,0  2,2 = 1, (1,1)
        public static List<(int, int)> GetPositionsOnGridFromLine((int, int) position, (int, int) target) {
            var positions = new List<(int, int)>();
            var xDir = target.Item1 - position.Item1;
            var yDir = target.Item2 - position.Item2;
            var smallestVector = GetSmallestVector(xDir, yDir);

            Console.WriteLine("Xdir: " + xDir + " yDir: " + yDir + " smallest vector: " + smallestVector);

            var x = position.Item1;

            var y = position.Item2;

            while (x != target.Item1 || y != target.Item2) {
                x += smallestVector.Item1;
                y += smallestVector.Item2;
                if ((x, y) != target) {
                             
                    positions.Add((x, y));
                }


            }
            

            return positions;

        }

        public static (int, int) GetSmallestVector(int xDelta, int yDelta) {
            var smaller = xDelta == 0 || (yDelta != 0 && xDelta > yDelta) ? Math.Abs(yDelta) : Math.Abs(xDelta);
            var range = Enumerable.Range(1, smaller + 1).Reverse();
            

            foreach (var i in range) {
                if (xDelta % i == 0 && yDelta % i == 0) {
                    return (xDelta / i, yDelta / i);

                }



            }

            return (xDelta, yDelta);

        }

    }

}
