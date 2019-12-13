using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using static System.Environment;
using static System.Linq.Enumerable;

namespace AdventOfCode2019.Day12 {
    public class MoonEnergy {


        public static void Day12() {
            var filePath = CurrentDirectory.ToString() + "/Day12/resources/test2.txt";
            var lines = System.IO.File.ReadAllLines(filePath);

            var coordinates = GetCoordinates(lines.ToList());
            var velocities = coordinates.Select(coordinate => new List<int> { 0, 0, 0 }).ToList();
            RunSimulation(coordinates, velocities, 10);

            coordinates = GetCoordinates(lines.ToList());
            velocities = coordinates.Select(coordinate => new List<int> { 0, 0, 0 }).ToList();

            RunSimulationIndefinitely(coordinates, velocities);

        }


        public static void RunSimulation(List<List<int>> coordinates, List<List<int>> velocities, int steps) {
            foreach (var step in Enumerable.Range(0, steps)) {
                velocities = CalculateVelocities(coordinates, velocities);
                for (var i = 0; i < coordinates.Count; i++) {
                    var moon = coordinates[i];
                    var velocity = velocities[i];
                    moon[0] += velocity[0];
                    moon[1] += velocity[1];
                    moon[2] += velocity[2];
                }
            }

            var potentialEnergy = coordinates.Select(coordinate => Math.Abs(coordinate[0]) + Math.Abs(coordinate[1]) + Math.Abs(coordinate[2])).ToList();
            var kineticyEnergy = velocities.Select(coordinate => Math.Abs(coordinate[0]) + Math.Abs(coordinate[1]) + Math.Abs(coordinate[2])).ToList();

            var totalEnergy = 0;
            for (var i = 0; i < potentialEnergy.Count; i++) {
                totalEnergy += potentialEnergy[i] * kineticyEnergy[i];
            }

            Console.WriteLine("Total Energy: " + totalEnergy);

        }

        public static void RunSimulationIndefinitely(List<List<int>> coordinates, List<List<int>> velocities) {
            var steps = new Dictionary<string, List<string>>();
            var lastStep = ("", "");
            long count = 0;
            List<string> velocityHashes;
            while (true) {
                var coordinateString = string.Join(",", coordinates.Select(coordinate => "" + coordinate[0] + coordinate[1] + coordinate[2]).ToList());
                var velocityString = string.Join(",", velocities.Select(coordinate => "" + coordinate[0] + coordinate[1] + coordinate[2]).ToList());
                if (velocityString.Replace("0", "").Length == 0) {
                    break;
                }
                //if (lastStep.Item1 == coordinateString && lastStep.Item2 == velocityString) {
                //    break;
                //}

                lastStep = (coordinateString, velocityString);


                
                if (steps.TryGetValue(coordinateString, out velocityHashes)) {
                    if (velocityHashes.Contains(velocityString)) {
                        break;
                    }
                    steps[coordinateString].Add(velocityString);

                } else {
                    steps.Add(coordinateString, new List<string> { velocityString });
                }
                




                velocities = CalculateVelocities(coordinates, velocities);
                for (var i = 0; i < coordinates.Count; i++) {
                    var moon = coordinates[i];
                    var velocity = velocities[i];
                    moon[0] += velocity[0];
                    moon[1] += velocity[1];
                    moon[2] += velocity[2];
                }
                count++;

            }

            Console.WriteLine("Total Steps: " + count);



        }


        public static List<List<int>> CalculateVelocities(List<List<int>> coordinates, List<List<int>> velocities) {
            for (var i = 0; i < coordinates.Count; i++) {
                var moon = coordinates[i];
                foreach (var coordinate in coordinates) {
                    if (moon[0] != coordinate[0]) {
                        velocities[i][0] += moon[0] < coordinate[0] ? 1 : -1;
                    }
                    if (moon[1] != coordinate[1]) {
                        velocities[i][1] += moon[1] < coordinate[1] ? 1 : -1;
                    }
                    if (moon[2] != coordinate[2]) {
                        velocities[i][2] += moon[2] < coordinate[2] ? 1 : -1;
                    }

                }

            }

            return velocities;

        }

        public static List<List<int>> GetPermutations(List<int> list, int length) {
            if (length == 1) {
                return list.Select(p => new List<int>() { p }).ToList();
            }
            return GetPermutations(list, length - 1)
                .SelectMany(
                    permutation => list.Where(phase => !permutation.Contains(phase)),
                    (t1, t2) => t1.Concat(new List<int>() { t2 }).ToList()
                ).ToList();
        }

        public static List<List<int>> GetCoordinates(List<string> input) {
            var coordinates = new List<List<int>>();

            foreach (var moon in input) {
                var x = int.Parse(moon.Split("x=").Last().Split(",").First());
                var y = int.Parse(moon.Split("y=").Last().Split(",").First());
                var z = int.Parse(moon.Split("z=").Last().Split(">").First());
                coordinates.Add(new List<int> { x, y, z });

            }

            return coordinates;
        }
    }
}
