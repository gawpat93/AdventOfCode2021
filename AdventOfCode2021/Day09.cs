namespace AdventOfCode2021
{
    public static class Day09
    {
        public static int CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var xMax = lines.First().Length;
            var yMax = lines.Length;
            var numbers = new int[yMax, xMax];
            for (var i = 0; i < yMax; i++)
            {
                var lineNumbers = lines[i].ToCharArray();
                for (int j = 0; j < xMax; j++)
                {
                    numbers[i, j] = int.Parse(lineNumbers[j].ToString());
                }
            }

            var sum = 0;
            for (var i = 0; i < yMax; i++)
            {
                for (int j = 0; j < xMax; j++)
                {
                    var top = i - 1 >= 0 ? numbers[i - 1, j] : 9;
                    var bottom = i + 1 < yMax ? numbers[i + 1, j] : 9;
                    var right = j + 1 < xMax ? numbers[i, j + 1] : 9;
                    var left = j - 1 >= 0 ? numbers[i, j - 1] : 9;
                    var current = numbers[i, j];
                    if (current < top && current < bottom && current < right && current < left)
                    {
                        sum += current + 1;
                    }
                }
            }

            return sum;
        }

        public static int CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var xMax = lines.First().Length;
            var yMax = lines.Length;
            var numbers = new int[yMax, xMax];
            for (var i = 0; i < yMax; i++)
            {
                var lineNumbers = lines[i].ToCharArray();
                for (int j = 0; j < xMax; j++)
                {
                    numbers[i, j] = int.Parse(lineNumbers[j].ToString());
                }
            }

            var lowPoints = new List<(int, int)>();
            for (var i = 0; i < yMax; i++)
            {
                for (int j = 0; j < xMax; j++)
                {
                    var top = i - 1 >= 0 ? numbers[i - 1, j] : 9;
                    var bottom = i + 1 < yMax ? numbers[i + 1, j] : 9;
                    var right = j + 1 < xMax ? numbers[i, j + 1] : 9;
                    var left = j - 1 >= 0 ? numbers[i, j - 1] : 9;
                    var current = numbers[i, j];
                    if (current < top && current < bottom && current < right && current < left)
                    {
                        lowPoints.Add(new(j, i));
                    }
                }
            }

            var lowPointCounts = new List<int>();
            foreach (var lowPoint in lowPoints)
            {
                lowPointCounts.Add(GetAllBasinPointsSum(numbers, lowPoint.Item2, lowPoint.Item1));
            }

            var result = 1;
            lowPointCounts.OrderByDescending(x => x).Take(3).ToList().ForEach(x => result *= x);
            return result;
        }

        private static int GetAllBasinPointsSum(int[,] array, int x, int y)
        {
            var basinPoints = new List<(int, int)>();
            if (array[x, y] == 9) return basinPoints.Count;
            basinPoints.Add(new(x, y));
            GetTop(array, ref basinPoints, x, y);
            GetRight(array, ref basinPoints, x, y);
            GetLeft(array, ref basinPoints, x, y);
            GetBottom(array, ref basinPoints, x, y);
            return basinPoints.Count;
        }

        private static void GetTop(int[,] array, ref List<(int, int)> basinPoints, int x, int y)
        {
            x--;
            if (CannotProceed(array, ref basinPoints, x, y)) return;
            basinPoints.Add(new(x, y));
            GetLeft(array, ref basinPoints, x, y);
            GetTop(array, ref basinPoints, x, y);
            GetRight(array, ref basinPoints, x, y);
        }

        private static void GetRight(int[,] array, ref List<(int, int)> basinPoints, int x, int y)
        {
            y++;
            if (CannotProceed(array, ref basinPoints, x, y)) return;
            basinPoints.Add(new(x, y));
            GetTop(array, ref basinPoints, x, y);
            GetRight(array, ref basinPoints, x, y);
            GetBottom(array, ref basinPoints, x, y);
        }

        private static void GetBottom(int[,] array, ref List<(int, int)> basinPoints, int x, int y)
        {
            x++;
            if (CannotProceed(array, ref basinPoints, x, y)) return;
            basinPoints.Add(new(x, y));
            GetLeft(array, ref basinPoints, x, y);
            GetBottom(array, ref basinPoints, x, y);
            GetRight(array, ref basinPoints, x, y);
        }

        private static void GetLeft(int[,] array, ref List<(int, int)> basinPoints, int x, int y)
        {
            y--;
            if (CannotProceed(array, ref basinPoints, x, y)) return;
            basinPoints.Add(new(x, y));
            GetTop(array, ref basinPoints, x, y);
            GetLeft(array, ref basinPoints, x, y);
            GetBottom(array, ref basinPoints, x, y);
        }

        private static bool CannotProceed(int[,] array, ref List<(int, int)> basinPoints, int x, int y) =>
               x < 0 || x >= array.GetLength(0)
            || y < 0 || y >= array.GetLength(1)
            || array[x, y] == 9
            || basinPoints.Any(ele => ele.Item1 == x && ele.Item2 == y);
    }
}