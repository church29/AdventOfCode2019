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

            var permutations = System.IO.File.ReadAllLines(CurrentDirectory.ToString() + "/Day7/resources/permutations.txt");


            var maxes = new List<int>();

            foreach (var permutation in permutations) {
                maxes.Add(getMaxThrusterAtPhaseSetting(new List<int>(intcode), permutation.Select(Char.ToString).Select(int.Parse).ToList()));

            }

            Console.WriteLine("Day 7: Problem 1: " + maxes.Max());

        }
          
                            
        public static int getMaxThrusterAtPhaseSetting(
            List<int> intCode,
            List<int> phaseSetting,
            Boolean feedbackMode = false,
            int input = 0
        ) {
                                                       
                       
            foreach (var phase in phaseSetting) {
                                
                var inputs = new List<int>() {
                     phase,
                     input,
                 };
                var results = processIntCode(new List<int>(intCode), 0, inputs);

                if (results.Count() == 0) {
                    return input;

                }

                input = results.First();
                

            }

            if (!feedbackMode) {
                return input;
            }
                                 

            return getMaxThrusterAtPhaseSetting(new List<int>(intCode), phaseSetting, feedbackMode, input);

        }


        public static List<int> processIntCode(List<int> intCode, int startingPosition, List<int> inputs) {
            var instructionCode = intCode[startingPosition];
            var opCode = instructionCode % 100;
            var param1Mode = instructionCode.ToString().Length >= 3 ? int.Parse(instructionCode.ToString()[^3].ToString()) : 0;
            var param2Mode = instructionCode.ToString().Length >= 4 ? int.Parse(instructionCode.ToString()[^4].ToString()) : 0;
            var param3Mode = instructionCode.ToString().Length >= 5 ? int.Parse(instructionCode.ToString()[^5].ToString()) : 0;

            /*
            Console.WriteLine(
                "instructionCode: " + instructionCode +
                " opCode " + opCode +
                " param1Mode: " + param1Mode +
                " param2Mode: " + param2Mode +
                " param3Mode: " + param3Mode
            );
            */

            if (opCode == 99) {
                var results = new List<int>();
                return results;
            }

            var value1Address = intCode[startingPosition + 1];
            var value1 = param1Mode == 0 ? intCode[value1Address] : value1Address;

            if (opCode == 3) {
                intCode[value1Address] = inputs.First();
                inputs = inputs.Skip(1).ToList();
                Console.WriteLine("INPUT: opCode 3 Input " + inputs + " at : " + value1Address + " instructionCode: " + instructionCode + "\n");
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode), inputs);
            } else if (opCode == 4) {
                Console.WriteLine("OUTPUT: opCode 4 Output at value1Address: " + value1 + " instructionCode: " + instructionCode + "\n");
                var results = new List<int>() { value1 };
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

            /*
            Console.WriteLine(
                "value1Address: " + value1Address +
                " value1: " + value1 +
                " value2Address: " + value2Address +
                " value2: " + value2 +
                " resultAddress: " + resultAddress +
                " result: " + result);
            */

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

