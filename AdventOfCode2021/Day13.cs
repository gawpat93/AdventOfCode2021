namespace AdventOfCode2021
{
    public static class Day13
    {
        private static (List<Point> points, List<Fold> instructions) LoadPointsAndInstructions(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var points = new List<Point>();
            var instructions = new List<Fold>();
            foreach (var line in lines)
            {
                if (line==string.Empty) continue;
                if (line.StartsWith("fold along"))
                {
                    var values = line.Remove(0, 11).Trim().Split('=');
                    instructions.Add(new Fold(values[0], int.Parse(values[1])));
                }
                else
                {
                    var values = line.Split(',').Select(int.Parse).ToArray();
                    points.Add(new Point(values[0], values[1]));
                }
            }

            return (points, instructions);
        }

        public static long CalculatePart1(string inputFileName)
        {
            var input = LoadPointsAndInstructions(inputFileName);
            var points = input.points;
            var instructions = input.instructions;

            var inst = instructions.First();
            for (int i = 0; i<points.Count; i++)
            {
                var val = inst.Value;
                var axis = inst.Axis;
                if (axis is AxisX)
                {
                    if (points[i].X > val)
                    {
                        var newX = 2*val - points[i].X;
                        points[i].SetX(newX);
                    }
                }
                else if (axis is AxisY)
                {
                    if (points[i].Y > val)
                    {
                        var newY = 2*val - points[i].Y;
                        points[i].SetY(newY);
                    }
                }
            }

            return DistinctPoints(points).Count;
        }

        public static long CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);

            //todo

            return 0;
        }

        private static List<Point> DistinctPoints(List<Point> points)
        {
            var uniquePoints = new List<Point>();
            foreach (var point in points)
            {
                if (!uniquePoints.Any(up => up.X == point.X && up.Y == point.Y)) uniquePoints.Add(point);
            }

            return uniquePoints;
        }

        private record struct Fold(string Axis, int Value);

        private const string AxisX = "x";
        private const string AxisY = "y";

        private class Point
        {
            public int X { get; private set; }
            public int Y { get; private set; }

            public void SetX(int val) => X = val;
            public void SetY(int val) => Y = val;

            public Point(int x, int y)
            {
                X= x;
                Y= y;
            }
        }
    }
}