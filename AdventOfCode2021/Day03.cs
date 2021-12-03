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
            var oxygenGeneratorRating = GetOxygenGeneratorRating(inputFileName);
            var CO2ScrubberRating = GetCO2ScrubberRating(inputFileName);
            return oxygenGeneratorRating * CO2ScrubberRating;
        }

        private static int GetOxygenGeneratorRating(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName).ToList();
            var length = lines[0].Length;
            for (int i = 0; i < length; i++)
            {
                var sum = 0;
                foreach (var line in lines)
                {
                    sum += int.Parse(line[i].ToString());
                }
;
                int mostCommonBit = (double)sum / (double)lines.Count >= 0.5 ? 1 : 0;

                lines = lines.Where(line => int.Parse(line[i].ToString()) == mostCommonBit).ToList();

                if (lines.Count == 1) break;
            }

            var result = string.Join("", lines.Single());
            int gammaRate = Convert.ToInt32(result, 2);

            return Convert.ToInt32(result, 2);
        }

        private static int GetCO2ScrubberRating(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName).ToList();
            var length = lines[0].Length;
            for (int i = 0; i < length; i++)
            {
                var sum = 0;
                foreach (var line in lines)
                {
                    sum += int.Parse(line[i].ToString());
                }
;
                int lessCommonBit = (double)sum / (double)lines.Count >= 0.5 ? 0 :1;

                lines = lines.Where(line => int.Parse(line[i].ToString()) == lessCommonBit).ToList();

                if (lines.Count == 1) break;
            }

            var result = string.Join("", lines.Single());
            int gammaRate = Convert.ToInt32(result, 2);

            return Convert.ToInt32(result, 2);
        }
    }
}