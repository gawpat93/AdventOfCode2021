namespace AdventOfCode2021
{
    public static class Day19
    {
        public static long CalculatePart1(string inputFileName)
        {
            var scanners = LoadScannerData(inputFileName);
            var allRelativePoints = new List<Vector>();

            var res = GetCommonPoints(scanners[0], scanners[1]);

            foreach (var point in res.commonPoints)
            {
                var tmp2 = point.first.Subtract(point.second.GetOrientation(res.orientationId));
            }

            //todo

            return 0;
        }

        private static (IEnumerable<(Vector first, Vector second)> commonPoints, int orientationId) GetCommonPoints(Scanner first, Scanner second)
        {
            for (var i = 0; i<24; i++)
            {
                var result = new List<(Vector first, Vector second)>();
                var j = 0;
                foreach (var beconOffsets in first.GetOffsets())
                {
                    var k = 0;
                    foreach (var beconOffsetsToCompare in second.GetOffsets())
                    {
                        var common = beconOffsetsToCompare.Where(botc => beconOffsets.Any(bo => bo.GetOrientation(i).X == botc.X && bo.GetOrientation(i).Y == botc.Y && bo.GetOrientation(i).Z == botc.Z));
                        if (common.Count() >= 12)
                        {
                            var v1 = first.Points[j];
                            var v2 = second.Points[k];
                            result.Add((v1, v2));
                        }
                        k++;
                    }
                    j++;
                }
                if (result.Count >=12) return (result, i);
            }

            return new(new List<(Vector first, Vector second)>(), -1);

        }

        public static long CalculatePart2(string inputFileName)
        {
            var scanners = LoadScannerData(inputFileName);

            //todo

            return 0;
        }

        private static List<Scanner> LoadScannerData(string inputFileName)
        {
            var result = new List<Scanner>();
            var lines = File.ReadAllLines(inputFileName);
            var i = 0;
            foreach (var line in lines)
            {
                if (line.StartsWith("--- scanner")) result.Add(new Scanner(i));
                else if (string.IsNullOrWhiteSpace(line)) continue;
                else
                {
                    var coordinates = line.Split(',').Select(int.Parse).ToArray();
                    var point = new Vector(coordinates[0], coordinates[1], coordinates[2]);
                    result.Last().Points.Add(point);
                }
                i++;
            }

            return result;
        }

        private class Scanner
        {
            public int Id { get; init; }
            public Vector Position { get; init; } = new Vector(0, 0, 0);
            public List<Vector> Points { get; init; } = new();

            public List<List<Vector>> GetOffsets()
            {
                var result = new List<List<Vector>>();
                foreach (var point in Points)
                {
                    var pointOffsets = new List<Vector>();
                    foreach (var otherPoint in Points)
                    {
                        pointOffsets.Add(point.Subtract(otherPoint));
                    }

                    result.Add(pointOffsets);
                }

                return result;
            }

            public Scanner(int id)
            {
                Id = id;
            }
        }

        private record Vector(int X, int Y, int Z)
        {
            public Vector Subtract(Vector v) => new(X-v.X, Y-v.Y, Z-v.Z);
            public Vector Add(Vector v) => new(X+v.X, Y+v.Y, Z+v.Z);

            public Vector GetOrientation(int id) => id switch
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