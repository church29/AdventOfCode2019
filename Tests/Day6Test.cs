using System;
using System.Linq;
using AdventOfCode2019.Day2;
using NUnit.Framework;

namespace AdventOfCode2019.Day6 {

    [TestFixture]
    public class OrbitalSantaTest {

        [Test]
        [TestCase("a)b", 1)]
        [TestCase("a)b,c)d", 2)]
        [TestCase("a)b,c)d,b)e", 4)]
        public void Test_CalculateNumOrbits(string input, int expectedResult) =>
            Assert.AreEqual(
                expectedResult,
                OrbitalSanta.CalculateNumOrbits(input.Split(",").ToList()));

    }
}
