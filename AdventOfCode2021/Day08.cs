namespace AdventOfCode2021
{
    public static class Day08
    {
        /*
         * Number of lines per value:
         * 0 - 6 
         * 1 - 2 *
         * 2 - 5
         * 3 - 5
         * 4 - 4 *
         * 5 - 5
         * 6 - 6
         * 7 - 3 *
         * 8 - 7 *
         * 9 - 6
         */
        public static int CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var count = 0;
            foreach (var line in lines)
            {
                var outputValues = line.Split('|')[1].Trim().Split(' ');
                foreach (var value in outputValues)
                {
                    var len = value.Length;
                    if (len is 2 or 4 or 3 or 7)
                    {
                        count++;
                    }
                }
            }


            return count;
        }

        public static int CalculatePart2(string inputFileName) => 0;
    }
}