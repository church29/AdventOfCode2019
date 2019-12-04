using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;


namespace AdventOfCode2019.Day3
{
    public class ManhattanWires
    {

        public static void Day3()
        {
            var filePath = CurrentDirectory.ToString() + "/Day3/resources/input.txt";
            var instructions = System.IO.File.ReadAllLines(filePath);
            var distance = GetDistance(instructions.First(), instructions.Last());
            Console.WriteLine("Day 3: Problem 1 Answer: " + distance);
        }


        public static int GetDistance(string wireAInstructions, string wireBInstructions)
        {

            var wireAPath = GetPath(wireAInstructions.Split(",").ToList());
            var wireBPath = GetPath(wireBInstructions.Split(",").ToList());
            var intersections = GetIntersections(wireAPath, wireBPath);

            var distance = intersections.Select(coordinate => Math.Abs(coordinate.Item1) + Math.Abs(coordinate.Item2)).Min();

            return distance;
        }

        public static List<(int, int)> GetIntersections(List<(int, int)> wireAPath, List<(int, int)> wireBPath)
        {
            var intersections = new List<(int, int)>();
            foreach (var coordinate in wireAPath)
            {
                if (wireBPath.Contains(coordinate))
                {
                    if (!coordinate.Equals((0, 0)))
                    {
                        intersections.Add(coordinate);
                    }

                }
            }

            return intersections;

        }



        public static List<(int, int)> GetPath(List<string> wireInstructions)
        {
            var path = new List<(int, int)>();
            path.Add((0, 0));
            foreach (var instruction in wireInstructions)
            {
                char direction = instruction[0];
                var distance = int.Parse(String.Join("", instruction.Skip(1).ToArray()));

               

                var line = GetLine(path.Last(), direction, distance);
              
                path.AddRange(line);

            }

            return path;
        }


        public static List<(int, int)> GetLine((int, int) position, char direction, int distance)
        {
            var line = new List<(int, int)>();
            var range = Enumerable.Range(0, distance + 1);
            
            switch (direction)
            {
                case 'U':
                    foreach (int y in range)
                    {
                        line.Add((position.Item1, position.Item2 + y));
                    }
                    break;
                case 'D':
                    foreach (int y in range)
                    {
                        line.Add((position.Item1, position.Item2 - y));
                    }
                    break;
                case 'R':
                    foreach (int x in range)
                    {
                        line.Add((position.Item1 + x, position.Item2));
                    }
                    break;
                case 'L':
                    foreach (int x in range)
                    {
                        line.Add((position.Item1 - x, position.Item2));
                    }
                    break;

            }

            return line;

        }
    }
}
