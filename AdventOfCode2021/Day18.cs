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
            for (var i = 0; i <= numbers.Count; i++)
            {
                do
                {
                    do
                    {
                        var exploded = result.Explode(0);
                        if (!exploded) break;
                        else
                        {

                        }
                    }
                    while (true);
                    var splitted = result.Split();
                    if (!splitted) break;
                    else
                    {

                    }
                }
                while (true);
                if (i>0 && i<numbers.Count) result = Add(result, numbers[i]);
            }

            return result.GetMagnitude();
        }

        private static SnailFishNumberPair CreatePair(string input, SnailFishNumberPair? parent)
        {
            input = input.Substring(1); //remove '['
            input = input.Substring(0, input.Length-1); //remove ']'
            var pair = new SnailFishNumberPair(parent);
            int endFirstIndex;
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
                pair.SetLeftPair(CreatePair(input.Substring(0, endFirstIndex), pair));
            }
            else
            {
                endFirstIndex = 1;
                pair.SetLeftNumber(int.Parse(input[0].ToString()));
            }

            input = input.Substring(endFirstIndex+1);

            if (input.StartsWith("["))
            {
                pair.SetRightPair(CreatePair(input, pair));
            }
            else
            {
                pair.SetRightNumber(int.Parse(input[0].ToString()));
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
            result.SetLeftPair(first);
            result.SetRightPair(second);
            return result;
        }

        private class SnailFishNumberPair
        {
            public int? LeftNumber { get; private set; }
            public SnailFishNumberPair? LeftPair { get; private set; }
            public int? RightNumber { get; private set; }
            public SnailFishNumberPair? RightPair { get; private set; }
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
                    var first = LeftNumber.GetValueOrDefault();
                    var second = RightNumber.GetValueOrDefault();
                    if (this == Parent.LeftPair)
                    {
                        AddToNextLeftUp(first);
                        AddToNextRightUp(second);
                        Parent.LeftNumber = 0;
                        Parent.LeftPair = null;
                    }
                    else if (this == Parent.RightPair)
                    {
                        AddToNextLeftUp(first);
                        AddToNextRightUp(second);
                        Parent.RightNumber = 0;
                        Parent.RightPair = null;
                    }

                    exploded = true;
                }
                else
                {
                    if (LeftPair is not null && !exploded)
                    {
                        exploded = LeftPair.Explode(level+1);
                    }
                    if (RightPair is not null && !exploded)
                    {
                        exploded = RightPair.Explode(level+1);
                    }
                }

                return exploded;
            }

            private void AddToNextLeftUp(int value)
            {
                if (Parent is null) return;
                if (Parent.LeftPair is not null && Parent.LeftPair != this)
                {
                    Parent.LeftPair.AddToNextRightDown(value);
                }
                else if (Parent.LeftNumber is not null)
                {
                    Parent.LeftNumber+=value;
                }
                else
                {
                    Parent.AddToNextLeftUp(value);
                }
            }

            private void AddToNextRightDown(int value)
            {
                if (RightPair is not null)
                {
                    RightPair.AddToNextRightDown(value);
                }
                else if (RightNumber is not null)
                {
                    RightNumber+=value;
                }
            }

            private void AddToNextRightUp(int value)
            {
                if (Parent is null) return;
                if (Parent.RightPair is not null && Parent.RightPair != this)
                {
                    Parent.RightPair.AddToNextLeftDown(value);
                }
                else if (Parent.RightNumber is not null)
                {
                    Parent.RightNumber+=value;
                }
                else
                {
                    Parent.AddToNextRightUp(value);
                }
            }

            private void AddToNextLeftDown(int value)
            {
                if (LeftPair is not null)
                {
                    LeftPair.AddToNextLeftDown(value);
                }
                else if (LeftNumber is not null)
                {
                    LeftNumber+=value;
                }
            }

            public bool Split()
            {
                var splitted = false;
                if (!splitted && LeftPair is not null)
                {
                    splitted = LeftPair.Split();
                }
                
                if (!splitted && LeftNumber is not null && LeftNumber.Value >= 10)
                {
                    LeftPair = new SnailFishNumberPair(this)
                    {
                        LeftNumber = Convert.ToInt32(Math.Floor(LeftNumber.Value/2f)),
                        RightNumber = Convert.ToInt32(Math.Ceiling(LeftNumber.Value/2f))
                    };

                    LeftNumber = null;
                    splitted = true;
                }
                
                if (!splitted && RightPair is not null)
                {
                    splitted = RightPair.Split();
                }
                
                if (!splitted && RightNumber is not null && RightNumber >= 10)
                {
                    RightPair = new SnailFishNumberPair(this)
                    {
                        LeftNumber = Convert.ToInt32(Math.Floor(RightNumber.Value/2f)),
                        RightNumber = Convert.ToInt32(Math.Ceiling(RightNumber.Value/2f))
                    };

                    RightNumber = null;
                    splitted = true;
                }

                return splitted;
            }

            //public bool TrySplitting()
            //{
            //    var splitted = false;
            //    if (!splitted && LeftPair is not null)
            //    {
            //        splitted = LeftPair.TrySplitting();
            //    }

            //    if (!splitted && RightPair is not null)
            //    {
            //        splitted = RightPair.TrySplitting();
            //    }

            //    if (!splitted)
            //    {
            //        splitted = SplitIfNeeded();
            //    }

            //    return splitted;
            //}

            //private bool SplitIfNeeded()
            //{
            //    var splitted = false;
            //    if (LeftNumber is not null && LeftNumber >= 10)
            //    {
            //        var splitedValue = ((double)LeftNumber.Value/2);
            //        LeftPair = new SnailFishNumberPair(this)
            //        {
            //            LeftNumber = Convert.ToInt32(Math.Floor(splitedValue)),
            //            RightNumber = Convert.ToInt32(Math.Ceiling(splitedValue))
            //        };
            //        LeftNumber = null;
            //        splitted = true;
            //    }
            //    else if (RightNumber is not null && RightNumber >= 10)
            //    {
            //        var splitedValue = ((double)RightNumber.Value/2);
            //        RightPair = new SnailFishNumberPair(this)
            //        {
            //            LeftNumber = Convert.ToInt32(Math.Floor(splitedValue)),
            //            RightNumber = Convert.ToInt32(Math.Ceiling(splitedValue))
            //        };
            //        RightNumber = null;
            //        splitted = true;
            //    }

            //    return splitted;
            //}

            public long GetMagnitude()
            {
                long firstValue = LeftPair is not null ? LeftPair.GetMagnitude() : LeftNumber.GetValueOrDefault();
                long secondValue = RightPair is not null ? RightPair.GetMagnitude() : RightNumber.GetValueOrDefault();
                return 3*firstValue+2*secondValue;
            }

            public void SetLeftPair(SnailFishNumberPair pair)
            {
                LeftPair = pair;
                LeftNumber = null;
            }

            public void SetLeftNumber(int number)
            {
                LeftPair = null;
                LeftNumber = number;
            }

            public void SetRightPair(SnailFishNumberPair pair)
            {
                RightPair = pair;
                RightNumber = null;
            }

            public void SetRightNumber(int number)
            {
                RightPair = null;
                RightNumber = number;
            }
        }
    }
}