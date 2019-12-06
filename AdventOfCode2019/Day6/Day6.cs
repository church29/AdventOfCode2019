using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;


namespace AdventOfCode2019.Day6 {
    public class OrbitalSanta {
        public static void Day6() {
            var filePath = CurrentDirectory.ToString() + "/Day6/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);

            var numOrbits = CalculateNumOrbits(lines.ToList());
            Console.WriteLine("Day 6: Problem 1: " + numOrbits);
            var numJumpsToSanta = GetNumJumpsToSanta(lines.ToList());
            Console.WriteLine("Day 6: Problem 2: " + numJumpsToSanta);

        }

        public static int CalculateNumOrbits(List<string> orbitList) {
            var orbitMap = GetOrbitMap(orbitList);
            var numOrbits = 0;
            foreach (var planet in orbitMap.Keys) {
                numOrbits += CalculateNumOrbitsForPlanet(orbitMap, planet);

            }

            return numOrbits;
        }

        public static Dictionary<string, string> GetOrbitMap(List<string> orbitList) {
            var orbitMap = new Dictionary<string, string>();
            foreach (var orbit in orbitList) {
                var orbits = orbit.Split(")");
                if (orbits.Length > 1) {
                    orbitMap.Add(orbits[1], orbits[0]);
                }

            }

            return orbitMap;
        }

        public static int CalculateNumOrbitsForPlanet(Dictionary<string, string> orbitMap, string planet) {
            if (planet == "COM") {
                return 0;
            }

            string orbitingAround;
            if (orbitMap.TryGetValue(planet, out orbitingAround)) {
                return 1 + CalculateNumOrbitsForPlanet(orbitMap, orbitingAround);
            }

            return 0;

        }

        public static int GetNumJumpsToSanta(List<string> orbitList) {
            var orbitMap = GetOrbitMap(orbitList);
            var orbitListForYou = GetOrbitListForPlanet(orbitMap, "YOU");
            var orbitListForSanta = GetOrbitListForPlanet(orbitMap, "SAN");

            var intersectionOrbits = orbitListForYou.Intersect(orbitListForSanta);
            var firstIntersection = intersectionOrbits.First();
            

            return orbitListForYou.IndexOf(firstIntersection) + orbitListForSanta.IndexOf(firstIntersection);
        }

        public static List<string> GetOrbitListForPlanet(Dictionary<string, string> orbitMap, string planet) {
            var orbitList = new List<String>();
            if (planet == "COM") {
                return orbitList;
            }

            
            string orbitingAround;
            if (orbitMap.TryGetValue(planet, out orbitingAround)) {
                orbitList.Add(orbitingAround);
                orbitList.AddRange(GetOrbitListForPlanet(orbitMap, orbitingAround));
            }

            return orbitList;
        }
    }
}
