namespace AdventOfCode2021
{
    public static class Day09
    {
        public static int CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var xMax = lines.First().Length;
            var yMax = lines.Length;
            var numbers = new int[yMax, xMax];
            for (var i = 0; i < yMax; i++)
            {
                var lineNumbers = lines[i].ToCharArray();
                for (int j = 0; j < xMax; j++)
                {
                    numbers[i, j] = int.Parse(lineNumbers[j].ToString());
                }
            }

            var sum = 0;
            for (var i = 0; i < yMax; i++)
            {
                for (int j = 0; j < xMax; j++)
                {
                    var top = i - 1 >= 0 ? numbers[i - 1, j] : 9;
                    var bottom = i + 1 < yMax ? numbers[i + 1, j] : 9;
                    var right = j + 1 < xMax ? numbers[i, j + 1] : 9;
                    var left = j - 1 >= 0 ? numbers[i, j - 1] : 9;
                    var current = numbers[i, j];
                    if (current < top && current < bottom && current < right && current < left)
                    {
                        sum += current + 1;
                    }
                }
            }

            return sum;
        }

        public static int CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);

            //todo

            return 0;
        }
    }
}