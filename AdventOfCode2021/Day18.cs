namespace AdventOfCode2021
{
    public static class Day18
    {
        public static long CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var numbers = new List<SnailFishNumberPair>();
            foreach (var line in lines)
            {
                numbers.Add(CreatePair(line, null));
            }

            var result = numbers.First();
            do
            {
                if (result.GetMaxSubLevel()>=4) result.Explode(0);
                if (!result.TrySplitting()) break;
            }
            while (true);

            for (var i = 1; i < numbers.Count; i++)
            {
                do
                {
                    if (result.GetMaxSubLevel()>=4) result.Explode(0);
                    if (!result.TrySplitting()) break;
                }
                while (true);
                result = Add(result, numbers[i]);
            }

            return result.GetMagnitude();
        }

        private static SnailFishNumberPair CreatePair(string input, SnailFishNumberPair? parent)
        {
            input = input.Substring(1); //remove '['
            input = input.Substring(0, input.Length-1); //remove ']'
            var pair = new SnailFishNumberPair(parent);
            var endFirstIndex = 0;
            if (input.StartsWith("["))
            {
                var openingCount = 0;
                var endingCount = 0;
                for (endFirstIndex = 0; endFirstIndex<input.Length; endFirstIndex++)
                {
                    var current = input[endFirstIndex];
                    if (current == '[') openingCount++;
                    else if (current == ']') endingCount++;
                    if (openingCount==endingCount) break;
                }
                endFirstIndex++;
                pair.SetFirstPair(CreatePair(input.Substring(0, endFirstIndex), pair));
            }
            else
            {
                endFirstIndex = 1;
                pair.SetFirstNumber(int.Parse(input[0].ToString()));
            }

            input = input.Substring(endFirstIndex+1);

            if (input.StartsWith("["))
            {
                pair.SetSecondPair(CreatePair(input, pair));
            }
            else
            {
                pair.SetSecondNumber(int.Parse(input[0].ToString()));
            }

            return pair;
        }

        public static long CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);

            //todo

            return 0;
        }

        private static SnailFishNumberPair Add(SnailFishNumberPair first, SnailFishNumberPair second)
        {
            var result = new SnailFishNumberPair(null);
            first.SetParent(result);
            second.SetParent(result);
            result.SetFirstPair(first);
            result.SetSecondPair(second);
            return result;
        }

        private class SnailFishNumberPair
        {
            public int? FirstNumber { get; private set; }
            public SnailFishNumberPair? FirstPair { get; private set; }
            public int? SecondNumber { get; private set; }
            public SnailFishNumberPair? SecondPair { get; private set; }
            public SnailFishNumberPair? Parent { get; private set; }

            public SnailFishNumberPair(SnailFishNumberPair? parent)
            {
                Parent = parent;
            }

            public void SetParent(SnailFishNumberPair? parent)
            {
                Parent=parent;
            }

            public bool Explode(int level)
            {
                var exploded = false;
                if (Parent is not null && level == 4)
                {
                    var first = FirstNumber.GetValueOrDefault();
                    var second = SecondNumber.GetValueOrDefault();
                    if (this == Parent.FirstPair)
                    {
                        AddFirstValueToParent(first);
                        AddSecondValueToParent(second);
                        Parent.FirstNumber = 0;
                        Parent.FirstPair = null;
                    }
                    else if (this == Parent.SecondPair)
                    {
                        AddFirstValueToParent(first);
                        AddSecondValueToParent(second);
                        Parent.SecondNumber = 0;
                        Parent.SecondPair = null;
                    }

                    exploded = true;
                }
                else
                {
                    if (FirstPair is not null && !exploded)
                    {
                        exploded = FirstPair.Explode(level+1);
                        //if (FirstPair.Explode(level+1))
                        //{
                        //    //FirstPair = null;
                        //}
                    }
                    if (SecondPair is not null && !exploded)
                    {
                        exploded = SecondPair.Explode(level+1);
                        //if (SecondPair.Explode(level+1))
                        //{
                        //    //SecondPair = null;
                        //}
                    }
                }

                return exploded;
            }

            private void AddFirstValueToParent(int value)
            {
                if (Parent is null) return;
                if (Parent.FirstNumber is not null) Parent.SetFirstNumber(Parent.FirstNumber.Value + value);
                else Parent.AddFirstValueToParent(value);
            }

            private void AddSecondValueToParent(int value)
            {
                if (Parent is null) return;
                if (Parent.SecondNumber is not null) Parent.SetSecondNumber(Parent.SecondNumber.Value + value);
                else Parent.AddSecondValueToParent(value);

            }

            public bool TrySplitting()
            {
                var splitted = SplitIfNeeded();
                if (!splitted && FirstPair is not null)
                {
                    splitted = FirstPair.TrySplitting();
                }
                if (!splitted && SecondPair is not null)
                {
                    splitted = SecondPair.TrySplitting();
                }

                return splitted;
            }

            private bool SplitIfNeeded()
            {
                var splitted = false;
                if (FirstNumber is not null && FirstNumber > 9)
                {
                    var splitedValue = ((double)FirstNumber.Value/2);
                    FirstPair = new SnailFishNumberPair(this);
                    FirstPair.FirstNumber = Convert.ToInt32(Math.Ceiling(splitedValue));
                    FirstPair.SecondNumber = Convert.ToInt32(Math.Floor(splitedValue));
                    FirstNumber = null;
                    splitted = true;
                }
                else if (SecondNumber is not null && SecondNumber > 9)
                {
                    var splitedValue = ((double)SecondNumber.Value/2);
                    SecondPair = new SnailFishNumberPair(this);
                    SecondPair.FirstNumber = Convert.ToInt32(Math.Ceiling(splitedValue));
                    SecondPair.SecondNumber = Convert.ToInt32(Math.Floor(splitedValue));
                    SecondNumber = null;
                    splitted = true;
                }

                return splitted;
            }

            public int GetMaxSubLevel()
            {
                var maxSubLevel = 0;
                if (FirstPair is not null)
                {
                    var pairMaxSubLevel = FirstPair.GetMaxSubLevel()+1;
                    if (pairMaxSubLevel > maxSubLevel) maxSubLevel = pairMaxSubLevel;
                }

                if (SecondPair is not null)
                {
                    var pairMaxSubLevel = SecondPair.GetMaxSubLevel()+1;
                    if (pairMaxSubLevel > maxSubLevel) maxSubLevel = pairMaxSubLevel;
                }

                return maxSubLevel;
            }

            public long GetMagnitude()
            {
                long firstValue = FirstPair is not null ? FirstPair.GetMagnitude() : FirstNumber.GetValueOrDefault();
                long secondValue = SecondPair is not null ? SecondPair.GetMagnitude() : SecondNumber.GetValueOrDefault();
                return 3*firstValue+2*secondValue;
            }

            public void SetFirstPair(SnailFishNumberPair pair)
            {
                FirstPair = pair;
                FirstNumber = null;
            }

            public void SetFirstNumber(int number)
            {
                FirstPair = null;
                FirstNumber = number;
            }

            public void SetSecondPair(SnailFishNumberPair pair)
            {
                SecondPair = pair;
                SecondNumber = null;
            }

            public void SetSecondNumber(int number)
            {
                SecondPair = null;
                SecondNumber = number;
            }
        }
    }
}