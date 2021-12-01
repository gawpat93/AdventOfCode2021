using AdventOfCode2021;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var result = Day01.CalculatePart1(@"Day01\Data\input.dat");
            Assert.AreEqual(1316, result);
        }

        [Test]
        public void Test2()
        {
            var result = Day01.CalculatePart2(@"Day01\Data\input.dat");
            Assert.AreEqual(1344, result);
        }
    }
}