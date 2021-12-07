namespace AdventOfCode2021
{
    public static class Day07
    {
        private static int GetRisingStepCost(int numberOfSteps)
        {
            var cost = 0;
            var step = 1;
            while (step <= numberOfSteps)
            {
                cost += step;
                step++;
            }

            return cost;
        }

        private static int Solve(string inputFileName, bool risingStepCost)
        {
            var input = File.ReadAllText(inputFileName).Split(',').Select(int.Parse).OrderBy(x => x).ToArray();
            var max = input.Max();
            var min = input.Min();
            int cost = 0;
            for (var i = min; i <= max; i++)
            {
                var currentCost = 0;
                foreach (var item in input)
                {
                    var distance = Math.Abs(i - item);
                    currentCost += risingStepCost ? GetRisingStepCost(distance) : distance;
                }

                if (i == min || currentCost < cost)
                {
                    cost = currentCost;
                }
            }

            return cost;
        }

        public static int CalculatePart1(string inputFileName) => Solve(inputFileName, false);

        public static int CalculatePart2(string inputFileName) => Solve(inputFileName, true);
    }
}