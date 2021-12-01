namespace AdventOfCode2021
{
    public static class Day01
    {
        public static int CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var array = lines.Select(int.Parse).ToArray();

            var previous = 0;
            var result = 0;
            for (int i = 0; i < array.Length; i++)
            {
                var current = array[i];
                if (i > 0 && current > previous) result++;
                previous = current;
            }

            return result;
        }

        public static int CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var array = lines.Select(int.Parse).ToArray();

            var previous = 0;
            var result = 0;
            for (int i = 0; i < array.Length - 2; i++)
            {
                var current = array[i] + array[i + 1] + array[i + 2];
                if (i > 0 && current > previous) result++;
                previous = current;
            }

            return result;
        }
    }
}