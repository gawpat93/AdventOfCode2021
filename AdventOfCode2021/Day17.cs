namespace AdventOfCode2021
{
    public static class Day17
    {
        private record TargetArea(int MinX, int MaxX, int MinY, int MaxY);
        private record Point(int X, int Y);

        private static TargetArea LoadTargetArea(string inputFileName)
        {
            var text = File.ReadAllText(inputFileName);
            var ranges = text[12..].Split(",").Select(x => x.Trim()).ToArray();
            var xRange = ranges[0][2..].Split("..").Select(int.Parse).ToArray();
            var yRange = ranges[1][2..].Split("..").Select(int.Parse).ToArray();
            return new TargetArea(xRange[0], xRange[1], yRange[0], yRange[1]);
        }

        private static (int maxY, bool targetReached) CalculateMaxY(int vX, int vY, TargetArea ta)
        {
            var maxY = 0;
            var x = 0;
            var y = 0;
            var targetReached = false;
            while (true)
            {
                x+=vX;
                y+=vY;
                if (y>maxY) maxY = y;

                vY--;
                if (vX>0) vX--;

                if (x >= ta.MinX && x <= ta.MaxX && y>= ta.MinY && y<= ta.MaxY)
                {
                    targetReached= true;
                    break;
                }

                if (y<ta.MinY) break;
            }

            return (maxY, targetReached);
        }

        public static long CalculatePart1(string inputFileName)
        {
            var result = int.MinValue;
            var targetArea = LoadTargetArea(inputFileName);
            var vxMin = 0;
            var vxMax = targetArea.MaxX;
            var vyMin = targetArea.MinY;
            var vyMax = -targetArea.MinY;
            for (var vx = vxMin; vx <=vxMax; vx++)
            {
                for (var vY = vyMin; vY <= vyMax; vY++)
                {
                    var (maxY, targetReached)= CalculateMaxY(vx, vY, targetArea);
                    if (targetReached && result < maxY) result = maxY;
                }
            }

            return result;
        }

        public static long CalculatePart2(string inputFileName)
        {
            var targetArea = LoadTargetArea(inputFileName);
            //todo
            return 0;
        }
    }
}