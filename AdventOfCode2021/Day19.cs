namespace AdventOfCode2021
{
    public static class Day19
    {
        public static long CalculatePart1(string inputFileName) => GetNumberOfBeacons(inputFileName);
        public static long CalculatePart2(string inputFileName)
        {
            var (_, scanners)=Process(inputFileName);
            var maxDistance = 0;
            for (int i = 0; i < scanners.Count; i++)
            {
                for (int j = 0; j < scanners.Count; j++)
                {
                    if (i==j) continue;
                    var md = scanners[i].ManhattanDistanceTo(scanners[j]);
                    if (md>maxDistance) maxDistance = md;
                }
            }

            return maxDistance;
        }

        private static int GetNumberOfBeacons(string inputFileName)
        {
            var (numberOfBeacons, _)=Process(inputFileName);
            return numberOfBeacons;
        }
        private static (int numberOfBeacons, List<Vector> scanners) Process(string inputFileName)
        {
            var scanners = LoadScannerData(inputFileName);
            var toProcess = new Queue<Scanner>(scanners.Skip(1));
            var allRelative = scanners[0].Points.ToHashSet();
            var scannerPositions = new List<Vector> { new Vector(0, 0, 0) };

            while (toProcess.TryDequeue(out var current))
            {
                bool processed = false;
                for (var t = 0; t<24; t++)
                {
                    var transformed = current.Points.Select(p => p.Transform(t)).ToArray();
                    var offset = transformed.SelectMany(i => allRelative.Select(j => i.Subtract(j)))
                        .GroupBy(g => g).Select(i => (Key: i.Key, Count: i.Count())).MaxBy(i => i.Count);
                    if (offset.Count < 12) continue;
                    var added = transformed.Count(i => allRelative.Add(i.Subtract(offset.Key)));
                    processed = true;
                    scannerPositions.Add(offset.Key);
                    break;
                }

                if (!processed) toProcess.Enqueue(current);
            }

            return (allRelative.Count, scannerPositions);
        }

        private static List<Scanner> LoadScannerData(string inputFileName)
        {
            var result = new List<Scanner>();
            var lines = File.ReadAllLines(inputFileName);
            var i = 0;
            foreach (var line in lines)
            {
                if (line.StartsWith("--- scanner")) result.Add(new Scanner());
                else if (string.IsNullOrWhiteSpace(line)) continue;
                else
                {
                    var coordinates = line.Split(',').Select(int.Parse).ToArray();
                    var point = new Vector(coordinates[0], coordinates[1], coordinates[2]);
                    result.Last().Points.Add(point);
                }
            }

            return result;
        }

        private class Scanner
        {
            public List<Vector> Points { get; init; } = new();
        }

        private record Vector(int X, int Y, int Z)
        {
            public Vector Subtract(Vector v) => new(X-v.X, Y-v.Y, Z-v.Z);
            public Vector Add(Vector v) => new(X+v.X, Y+v.Y, Z+v.Z);
            public int ManhattanDistanceTo(Vector v) => Math.Abs(X - v.X) + Math.Abs(Y - v.Y) + Math.Abs(Z - v.Z);
            public Vector Transform(int id) => id switch
            {
                0 => new(X, Y, Z),
                1 => new(X, -Z, Y),
                2 => new(X, -Y, -Z),
                3 => new(X, Z, -Y),

                4 => new(-Y, X, Z),
                5 => new(Z, X, Y),
                6 => new(Y, X, -Z),
                7 => new(-Z, X, -Y),

                8 => new(-X, -Y, Z),
                9 => new(-X, -Z, -Y),
                10 => new(-X, Y, -Z),
                11 => new(-X, Z, Y),

                12 => new(Y, -X, Z),
                13 => new(Z, -X, -Y),
                14 => new(-Y, -X, -Z),
                15 => new(-Z, -X, Y),

                16 => new(-Z, Y, X),
                17 => new(Y, Z, X),
                18 => new(Z, -Y, X),
                19 => new(-Y, -Z, X),

                20 => new(-Z, -Y, -X),
                21 => new(-Y, Z, -X),
                22 => new(Z, Y, -X),
                23 => new(Y, -Z, -X),
                _ => throw new ArgumentException()
            };
        }
    }
}