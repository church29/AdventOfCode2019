using NUnit.Framework;

namespace AdventOfCode2019.Day1 {

    [TestFixture]
    public class FuelCalculatorTest {

        [Test]
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 654)]
        [TestCase(100756, 33583)]
        public void Test_GetFuel(int input, int expectedResult) =>
            Assert.AreEqual(expectedResult, FuelCalculator.GetFuel(input));

        [Test]
        [TestCase(12, 2)]
        [TestCase(14, 2)]
        [TestCase(1969, 966)]
        [TestCase(100756, 50346)]
        public void Test_GetCompoundedFuel(int input, int expectedResult) =>
            Assert.AreEqual(expectedResult, FuelCalculator.GetCompoundedFuel(input));

    }
}
