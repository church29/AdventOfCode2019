using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Environment;
using static System.Linq.Enumerable;

namespace AdventOfCode2019.Day9 {
    public class BoostIntCodeComputer {
        public static void Day9() {
            var filePath = CurrentDirectory.ToString() + "/Day9/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var intCode = lines.First().Split(",").Select(long.Parse).ToList();

            var output = processIntCode(new List<long>(intCode), 0, new List<long>() { 1 }, new List<long>(), 0);
            Console.WriteLine("Day 9: Problem 1: " + String.Join(",", output));

            output = new List<long>();
            var thread =
                new Thread(
                    _ => processIntCode(new List<long>(intCode), 0, new List<long>() { 2 }, output, 0),
              512000000); // ~ 128MB stack

            thread.Start();
            thread.Join();
            
            Console.WriteLine("Day 9: Problem 2: " + String.Join(",", output));
        }




        public static List<long> processIntCode(List<long> intCode, int startingPosition, List<long> inputs, List<long> output, long relativeBase = 0) {
            if (intCode.Count < 10000) {
                intCode.AddRange(Repeat(0L, 10000).ToList());

            }
            var instructionCode = intCode[startingPosition];
            var opCode = instructionCode % 100;
            var param1Mode = instructionCode.ToString().Length >= 3 ? int.Parse(instructionCode.ToString()[^3].ToString()) : 0;
            var param2Mode = instructionCode.ToString().Length >= 4 ? int.Parse(instructionCode.ToString()[^4].ToString()) : 0;
            var param3Mode = instructionCode.ToString().Length >= 5 ? int.Parse(instructionCode.ToString()[^5].ToString()) : 0;

            if (opCode == 99) {
                return output;
            }

            var value1Address = intCode[startingPosition + 1];
            var value1 = GetValueAtPosition(intCode, startingPosition + 1, param1Mode, relativeBase);

            if (opCode == 3) {
                if (param1Mode == 2) {
                    intCode[(int)(value1Address + relativeBase)] = inputs.First();
                } else {
                    intCode[(int)value1Address] = inputs.First();
                }

                inputs = inputs.Skip(1).ToList();
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode), inputs, output, relativeBase);
            }
            if (opCode == 4) {
                
                output.Add(value1);
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode), inputs, output, relativeBase);

            }
            if (opCode == 9) {
               
                return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode), inputs, output, relativeBase + value1);
            }

            var value2 = GetValueAtPosition(intCode, startingPosition + 2, param2Mode, relativeBase);

            if (opCode == 5) {
                return processIntCode(intCode, (int)(value1 != 0 ? value2 : (startingPosition + getIncrementFromOpCode(opCode))), inputs, output, relativeBase);
            }

            if (opCode == 6) {
                return processIntCode(intCode, (int)(value1 == 0 ? value2 : (startingPosition + getIncrementFromOpCode(opCode))), inputs, output, relativeBase);
            }



            var resultAddress = intCode[startingPosition + 3] + (param3Mode == 2 ? relativeBase : 0);

            var result = processOperation(opCode, value1, value2);
            intCode[(int)resultAddress] = result;

            return processIntCode(intCode, startingPosition + getIncrementFromOpCode(opCode), inputs, output, relativeBase);
        }


        public static long GetValueAtPosition(List<long> intCode, int paramPosition, int paramMode, long relativeBase) {

            var valueAddress = intCode[paramPosition];
            switch (paramMode) {
                case 0:
                return intCode[(int)valueAddress];
                case 1:
                return valueAddress;
                default:

                return intCode[(int)(valueAddress + relativeBase)];


            }
        }

        static int getIncrementFromOpCode(long opCode) {
            switch (opCode) {
                case 3:
                case 4:
                case 9:
                return 2;

                case 5:
                case 6:
                return 3;
                default:
                return 4;
            }
        }

        static long processOperation(long opCode, long value1, long value2) {

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

