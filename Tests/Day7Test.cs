using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace AdventOfCode2019.Day7 {

    [TestFixture]
    public class Day7Test {

        [Test]
        [TestCase("99,0,0", "4,3,2,1,0", 0)]
        [TestCase("3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0", "4,3,2,1,0", 43210)]
        [TestCase("3,23,3,24,1002,24,10,24,1002,23,-1,23, 101, 5, 23, 23, 1, 24, 23, 23, 4, 23, 99, 0, 0", "0,1,2,3,4", 54321)]
        public void Test_Thrusters(string input, string phaseCode, int expectedResult) =>
            Assert.AreEqual(
                expectedResult,
                MultiInputIntCodeComputer.getMaxThrusterAtPhaseSetting(
                    input.Split(",").Select(int.Parse).ToList(),
                    phaseCode.Split(",").Select(int.Parse).ToList(),
                    new List<int>() { 0, 0, 0, 0, 0 },
                    new Dictionary<int, List<int>>()
                    )
                );

        [Test]
        [TestCase("99,0,0", "4,3,2,1,0", 0)]
        [TestCase("3,26,1001,26,-4,26,3,27,1002,27,2,27,1,27,26, 27, 4, 27, 1001, 28, -1, 28, 1005, 28, 6, 99, 0, 0, 5", "9,8,7,6,5", 139629729)]
        [TestCase(
            "3,52,1001,52,-5,52,3,53,1,52,56,54,1007,54,5,55,1005,55,26,1001,54,-5, 54, 1105, 1, 12, 1, 53, 54, 53, 1008, 54, 0, 55, 1001, 55, 1, 55, 2, 53, 55, 53, 4,53, 1001, 56, -1, 56, 1005, 56, 6, 99, 0, 0, 0, 0, 10",
            "9,8,7,6,5",
            18216
         )]

        public void Test_ThrustersFeedback(string input, string phaseCode, int expectedResult) =>
           Assert.AreEqual(
               expectedResult,
               MultiInputIntCodeComputer.getMaxThrusterAtPhaseSetting(
                   input.Split(",").Select(int.Parse).ToList(),
                   phaseCode.Split(",").Select(int.Parse).ToList(),
                   new List<int>() { 0, 0, 0, 0, 0 },
                   new Dictionary<int, List<int>>()
               )
               );


    }
}
