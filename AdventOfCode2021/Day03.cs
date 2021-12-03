using System.Text;

namespace AdventOfCode2021
{
    public static class Day03
    {
        public static int CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var length = lines[0].Length;
            var count = 0;
            var numbers = new int[length];
            foreach (var line in lines)
            {
                var num = line.ToCharArray();
                for (int i = 0; i < length; i++)
                {
                    numbers[i] += int.Parse(num[i].ToString());
                }
                count++;
            }

            var gammaRateStr = new StringBuilder();
            var epsilonRateStr = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                if ((((double)numbers[i]) / ((double)count)) >= 0.5)
                {
                    gammaRateStr.Append('1');
                    epsilonRateStr.Append('0');
                }
                else
                {
                    gammaRateStr.Append('0');
                    epsilonRateStr.Append('1');
                }
            }

            int gammaRate = Convert.ToInt32(gammaRateStr.ToString(), 2);
            int epsilonRate = Convert.ToInt32(epsilonRateStr.ToString(), 2);

            return gammaRate * epsilonRate;
        }

        public static int CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            foreach (var line in lines)
            {
                //todo
            }

            return 0;
        }
    }
}