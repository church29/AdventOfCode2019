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
            var filePath = CurrentDirectory.ToString() + "/Day12/resources/input.txt";
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


            var totalEnergy = GetTotalEnergy(coordinates, velocities);

            Console.WriteLine("Day 12: Problem 1: " + totalEnergy);

        }

        public static long GetTotalEnergy(List<List<int>> coordinates, List<List<int>> velocities) {
            var potentialEnergy = coordinates.Select(coordinate => Math.Abs(coordinate[0]) + Math.Abs(coordinate[1]) + Math.Abs(coordinate[2])).ToList();
            var kineticyEnergy = velocities.Select(coordinate => Math.Abs(coordinate[0]) + Math.Abs(coordinate[1]) + Math.Abs(coordinate[2])).ToList();

            var totalEnergy = 0;
            for (var i = 0; i < potentialEnergy.Count; i++) {
                totalEnergy += potentialEnergy[i] * kineticyEnergy[i];
            }

            return totalEnergy;
        }

        public static void RunSimulationIndefinitely(List<List<int>> coordinates, List<List<int>> velocities) {
            var axisCounts = new Dictionary<int, int>();


            var cs = new List<List<int>>(coordinates);
            var vs = new List<List<int>>(velocities);

            var steps = new List<Dictionary<string, string>>();
            var axii = new List<int> { 0, 1, 2 };
            steps.Add(new Dictionary<string, string>());
            steps.Add(new Dictionary<string, string>());
            steps.Add(new Dictionary<string, string>());
           
            while (true) {
                foreach (var axis in axii) {
                    var coordinateString = string.Join(",", cs.Select(coordinate => coordinate[axis]).ToList());
                    var velocityString = string.Join(",", vs.Select(coordinate => coordinate[axis]).ToList());
                    var hash = coordinateString + velocityString;

                    if (steps[axis].Keys.Contains(coordinateString) && steps[axis][coordinateString].Contains(velocityString)) {
                        List<string> values = steps[axis].Values.ToList();
                        axisCounts[axis] = values.Select(vHash => vHash.Split(":").Count()).Sum();
                    } else {
                        if (steps[axis].Keys.Contains(coordinateString)) {
                            steps[axis][coordinateString] += (":" + velocityString);
                        } else {
                            steps[axis][coordinateString] = (velocityString);
                        }
                        
                    }

                }

                axii = axii.Except(axisCounts.Keys).ToList();

                if (axisCounts.Count == 3) {
                    break;
                }



                velocities = CalculateVelocities(cs, vs);
                for (var j = 0; j < cs.Count; j++) {
                    var moon = cs[j];
                    var velocity = vs[j];
                    moon[0] += velocity[0];
                    moon[1] += velocity[1];
                    moon[2] += velocity[2];

                }

            }

            var counts = axisCounts.Values.ToList();
            counts.Sort();
            var xy = GetSmallestVector(counts[0], counts[1]);
            var xyLCM = Math.Min(xy.Item1, xy.Item2) * Math.Max(counts[0], counts[1]) ;
            var xyz = GetSmallestVector(xyLCM, counts[2]);
            long xyzLCM = (long)Math.Max(xyz.Item1, xyz.Item2) * (long)Math.Min(xyLCM, counts[2]);

            Console.WriteLine("Day 12: Problem 2: " + xyzLCM);



        }

        public static (long, long) GetSmallestVector(long xDelta, long yDelta) {
            var smaller = xDelta == 0 || (yDelta != 0 && xDelta > yDelta) ? Math.Abs(yDelta) : Math.Abs(xDelta);
            var range = Enumerable.Range(1, (int)smaller + 1).Reverse();

            foreach (var i in range) {
                if (xDelta % i == 0 && yDelta % i == 0) {
                    return (xDelta / i, yDelta / i);
                }
            }

            return (xDelta, yDelta);
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
