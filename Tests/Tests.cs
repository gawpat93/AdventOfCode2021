using AdventOfCode2021;
using NUnit.Framework;

namespace Tests
{
    public class Tests
    {
        [Test]
        public void TestDay01()
        {
            var result1 = Day01.CalculatePart1(@"Data\inputDay01.dat");
            Assert.AreEqual(1316, result1);
            var result2 = Day01.CalculatePart2(@"Data\inputDay01.dat");
            Assert.AreEqual(1344, result2);
        }

        [Test]
        public void TestDay02()
        {
            var result1 = Day02.CalculatePart1(@"Data\inputDay02.dat");
            Assert.AreEqual(1815044, result1);
            var result2 = Day02.CalculatePart2(@"Data\inputDay02.dat");
            Assert.AreEqual(1739283308, result2);
        }

        [Test]
        public void TestDay03()
        {
            var result1 = Day03.CalculatePart1(@"Data\inputDay03.dat");
            Assert.AreEqual(845186, result1);
            var result2 = Day03.CalculatePart2(@"Data\inputDay03.dat");
            Assert.AreEqual(4636702, result2);
        }

        [Test]
        public void TestDay04()
        {
            var result1 = Day04.CalculatePart1(@"Data\inputDay04.dat");
            Assert.AreEqual(8442, result1);
            var result2 = Day04.CalculatePart2(@"Data\inputDay04.dat");
            Assert.AreEqual(4590, result2);
        }

        [Test]
        public void TestDay05()
        {
            var result1 = Day05.CalculatePart1(@"Data\inputDay05.dat");
            Assert.AreEqual(5835, result1);
            var result2 = Day05.CalculatePart2(@"Data\inputDay05.dat");
            Assert.AreEqual(17013, result2);
        }

        [Test]
        public void TestDay06()
        {
            var result1 = Day06.CalculatePart1(@"Data\inputDay06.dat");
            Assert.AreEqual(361169, result1);
            var result2 = Day06.CalculatePart2(@"Data\inputDay06.dat");
            Assert.AreEqual(1634946868992, result2);
        }

        [Test]
        public void TestDay07()
        {
            var result1 = Day07.CalculatePart1(@"Data\inputDay07.dat");
            Assert.AreEqual(347449, result1);
            var result2 = Day07.CalculatePart2(@"Data\inputDay07.dat");
            Assert.AreEqual(98039527, result2);
        }

        [Test]
        public void TestDay08()
        {
            var result1 = Day08.CalculatePart1(@"Data\inputDay08.dat");
            Assert.AreEqual(239, result1);
            var result2 = Day08.CalculatePart2(@"Data\inputDay08.dat");
            Assert.AreEqual(946346, result2);
        }

        [Test]
        public void TestDay09()
        {
            var result1 = Day09.CalculatePart1(@"Data\inputDay09.dat");
            Assert.AreEqual(417, result1);
            var result2 = Day09.CalculatePart2(@"Data\inputDay09.dat");
            Assert.AreEqual(1148965, result2);
        }

        [Test]
        public void TestDay10()
        {
            var result1 = Day10.CalculatePart1(@"Data\inputDay10.dat");
            Assert.AreEqual(339537, result1);
            var result2 = Day10.CalculatePart2(@"Data\inputDay10.dat");
            Assert.AreEqual(2412013412, result2);
        }

        [Test]
        public void TestDay11()
        {
            var result1 = Day11.CalculatePart1(@"Data\inputDay11.dat");
            Assert.AreEqual(1588, result1);
            var result2 = Day11.CalculatePart2(@"Data\inputDay11.dat");
            Assert.AreEqual(517, result2);
        }

        [Test]
        public void TestDay12()
        {
            var result1 = Day12.CalculatePart1(@"Data\inputDay12.dat");
            Assert.AreEqual(4749, result1);
            var result2 = Day12.CalculatePart2(@"Data\inputDay12.dat");
            Assert.AreEqual(123054, result2);
        }

        [Test]
        public void TestDay13()
        {
            var result1 = Day13.CalculatePart1(@"Data\inputDay13.dat");
            Assert.AreEqual(655, result1);
            var result2 = Day13.CalculatePart2(@"Data\inputDay13.dat");
            Assert.AreEqual(95, result2); //JPZCUAUR
        }

        [Test]
        public void TestDay14()
        {
            var result1 = Day14.CalculatePart1(@"Data\inputDay14.dat");
            Assert.AreEqual(2740, result1);
            var result2 = Day14.CalculatePart2(@"Data\inputDay14.dat");
            Assert.AreEqual(2959788056211, result2);
        }

        [Test]
        public void TestDay15()
        {
            var result1 = Day15.CalculatePart1(@"Data\inputDay15.dat");
            Assert.AreEqual(562, result1);
            var result2 = Day15.CalculatePart2(@"Data\inputDay15.dat");
            Assert.AreEqual(2874, result2);
        }

        [Test]
        public void TestDay16()
        {
            var result1 = Day16.CalculatePart1(@"Data\inputDay16.dat");
            Assert.AreEqual(883, result1);
            var result2 = Day16.CalculatePart2(@"Data\inputDay16.dat");
            Assert.AreEqual(1675198555015, result2);
        }

        [Test]
        public void TestDay17()
        {
            var result1 = Day17.CalculatePart1(@"Data\inputDay17.dat");
            Assert.AreEqual(2850, result1);
            var result2 = Day17.CalculatePart2(@"Data\inputDay17.dat");
            Assert.AreEqual(1117, result2);
        }

        [Test]
        public void TestDay18()
        {
            var result1 = Day18.CalculatePart1(@"Data\inputDay18.dat");
            Assert.AreEqual(4289, result1);
            var result2 = Day18.CalculatePart2(@"Data\inputDay18.dat");
            Assert.AreEqual(4807, result2);
        }

        [Test]
        public void TestDay19()
        {
            var result1 = Day19.CalculatePart1(@"Data\inputDay19.dat");
            Assert.AreEqual(306, result1);
            var result2 = Day19.CalculatePart2(@"Data\inputDay19.dat");
            Assert.AreEqual(9764, result2);
        }
    }
}