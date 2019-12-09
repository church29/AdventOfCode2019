using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

namespace AdventOfCode2019.Day9 {

    [TestFixture]
    public class IntCodeComputerTest {

        /*
        [Test]
        [TestCase("1,0,0,0,99", "2,0,0,0,99")]
        [TestCase("2,3,0,3,99", "2,3,0,6,99")]
        [TestCase("2,4,4,5,99,0", "2,4,4,5,99,9801")]
        [TestCase("1,1,1,4,99,5,6,0,99", "30,1,1,4,2,5,6,0,99")]
        public void Test_ProcessIntCode(string input, string expectedResult) =>
            Assert.AreEqual(
                expectedResult,
                String.Join(",", BoostIntCodeComputer.processIntCode(input.Split(",").Select(int.Parse).ToList(), 0, new List<int>(), 0).ToArray()));

    */
        
        [Test]

       

       
        [TestCase(
            "104,1125899906842624,99",
            1125899906842624
        )]
        public void Test_ProcessIntCode2(string input, long expectedResult) =>
            Assert.AreEqual(
                expectedResult,
                BoostIntCodeComputer.processIntCode(input.Split(",").Select(long.Parse).ToList(), 0, new List<long>(), new List<long>(), 0).First());
        [Test]
        [TestCase(
            "1102,34915192,34915192,7,4,7,99,0",
            16
        )]
        public void Test_ProcessIntCodeLength(string input, long expectedResult) =>
           Assert.AreEqual(
               expectedResult,
               BoostIntCodeComputer.processIntCode(input.Split(",").Select(long.Parse).ToList(), 0, new List<long>(), new List<long>(), 0).First().ToString().Length);

        [Test]
        [TestCase(
            "109,1,204,-1,99,0,0",
            "109"
            )]
        [TestCase(
            "109,1,204,-1,109,2,204,-1,99,0,0",
            "109,204"
            )]
        [TestCase(
            "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99",
            "109,1,204,-1,1001,100,1,100,1008,100,16,101,1006,101,0,99"

        )]
        public void Test_ProcessIntCodeOutput(string input, string expectedResult) =>
           Assert.AreEqual(
               expectedResult,
               String.Join(",", BoostIntCodeComputer.processIntCode(input.Split(",").Select(long.Parse).ToList(), 0, new List<long>(), new List<long>(), 0)));

    }

}

