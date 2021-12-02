namespace AdventOfCode2021
{
    public static class Day02
    {
        public static int CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var horizontal = 0;
            var depth = 0;
            foreach (var line in lines)
            {
                var elements = line.Split(' ');
                var command = elements[0];
                var value = int.Parse(elements[1]);
                if (command == "forward")
                {
                    horizontal += value;
                }
                else if (command == "down")
                {
                    depth += value;
                }
                else if (command == "up")
                {
                    depth -= value;
                }
            }

            return horizontal*depth;
        }

        public static int CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var horizontal = 0;
            var depth = 0;
            var aim = 0;
            foreach (var line in lines)
            {
                var elements = line.Split(' ');
                var command = elements[0];
                var value = int.Parse(elements[1]);
                if (command == "forward")
                {
                    horizontal += value;
                    depth += value * aim;
                }
                else if (command == "down")
                {
                    aim += value;
                }
                else if (command == "up")
                {
                    aim -= value;
                }
            }

            return horizontal * depth;
        }
    }
}