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
        [TestCase(123789, false)]
        public void Test_IsValidPassword(int password, Boolean expected) =>
            Assert.AreEqual(
                expected,
                PasswordElves.IsValidPassword(password)

                );



    }
}
