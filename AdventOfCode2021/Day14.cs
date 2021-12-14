using System.Text;

namespace AdventOfCode2021
{
    public static class Day14
    {
        public static long CalculatePart1(string inputFileName) => Solve(inputFileName, 10);

        public static long CalculatePart2(string inputFileName) => Solve(inputFileName, 0);

        public static int Solve(string inputFileName, int numberOfSteps)
        {
            var lines = File.ReadAllLines(inputFileName);
            var polymerTemplate = lines[0];
            var insertionPairs = new Dictionary<string, string>();
            for (var i = 2; i<lines.Length; i++)
            {
                var values = lines[i].Split(" -> ");
                insertionPairs.Add(values[0], values[1]);
            }

            for (int t = 0; t < numberOfSteps; t++)
            {
                var nextPolymerTemplate = new StringBuilder();
                nextPolymerTemplate.Append(polymerTemplate[0]);
                for (var i = 0; i < polymerTemplate.Length-1; i++)
                {
                    var key = polymerTemplate.Substring(i, 2);
                    if (insertionPairs.TryGetValue(key, out var value))
                    {
                        nextPolymerTemplate.Append(value);
                    }
                    nextPolymerTemplate.Append(polymerTemplate[i+1]);
                }
                polymerTemplate = nextPolymerTemplate.ToString();
            }

            var groupedByLetters = polymerTemplate.GroupBy(x => x).OrderByDescending(x => x.Count());
            return groupedByLetters.First().Count()-groupedByLetters.Last().Count();
        }
    }
}