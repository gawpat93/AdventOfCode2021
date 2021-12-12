namespace AdventOfCode2021
{
    public static class Day12
    {
        public const string START = "start";
        public const string END = "end";

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

        public static long CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var connections = new List<Connection>();

            foreach (var line in lines)
            {
                var elements = line.Split('-');
                connections.Add(new Connection(elements[0], elements[1]));
            }

            return CountPaths(connections, START, new List<string>());
        }

        private static int CountPaths(List<Connection> connections, string current, List<string> visited)
        {
            if (current == END) return 1;
            var count = 0;
            var allConnectionFromCurrentPath = GetAllConnections(connections, current);
            foreach (var next in allConnectionFromCurrentPath)
            {
                var alreadyVisited = visited.Any(x => x == next);
                var isBigCave = next.All(char.IsUpper);
                if (isBigCave || !alreadyVisited)
                {
                    var newVisited = new List<string>(visited);
                    newVisited.Add(current);
                    count += CountPaths(connections, next, newVisited);
                }
            }

            return count;
        }

        private static List<string> GetAllConnections(List<Connection> connections, string from) =>
        connections.Where(c => c.From == from).Select(x => x.To)
            .Union(connections.Where(c => c.To == from).Select(x => x.From)).ToList();

        public static long CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);

            //todo

            return 0;
        }
    }
}