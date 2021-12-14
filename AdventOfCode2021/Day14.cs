namespace AdventOfCode2021
{
    public static class Day14
    {
        public static long CalculatePart1(string inputFileName) => Solve(inputFileName, 10);

        public static long CalculatePart2(string inputFileName) => Solve(inputFileName, 40);

        private class PairCollection
        {
            public Dictionary<string, long> Collection { get; private set; } = new();

            public void AddOrUpdate(string key, long value)
            {
                if (Collection.ContainsKey(key)) Collection[key]+=value;
                else Collection.Add(key, value);
            }
        }

        private class CharCollection
        {
            public Dictionary<char, long> Collection { get; private set; } = new();

            public void AddOrUpdate(char key, long value)
            {
                if (Collection.ContainsKey(key)) Collection[key]+=value;
                else Collection.Add(key, value);
            }
        }

        private static long Solve(string inputFileName, int numberOfSteps)
        {
            var lines = File.ReadAllLines(inputFileName);
            var polymerTemplate = lines[0];
            var insertionPairs = new Dictionary<string, string>();
            var pairs = new PairCollection();
            for (var i = 2; i<lines.Length; i++)
            {
                var values = lines[i].Split(" -> ");
                insertionPairs.Add(values[0], values[1]);
            }

            for (var i = 0; i < polymerTemplate.Length-1; i++)
            {
                var key = polymerTemplate.Substring(i, 2);
                pairs.AddOrUpdate(key, 1);
            }

            for (var t = 0; t < numberOfSteps; t++)
            {
                var nextPairs = new PairCollection();
                foreach (var pair in pairs.Collection)
                {
                    if (insertionPairs.TryGetValue(pair.Key, out var value))
                    {
                        var firstPair = pair.Key[0]+value;
                        var secondPair = value+pair.Key[1];
                        var count = pair.Value;
                        nextPairs.AddOrUpdate(firstPair, count);
                        nextPairs.AddOrUpdate(secondPair, count);
                    }
                    else
                    {
                        nextPairs.AddOrUpdate(pair.Key, pair.Value);
                    }
                }
                pairs = nextPairs;
            }

            var charCollection = new CharCollection();
            charCollection.AddOrUpdate(polymerTemplate.First(), 1);
            charCollection.AddOrUpdate(polymerTemplate.Last(), 1);
            foreach (var pair in pairs.Collection)
            {
                charCollection.AddOrUpdate(pair.Key[0], pair.Value);
                charCollection.AddOrUpdate(pair.Key[1], pair.Value);
            }

            var min = long.MaxValue;
            var max = long.MinValue;
            foreach (var item in charCollection.Collection)
            {
                var value = item.Value/2;
                if (value>max) max=value;
                if (value<min) min=value;
            }

            return max-min;
        }
    }
}