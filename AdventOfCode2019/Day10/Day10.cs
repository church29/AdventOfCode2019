using System;

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Environment;

namespace AdventOfCode2019.Day10 {
    public class MonitoringStation {
        // 210 to low
        public static void Day10() {
            var filePath = CurrentDirectory.ToString() + "/Day10/resources/input2.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var astroidMap = GetAstroidMap(lines.ToList(), lines.First().Count(), lines.Count());
            var optimalPosition = GetOptimalPosition(astroidMap);

            Console.WriteLine("Day 10: Problem 1: " + optimalPosition);
            var point = (0, 0);

            var points = Problem2(astroidMap, optimalPosition, lines.First().Count(), lines.Count());
            

            Console.WriteLine("Day 10: Problem 2: " + points[199]);
        }


        public static Dictionary<(int, int), Boolean> GetAstroidMap(
                List<string> instructions,
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

        public static List<(int, int)> Problem2(Dictionary<(int, int), Boolean> astroidMap, (int, int) station, int width, int height) {
            var availableAstroids = astroidMap.Where(entry => entry.Value == true && entry.Key != station).Select(entry => entry.Key).ToList();
            var destroyedAstroids = new List<(int, int)>();
            var outerPoints = GetAllPoints(station, width, height);
            var remainingOuterPoints = new List<(int, int)>(outerPoints);
            while (availableAstroids.Count > 0 && outerPoints.Count != 0) {
                foreach (var outerPoint in outerPoints) {
                    var positions = GetPositionsOnGridFromLine(station, outerPoint);
                    positions.Add(outerPoint);

                    var collisions = positions.Where(position => availableAstroids.Contains(position)).ToList();
                    if (collisions.Count > 0) {
                        var collision = collisions.First();
                        destroyedAstroids.Add(collision);
                        Console.WriteLine("Destroying Astroid # " + destroyedAstroids.Count + " : " + collision);
                        availableAstroids.Remove(collision);
                    } else {
                        remainingOuterPoints.Remove(outerPoint);
                    }

                }
                outerPoints = new List<(int, int)>(remainingOuterPoints);
            }

            return destroyedAstroids;
        }

        public static (int, int) GetVector((int, int) origin, (int, int) point) {
            return GetSmallestVector(point.Item1 - origin.Item1, point.Item2 - origin.Item2);
        }

        public static double GetAngleFromVector((int, int) vector) {
            return 90 + (Math.Atan2(vector.Item2, vector.Item1) * (180 / Math.PI));
        }

        public static List<(int, int)> SortPointsByAngle(List<(int, int)> points, (int, int) origin) {
            var pointsByAngle = new SortedDictionary<double, (int, int)>();


            foreach (var point in points) {
                var vector = GetVector(origin, point);
                var angle = GetAngleFromVector(vector);
                if (angle < 0) {
                    angle += 360;
                }
                if (!pointsByAngle.ContainsKey(angle)) {
                    pointsByAngle.Add(angle, point);
                } else {
                    var newMagnitude = (Math.Sqrt(Math.Pow(point.Item1 - origin.Item1, 2) + Math.Pow(point.Item2 - origin.Item2, 2)));
                    var oldPoint = pointsByAngle[angle];
                    var oldMagnitude = (Math.Sqrt(Math.Pow(oldPoint.Item1 - origin.Item1, 2) + Math.Pow(oldPoint.Item2 - origin.Item2, 2)));
                    if (newMagnitude > oldMagnitude) {
                        pointsByAngle[angle] = point;
                    }
                }

            }
                       
            return pointsByAngle.Values.ToList();

        }

        
        public static List<(int, int)> GetAllPoints((int, int) station, int width, int height) {
            var outerPoints = new HashSet<(int, int)>();

            for (var y = 0; y < height; y++) {
                for (var x = 0; x < width; x++) {
                    if ((x, y) != station) {
                        outerPoints.Add((x, y));
                    }
                }
            }

            return SortPointsByAngle(outerPoints.ToList(), station);

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

            var x = position.Item1;
            var y = position.Item2;

            var positions = GetPositionsOnGridFromLine(position, target);
            return astroidLocations.Where(location => positions.Contains(location)).Count() == 0;

        }


        public static List<(int, int)> GetPositionsOnGridFromLine((int, int) position, (int, int) target) {
            var positions = new List<(int, int)>();

            var smallestVector = GetVector(position, target);


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
