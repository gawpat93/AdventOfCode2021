namespace AdventOfCode2021
{
    public static class Day06
    {
        private static long Solve(string inputFileName, int steps)
        {
            var input = File.ReadAllText(inputFileName).Split(',').Select(long.Parse).ToArray();
            var numbers = new Dictionary<int, long>();
            for (var i = 0; i <= 8; i++)
            {
                var count = input.Count(x => x == i);
                numbers.Add(i, count);
            }

            for (var t = 0; t < steps; t++)
            {
                var next = new Dictionary<int, long>();
                foreach (var current in numbers)
                {
                    var key = current.Key;
                    long value = current.Value;
                    if (value == 0) continue;
                    if (key == 0)
                    {
                        if (!next.TryAdd(6, value))
                        {
                            next[6] += value;
                        }
                        if (!next.TryAdd(8, value))
                        {
                            next[8] += value;
                        }
                    }
                    else
                    {
                        var nextKey = key - 1;
                        if (!next.TryAdd(nextKey, value))
                        {
                            next[nextKey] += value;
                        }

                    }
                }
                numbers = new Dictionary<int, long>(next);
            }

            return numbers.Sum(x => x.Value);
        }
        public static long CalculatePart1(string inputFileName) => Solve(inputFileName, 80);

        public static long CalculatePart2(string inputFileName) => Solve(inputFileName, 256);
    }
}