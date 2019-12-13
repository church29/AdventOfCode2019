using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using static System.Environment;
using static System.Linq.Enumerable;

namespace AdventOfCode2019.Day11 {
    public class HullPainter {
        public static void Day11() {
            var filePath = CurrentDirectory.ToString() + "/Day11/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var intCode = lines.First().Split(",").Select(long.Parse).ToList();
            var hull = new Dictionary<(int, int), List<long>>();

            hull = PaintHull(hull, new List<long>(intCode));
            Console.WriteLine("Day 11: Problem 1: " + hull.Keys.Count);
            PrintHull(hull);

            hull = new Dictionary<(int, int), List<long>>();
            hull.Add((0, 0), new List<long>() { 1L });
            hull = PaintHull(hull, new List<long>(intCode));
            Console.WriteLine("Day 11: Problem 2: ");
            PrintHull(hull);

        }

        public static void PrintHull(Dictionary<(int,int), List<long>> paintedHull) {
            var hullXCoordinates = paintedHull.Keys.Select(point => point.Item1).ToList();
            var hullWidth = hullXCoordinates.Max() - hullXCoordinates.Min();

            var hullYCoordinates = paintedHull.Keys.Select(point => point.Item2).ToList();
            var hullHeight = hullYCoordinates.Max() - hullYCoordinates.Min();
            var color = "\u2588";
            var colors = new List<long>();
            foreach (var y in Enumerable.Range(hullYCoordinates.Min(), hullHeight+1)) {
                Console.Write("\n");
                foreach (var x in Enumerable.Range(hullXCoordinates.Min(), hullWidth+1)) {
                    if (paintedHull.TryGetValue((x, y), out colors)) {
                        Console.Write(colors.Last() == 1 ? color : " ");

                    } else {
                        Console.Write(" ");
                    }

                }
            }
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
        }

        public static Dictionary<(int,int), List<long>> PaintHull(
            Dictionary<(int, int), List<long>> paintMap,
            List<long> intCode
        ){
            long input;
            var point = (0, 0);
            var direction = (0, -1);
            List<long> colors;

           
            // output, startingPosition, relativeBase
            var output = new List<long>() { 0, 0, 0 };
            
            
            while (true) {
            
                if (paintMap.TryGetValue(point, out colors)) {
                    input = colors.Last();
                    
                } else {
                    input = 0;
                }

                output = processIntCode(intCode, (int)output[1], new List<long>() { input }, new List<long>(), output[2]);
                if (output.Count == 0) {
                    break;
                }

                if (paintMap.TryGetValue(point, out colors)) {
                    colors.Add(output.First());

                } else {
                    paintMap.Add(point, new List<long>() { output.First() });
                }
               
                output = processIntCode(intCode, (int)output[1], new List<long>(), new List<long>(), output[2]);
                direction = GetNewDirection(direction, (int)output.First());


                point = (point.Item1 + direction.Item1, point.Item2 + direction.Item2);

            }
            
            return paintMap;
        }

        public static (int,int) GetNewDirection((int,int) direction, int directionCode) {
            // (0, 1) => (-1, 0) => (0, -1) => (1, 0)
            if (directionCode == 1) {
                return (-direction.Item2, direction.Item1);
            // (0, 1) => (1, 0) => (0, -1) => (-1, 0)
            } else {
                return (direction.Item2 , -direction.Item1);
            }
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
                return new List<long>() {};
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
                return new List<long>() { value1, startingPosition + getIncrementFromOpCode(opCode), relativeBase };
               
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

                var index = (int)(valueAddress + relativeBase);
                
                return intCode[index];


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


