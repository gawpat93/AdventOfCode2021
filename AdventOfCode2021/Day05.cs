namespace AdventOfCode2021
{
    public static class Day05
    {
        private class MarkedPoints
        {
            private readonly Dictionary<string, int> Points = new();

            public int GetCriticalPointsNumber() => Points.Count(p => p.Value > 1);

            public void MarkPoint(int x, int y)
            {
                var key = $"{x},{y}";
                if (Points.ContainsKey(key))
                {
                    Points[key]++;
                }
                else
                {
                    Points.Add(key, 1);
                }
            }
        }

        private static int Solve(string inputFileName, bool withDiagonal = false)
        {
            var lines = File.ReadAllLines(inputFileName);
            var mps = new MarkedPoints();
            foreach (var line in lines)
            {
                var points = line.Trim().Split("->");
                var begin = points[0].Trim().Split(',').Select(int.Parse).ToArray();
                var end = points[1].Trim().Split(',').Select(int.Parse).ToArray();
                var x0 = begin[0];
                var y0 = begin[1];
                var x1 = end[0];
                var y1 = end[1];
                if (x0 == x1)
                {
                    for (var y = Math.Min(y0, y1); y <= Math.Max(y0, y1); y++)
                    {
                        mps.MarkPoint(x0, y);
                    }
                }
                else if (y0 == y1)
                {
                    for (var x = Math.Min(x0, x1); x <= Math.Max(x0, x1); x++)
                    {
                        mps.MarkPoint(x, y0);
                    }
                }
                else if (withDiagonal)
                {
                    var stepX = x0 < x1 ? 1 : -1;
                    var stepY = y0 < y1 ? 1 : -1;
                    for (var i = 0; i <= Math.Abs(x0 - x1); i++)
                    {
                        var x = x0 + i * stepX;
                        var y = y0 + i * stepY;
                        mps.MarkPoint(x, y);
                    }
                }
            }

            return mps.GetCriticalPointsNumber();
        }

        public static int CalculatePart1(string inputFileName) => Solve(inputFileName);

        public static int CalculatePart2(string inputFileName) => Solve(inputFileName, true);
    }
}