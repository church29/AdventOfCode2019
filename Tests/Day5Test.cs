using System;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2019.Day5 {

    [TestFixture]
    public class ElvesTest {

        [Test]
        [TestCase(111111, true)]
        [TestCase(223450, false)]
        [TestCase(223455, true)]
        [TestCase(123789, false)]
        [TestCase(113789, true)]
        public void Test_IsValid(int password, Boolean expected) =>
            Assert.AreEqual(
                expected,
                true
            );

        [Test]
        [TestCase("3,225,1,225,6,6,1100,1,238,225,104,0,1101,34,7,225,101,17,169,224,1001,224,-92,224,4,99", false)]

        public void Test_IsValid2(string password, Boolean expected) =>
            Day2.IntcodeComputer.processIntCode(password.Split(",").Select(int.Parse).ToList(), 0);
            
    }
}
