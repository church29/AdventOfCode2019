
using NUnit.Framework;

namespace AdventOfCode2019.Day8 {

    [TestFixture]
    public class Day8Test {

        [Test]
        [TestCase("99,0,0", "4,3,2,1,0", -1)]
        
        public void Test_Day8(string input, string phaseCode, int expectedResult) =>
            Assert.AreEqual(
                expectedResult,
                -1
                );




    }
}
