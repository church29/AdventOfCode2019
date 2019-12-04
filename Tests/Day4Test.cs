using System;
using System.Linq;
using NUnit.Framework;

namespace AdventOfCode2019.Day4

{
    [TestFixture]
    public class PasswordElvesTest
    {

        [Test]
        [TestCase(111111, true)]
        [TestCase(223450, false)]
        [TestCase(223455, true)]
        [TestCase(123789, false)]
        [TestCase(113789, true)]
        public void Test_IsValidPassword(int password, Boolean expected) =>
            Assert.AreEqual(
                expected,
                PasswordElves.IsValidPassword(password)

                );



    }
}
