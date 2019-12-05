using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;

namespace AdventOfCode2019.Day2 {
    public class IntcodeComputer {

        public static void Day2() {
            var filePath = CurrentDirectory.ToString() + "/Day2/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var intCode = lines[0].Split(",").Select(int.Parse).ToList();
            var newIntCode = new List<int>(intCode);
            newIntCode[1] = 12;
            newIntCode[2] = 2;
            var processedIntCode = processIntCode(newIntCode, 0);
            Console.WriteLine("Day 2, Problem 1: " + processedIntCode[0]);

            foreach (var noun in Enumerable.Range(0, 99)) {
                foreach (var verb in Enumerable.Range(0, 99)) {
                    newIntCode = new List<int>(intCode);
                    newIntCode[1] = noun;
                    newIntCode[2] = verb;
                    var result = processIntCode(newIntCode, 0)[0];
                    if (result == 19690720) {

                        Console.WriteLine(
                            "Day 2, Problem 2:" +
                            " Noun: " + noun +
                            " Verb: " + verb +
                            " Result: " + result +
                            " Answer: " + ((100 * noun) + verb));

                    }
                }

            }
        }

        public static int INPUT = 5;

        public static List<int> processIntCode(List<int> intCode, int startingPosition) {
            var instructionCode = intCode[startingPosition];
            var opCode = int.Parse(instructionCode.ToString()[^1].ToString());
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

            if (opCode == 9) {
                return intCode;
            }

            var value1Address = intCode[startingPosition + 1];
            var value1 = param1Mode == 0 ? intCode[value1Address] : value1Address;

            if (opCode == 3) {
                intCode[value1Address] = INPUT;
                Console.WriteLine("INPUT: opCode 3 Input " + INPUT + " at : " + value1Address + " instructionCode: " + instructionCode + "\n");
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode));
            } else if (opCode == 4) {
                Console.WriteLine("OUTPUT: opCode 4 Output at value1Address: " + value1 + " instructionCode: " + instructionCode + "\n");
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode));
            }

            var value2Address = intCode[startingPosition + 2];
            var value2 = param2Mode == 0 ? intCode[value2Address] : value2Address;

            if (opCode == 5) {
                if (value1 != 0) {
                    // Console.WriteLine("opCode 5 Jump To at value1Address: " + value1 + " instructionCode: " + instructionCode + "\n");
                    return processIntCode(intCode, value2);
                }
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode));
            }

            if (opCode == 6) {
                if (value1 == 0) {
                    // Console.WriteLine("opCode 6 Jump To at value1Address: " + value1 + " instructionCode: " + instructionCode + "\n");
                    return processIntCode(intCode, value2);
                }
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode));
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
            
            return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode));
        }

        static int getIncrementFromOpCode(int opCode) {
            switch (opCode) {
                case 3:
                case 4:
                return 2;
                case 5:
                case 6:
                return 3;
            }

            return 4;
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
