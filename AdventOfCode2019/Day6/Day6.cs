using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;


namespace AdventOfCode2019.Day6 {
    public class OrbitalSanta {
        public static void Day6Problem() {
            var filePath = CurrentDirectory.ToString() + "/Day5/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
           

            Console.WriteLine("Day 6: Problem 1: ");
            CalculateNumOrbits(lines.ToList());
          
        }

        public static int CalculateNumOrbits(List<string> orbitList) {
            var orbitMap = GetOrbitMap(orbitList);
            var numOrbits = 0;
            foreach(var planet in orbitMap.Keys) {
                numOrbits += CalculateNumOrbitsForPlanet(orbitMap, planet);

            }

            return numOrbits;
        }

        public static Dictionary<string, string> GetOrbitMap(List<string> orbitList) {
            var orbitMap = new Dictionary<string, string>();
            foreach(var orbit in orbitList) {
                var orbits = orbit.Split(")");
                orbitMap.Add(orbits[1], orbits[0]);
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
    }
}
