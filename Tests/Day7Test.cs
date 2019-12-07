using System;
using System.Linq;

using NUnit.Framework;

namespace AdventOfCode2019.Day7 {

    [TestFixture]
    public class Day7Test {

        [Test]
        [TestCase("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "4,3,2,1,0", 43210)]
        [TestCase("3,23,3,24,1002,24,10,24,1002,23,-1,23, 101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0", "0,1,2,3,4", 54321)]

        public void Test_CalculateNumOrbits(string input, string phaseCode, int expectedResult) =>
            Assert.AreEqual(
                expectedResult,
                MultiInputIntCodeComputer.getMaxThrusterAtPhaseSetting(
                    input.Split(",").Select(int.Parse).ToList(),
                    phaseCode.Split(",").Select(int.Parse).ToList()
                    )
                );

    }
}
