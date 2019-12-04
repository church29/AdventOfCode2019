using System;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2019.Day2 {

    [TestFixture]
    public class IntCodeComputerTest {

        [Test]
        [TestCase("1,0,0,0,99", "2,0,0,0,99")]
        [TestCase("2,3,0,3,99", "2,3,0,6,99")]
        [TestCase("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [TestCase("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void Test_GetFuel(string input, string expectedResult) =>
            Assert.AreEqual(
                expectedResult,
                String.Join(",", IntcodeComputer.processIntCode(input.Split(",").Select(int.Parse).ToList(), 0).ToArray()));

    }
}
