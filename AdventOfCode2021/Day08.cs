using System.Text;

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

        /*
         * Number of lines per value:
         * 0,6,9 - 6 
         * 1 - 2 *
         * 2,3,5 - 5
         * 4 - 4 *
         * 7 - 3 *
         * 8 - 7 *
         */
        public static int CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var count = 0;
            foreach (var line in lines)
            {
                var all = line.Split('|');
                var input = all[0].Trim().Split(' ');
                var output = all[1].Trim().Split(' ');

                var val_1 = input.Single(x => x.Length == 2);
                var val_4 = input.Single(x => x.Length == 4);
                var val_7 = input.Single(x => x.Length == 3);
                var val_8 = input.Single(x => x.Length == 7);

                var val_235 = input.Where(x => x.Length == 5);
                var val_3 = val_235.Single(x => x.Intersect(val_1).Count() == 2);
                var val_2 = val_235.Where(x => x != val_3).Single(x => x.Intersect(val_4).Count() == 2);
                var val_5 = val_235.Where(x => x != val_3).Single(x => x.Intersect(val_4).Count() == 3);

                var val_069 = input.Where(x => x.Length == 6);
                var val_6 = val_069.Single(x => x.Intersect(val_1).Count() == 1);
                var val_0 = val_069.Where(x => x != val_6).Single(x => x.Intersect(val_3).Count() == 4);
                var val_9 = val_069.Single(x => x != val_6 && x != val_0);

                var result = new StringBuilder(4);
                foreach (var val in output)
                {
                    var number = string.Empty;
                    if (hasSameChars(val, val_0))
                    {
                        number = "0";
                    }
                    else if (hasSameChars(val, val_1))
                    {
                        number = "1";
                    }
                    else if (hasSameChars(val, val_2))
                    {
                        number = "2";
                    }
                    else if (hasSameChars(val, val_3))
                    {
                        number = "3";
                    }
                    else if (hasSameChars(val, val_4))
                    {
                        number = "4";
                    }
                    else if (hasSameChars(val, val_5))
                    {
                        number = "5";
                    }
                    else if (hasSameChars(val, val_6))
                    {
                        number = "6";
                    }
                    else if (hasSameChars(val, val_7))
                    {
                        number = "7";
                    }
                    else if (hasSameChars(val, val_8))
                    {
                        number = "8";
                    }
                    else if (hasSameChars(val, val_9))
                    {
                        number = "9";
                    }
                    else
                    {
                        throw new Exception("Wrong output value!");
                    }

                    result.Append(number);
                }

                count += int.Parse(result.ToString());
            }

            return count;
        }

        private static bool hasSameChars(string str1, string str2) => str1.Length == str2.Length && str2.Intersect(str1).Count() == str2.Length;

    }
}