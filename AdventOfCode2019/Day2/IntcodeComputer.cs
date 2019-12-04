using System;
using System.Collections.Generic;
using System.Linq;
using static System.Environment;

namespace AdventOfCode2019.Day2
{
    public class IntcodeComputer
    {

        public static void processIntCodeFromFile()
        {
            var filePath = CurrentDirectory.ToString() + "/Day2/resources/input.txt";
            var lines = System.IO.File.ReadAllLines(filePath);
            var intCode = lines[0].Split(",").Select(int.Parse).ToList();


            var newIntCode = new List<int>(intCode);
            newIntCode[1] = 12;
            newIntCode[2] = 2;
            var processedIntCode = processIntCode(newIntCode, 0);

            Console.WriteLine("Day 2, Problem 1: " + processedIntCode[0]);

            
            foreach(var noun in Enumerable.Range(0, 99))
            {
                foreach(var verb in Enumerable.Range(0, 99))
                {
                    newIntCode = new List<int>(intCode);
                    newIntCode[1] = noun;
                    newIntCode[2] = verb;
                    var result = processIntCode(newIntCode, 0)[0];
                    if (result == 19690720)
                    {

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

        public static List<int> processIntCode(List<int> intCode, int startingPosition)
        {
            var opCode = intCode[startingPosition];
            if (opCode == 99)
            {
                return intCode;
            }
            var value1Address = intCode[startingPosition + 1];
            var value2Address = intCode[startingPosition + 2];
            var resultAddress = intCode[startingPosition + 3];

            var value1 = intCode[value1Address];
            var value2 = intCode[value2Address];



            intCode[resultAddress] = processOperation(opCode, value1, value2);

            return processIntCode(intCode, startingPosition + 4);

        }

        static int processOperation(int opCode, int value1, int value2)
        {
            if (opCode == 1)
            {
                return value1 + value2;
            }

            // opCode == 2
            return value1 * value2;

        }
    }
}
