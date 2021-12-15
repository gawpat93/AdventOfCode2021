namespace AdventOfCode2021
{
    public static class Day15
    {
        public static long CalculatePart1(string inputFileName) => Solve(inputFileName);

        public static long CalculatePart2(string inputFileName) => 0;//todo


        private static long Solve(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var riskLevelMap = new Dictionary<Point, int>();
            var xMax = lines.First().Length-1;
            var yMax = lines.Length-1;
            for (var y = 0; y<=yMax; y++)
            {
                var line = lines[y];
                for (var x = 0; x <= xMax; x++)
                {
                    riskLevelMap.Add(new Point(x, y), int.Parse(line[x].ToString()));
                }
            }

            var start = new Point(0, 0);
            var end = new Point(xMax, yMax);
            var queue = new PriorityQueue<Point, int>();
            var totalRiskMap = new Dictionary<Point, int>
            {
                { start, 0 }
            };
            queue.Enqueue(start, 0);

            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                foreach (var n in p.GetNeighbours())
                {
                    if (riskLevelMap.ContainsKey(n) && !totalRiskMap.ContainsKey(n))
                    {
                        var totalRisk = totalRiskMap[p] + riskLevelMap[n];
                        totalRiskMap[n] = totalRisk;
                        queue.Enqueue(n, totalRisk);
                    }
                }
            }

            return totalRiskMap[end];
        }

        private record Point(int X, int Y);

        private static IEnumerable<Point> GetNeighbours(this Point point) => new[]
        {
           new Point(point.X, point.Y + 1),
           new Point(point.X, point.Y - 1),
           new Point(point.X + 1, point.Y),
           new Point(point.X - 1, point.Y),
        };
    }
}