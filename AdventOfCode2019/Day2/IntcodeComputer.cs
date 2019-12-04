using System;
using System.Collections.Generic;

namespace AdventOfCode2019.Day2
{
    public class IntcodeComputer
    {
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
