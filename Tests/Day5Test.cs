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
        [TestCase(111111, false)]
        [TestCase(122222, false)]
        [TestCase(112233, true)]
        [TestCase(123444, false)]
        [TestCase(111122, true)]
        public void Test_IsValid2(int password, Boolean expected) =>
            Assert.AreEqual(
                expected,
                true
            );
    }
}
