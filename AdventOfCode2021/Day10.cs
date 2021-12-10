namespace AdventOfCode2021
{
    public static class Day10
    {
        private const char aOpen = '(';
        private const char aClosed = ')';
        private const char bOpen = '[';
        private const char bClosed = ']';
        private const char cOpen = '{';
        private const char cClosed = '}';
        private const char dOpen = '<';
        private const char dClosed = '>';

        private const int aWrongPointValue = 3;
        private const int bWrongPointValue = 57;
        private const int cWrongPointValue = 1197;
        private const int dWrongPointValue = 25137;
        public static int CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var points = 0;
            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                foreach (var character in line.ToCharArray())
                {
                    if (character is aOpen or bOpen or cOpen or dOpen)
                    {
                        stack.Push(character);
                    }
                    else if (character is aClosed or bClosed or cClosed or dClosed)
                    {
                        var last = stack.Pop();
                        var isValid = (last is aOpen && character is aClosed)
                            || (last is bOpen && character is bClosed)
                            || (last is cOpen && character is cClosed)
                            || (last is dOpen && character is dClosed);

                        if (!isValid)
                        {
                            if (character is aClosed) points += aWrongPointValue;
                            else if (character is bClosed) points += bWrongPointValue;
                            else if (character is cClosed) points += cWrongPointValue;
                            else if (character is dClosed) points += dWrongPointValue;
                            break;
                        }
                    }
                    else
                    {
                        throw new Exception("Wrong character!");
                    }
                }
            }

            return points;
        }

        public static int CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);

            //todo

            return 0;
        }
    }
}