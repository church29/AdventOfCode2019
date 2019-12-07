﻿using System;
using System.Linq;

using NUnit.Framework;

namespace AdventOfCode2019.Day7 {

    [TestFixture]
    public class Day7Test {

        [Test]
        [TestCase("a)b", 1)]
        [TestCase("a)b,c)d", 2)]
        [TestCase("a)b,c)d,b)e", 4)]
        [TestCase("COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L", 42)]
        public void Test_CalculateNumOrbits(string input, int expectedResult) =>
            Assert.AreEqual(
                expectedResult,
                0
                );

    }
}
