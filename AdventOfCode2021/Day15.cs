namespace AdventOfCode2021
{
    public static class Day15
    {
        public static long CalculatePart1(string inputFileName)
        {
            (var riskLevelMap, var xMax, var yMax) = GetRiskLevelMap(inputFileName);
            return Solve(riskLevelMap, xMax, yMax);
        }

        public static long CalculatePart2(string inputFileName)
        {
            (var riskLevelMap, var xMax, var yMax) = GetRiskLevelMap(inputFileName, true);
            return Solve(riskLevelMap, xMax, yMax);
        }

        private static long Solve(Dictionary<Point, int> riskLevelMap, int xMax, int yMax)
        {
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

        private static (Dictionary<Point, int> RiskLevelMap, int xMax, int yMax) GetRiskLevelMap(string inputFileName, bool part2 = false)
        {
            var lines = File.ReadAllLines(inputFileName);
            var riskLevelMap = new Dictionary<Point, int>();
            var lineLength = lines.First().Length;
            var linesNumber = lines.Length;
            var xMax = (part2 ? lines.First().Length*5 : lines.First().Length) - 1;
            var yMax = (part2 ? lines.Length*5 : lines.Length) - 1;

            for (var y = 0; y <= yMax; y++)
            {
                var line = lines[y%linesNumber];
                for (var x = 0; x <= xMax; x++)
                {
                    var addtionalRisk = y/linesNumber + x/lineLength;
                    var risk = int.Parse(line[x%lineLength].ToString())+addtionalRisk;
                    riskLevelMap.Add(new Point(x, y), risk > 9 ? risk % 9 : risk);
                }
            }

            return (riskLevelMap, xMax, yMax);
        }

        private record Point(int X, int Y);

        //without diagonal
        private static IEnumerable<Point> GetNeighbours(this Point point) => new[]
        {
           new Point(point.X, point.Y + 1),
           new Point(point.X, point.Y - 1),
           new Point(point.X + 1, point.Y),
           new Point(point.X - 1, point.Y),
        };
    }
}