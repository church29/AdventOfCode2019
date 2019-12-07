using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;

namespace AdventOfCode2019.Day7 {
    public class MultiInputIntCodeComputer {
        public static void Day7() {
            var filePath = CurrentDirectory.ToString() + "/Day7/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var intcode = lines.First().Split(",").Select(int.Parse).ToList();
            
            var permutations = GetPermutations(new List<int>() { 0,1,2,3,4}, 5);
            // var permutations = System.IO.File.ReadAllLines(CurrentDirectory.ToString() + "/Day7/resources/permutations.txt");
                        
            var maxes = new List<int>();
            var intCodes = new Dictionary<int, List<int>>();
            foreach (var permutation in permutations) {
                maxes.Add(getMaxThrusterAtPhaseSetting(
                    new List<int>(intcode),
                    permutation,
                    new List<int>() { 0, 0, 0, 0, 0 },
                    intCodes
                    ));
            }

            Console.WriteLine("Day 7: Problem 1: " + maxes.Max());

            permutations = GetPermutations(new List<int>() { 5, 6, 7, 8, 9 }, 5);
            // permutations = System.IO.File.ReadAllLines(CurrentDirectory.ToString() + "/Day7/resources/problem2.txt");
            
            maxes = new List<int>();
            intCodes = new Dictionary<int, List<int>>();
            foreach (var permutation in permutations) {
                maxes.Add(getMaxThrusterAtPhaseSetting(
                    new List<int>(intcode),
                    permutation,
                    new List<int>() { 0, 0, 0, 0, 0 },
                    intCodes
                    ));
            }

            Console.WriteLine("Day 7: Problem 2: " + maxes.Max());
        }

        public static List<List<int>> GetPermutations(List<int> list, int length) {
            if (length == 1) {
                return list.Select(p => new List<int>() { p }).ToList();
            }
            return GetPermutations(list, length - 1)
                .SelectMany(
                    permutation => list.Where(phase => !permutation.Contains(phase)),
                    (t1, t2) => t1.Concat(new List<int>(){ t2 }).ToList()
                ).ToList();
        }
               
        public static int getMaxThrusterAtPhaseSetting(
            List<int> intCode,
            List<int> phaseSetting,
            List<int> startingPositions,
            Dictionary<int, List<int>> intCodes,
            int input = 0,
            Boolean initialRun = true

        ) {

            foreach (var phase in phaseSetting) {

                var inputs = new List<int>();
                if (initialRun) {
                    inputs.Add(phase);
                }

                inputs.Add(input);

                List<int> phaseCode = new List<int>();
                var results = new List<int>();
                if (intCodes.TryGetValue(phase, out phaseCode)) {

                    results = processIntCode(phaseCode, startingPositions[phaseSetting.IndexOf(phase)], inputs);

                } else {
                    phaseCode = new List<int>(intCode);
                    results = processIntCode(phaseCode, startingPositions[phaseSetting.IndexOf(phase)], inputs);

                }

                intCodes[phase] = phaseCode;
                if (results.Count() == 0) {
                    return input;

                }

                input = results.First();
                startingPositions[phaseSetting.IndexOf(phase)] = results.Last();

            }
                       
            return getMaxThrusterAtPhaseSetting(
                new List<int>(intCode),
                phaseSetting,
                startingPositions,
                intCodes,
                input,
                false
             );
        }
        
        public static List<int> processIntCode(List<int> intCode, int startingPosition, List<int> inputs) {
            var instructionCode = intCode[startingPosition];
            var opCode = instructionCode % 100;
            var param1Mode = instructionCode.ToString().Length >= 3 ? int.Parse(instructionCode.ToString()[^3].ToString()) : 0;
            var param2Mode = instructionCode.ToString().Length >= 4 ? int.Parse(instructionCode.ToString()[^4].ToString()) : 0;
            var param3Mode = instructionCode.ToString().Length >= 5 ? int.Parse(instructionCode.ToString()[^5].ToString()) : 0;
             
            if (opCode == 99) {
                var results = new List<int>();
                return results;
            }

            var value1Address = intCode[startingPosition + 1];
            var value1 = param1Mode == 0 ? intCode[value1Address] : value1Address;

            if (opCode == 3) {
                intCode[value1Address] = inputs.First();
                inputs = inputs.Skip(1).ToList();
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode), inputs);
            } else if (opCode == 4) {
                var results = new List<int>() { value1, (startingPosition + getIncrementFromOpCode(opCode)) };
                return results;

            }

            var value2Address = intCode[startingPosition + 2];
            var value2 = param2Mode == 0 ? intCode[value2Address] : value2Address;

            if (opCode == 5) {
                return processIntCode(intCode, value1 != 0 ? value2 : (startingPosition + getIncrementFromOpCode(opCode)), inputs);
            }

            if (opCode == 6) {
                return processIntCode(intCode, value1 == 0 ? value2 : (startingPosition + getIncrementFromOpCode(opCode)), inputs);
            }

            var resultAddress = intCode[startingPosition + 3];
            var result = processOperation(opCode, value1, value2);
            intCode[resultAddress] = result;

            return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode), inputs);
        }

        static int getIncrementFromOpCode(int opCode) {
            switch (opCode) {
                case 3:
                case 4:
                return 2;

                case 5:
                case 6:
                return 3;
                default:
                return 4;
            }
        }

        static int processOperation(int opCode, int value1, int value2) {

            if (opCode == 1) {
                return value1 + value2;
            }

            if (opCode == 7) {
                return value1 < value2 ? 1 : 0;
            }

            if (opCode == 8) {
                return value1 == value2 ? 1 : 0;
            }


            return value1 * value2;

        }
    }
}

