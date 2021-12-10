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

        private const int aWrongPointValuePart1 = 3;
        private const int bWrongPointValuePart1 = 57;
        private const int cWrongPointValuePart1 = 1197;
        private const int dWrongPointValuePart1 = 25137;

        private const int aWrongPointValuePart2 = 1;
        private const int bWrongPointValuePart2 = 2;
        private const int cWrongPointValuePart2 = 3;
        private const int dWrongPointValuePart2 = 4;
        private const int multiplyValuePart2 = 5;

        public static long CalculatePart1(string inputFileName)
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
                            if (character is aClosed) points += aWrongPointValuePart1;
                            else if (character is bClosed) points += bWrongPointValuePart1;
                            else if (character is cClosed) points += cWrongPointValuePart1;
                            else if (character is dClosed) points += dWrongPointValuePart1;
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

        public static long CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var pointList = new List<long>();
            foreach (var line in lines)
            {
                var stack = new Stack<char>();
                long points = 0;
                var isValid = true;
                foreach (var character in line.ToCharArray())
                {
                    if (character is aOpen or bOpen or cOpen or dOpen)
                    {
                        stack.Push(character);
                    }
                    else if (character is aClosed or bClosed or cClosed or dClosed)
                    {
                        var last = stack.Pop();
                        isValid = (last is aOpen && character is aClosed)
                            || (last is bOpen && character is bClosed)
                            || (last is cOpen && character is cClosed)
                            || (last is dOpen && character is dClosed);

                        if (!isValid) break;
                    }
                    else
                    {
                        throw new Exception("Wrong character!");
                    }
                }

                if (isValid && stack.Count > 0)
                {
                    foreach (var character in stack)
                    {
                        if (character is aOpen) points = points * multiplyValuePart2 + aWrongPointValuePart2;
                        else if (character is bOpen) points = points * multiplyValuePart2 + bWrongPointValuePart2;
                        else if (character is cOpen) points = points * multiplyValuePart2 + cWrongPointValuePart2;
                        else if (character is dOpen) points = points * multiplyValuePart2 + dWrongPointValuePart2;
                    }

                    pointList.Add(points);
                }
            }

            pointList.Sort();

            return pointList[pointList.Count / 2];
        }
    }
}