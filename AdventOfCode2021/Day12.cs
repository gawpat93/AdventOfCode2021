namespace AdventOfCode2021
{
    public static class Day12
    {
        private const string START = "start";
        private const string END = "end";

        public static long CalculatePart1(string inputFileName) => CountPaths(GetConnections(inputFileName), START, new List<string>(), false);
        public static long CalculatePart2(string inputFileName) => CountPaths(GetConnections(inputFileName), START, new List<string>(), false, true);

        private class Connection
        {
            public string From { get; private set; }
            public string To { get; private set; }

            public Connection(string from, string to)
            {
                From = from;
                To = to;
            }
        }

        private static int CountPaths(IEnumerable<Connection> connections, string current, IEnumerable<string> visitedCaves, bool smallCaveVisitedTwice, bool part2 = false)
        {
            if (current == END) return 1;
            var newVisitedCaves = new List<string>(visitedCaves) { current };
            var count = 0;
            var allConnectionFromCurrentPath = GetAllConnections(connections, current);
            foreach (var cave in allConnectionFromCurrentPath)
            {
                var alreadyVisited = newVisitedCaves.Contains(cave);
                var isBigCave = cave.Any(char.IsUpper);
                if (isBigCave || !alreadyVisited)
                {
                    count += CountPaths(connections, cave, newVisitedCaves, smallCaveVisitedTwice, part2);
                }
                else if (part2 && cave != START && !smallCaveVisitedTwice)
                {
                    count += CountPaths(connections, cave, newVisitedCaves, true, part2);
                }
            }

            return count;
        }

        private static IEnumerable<string> GetAllConnections(IEnumerable<Connection> connections, string from) =>
            connections.Where(c => c.From == from).Select(x => x.To).Union(connections.Where(c => c.To == from).Select(x => x.From));

        private static IEnumerable<Connection> GetConnections(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var connections = new Connection[lines.Length];

            for (var i = 0; i < lines.Length; i++)
            {
                var elements = lines[i].Split('-');
                connections[i] = new Connection(elements[0], elements[1]);
            }

            return connections;
        }
    }
}