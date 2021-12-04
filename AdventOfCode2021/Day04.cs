namespace AdventOfCode2021
{
    public static class Day04
    {
        private class Bingo
        {
            private List<int> SelectedNumbers { get; set; } = new();
            private List<Board> Boards { get; set; } = new();

            public Bingo(string inputFileName)
            {
                var lines = File.ReadAllLines(inputFileName);
                SelectedNumbers = lines[0].Split(',').Select(int.Parse).ToList();
                for (int i = 2; i < lines.Length; i += 6)
                {
                    var rows = new List<string>(5);
                    rows.Add(lines[i + 0]);
                    rows.Add(lines[i + 1]);
                    rows.Add(lines[i + 2]);
                    rows.Add(lines[i + 3]);
                    rows.Add(lines[i + 4]);
                    Boards.Add(new Board(rows));
                }
            }

            public int GeResultPart1()
            {
                Board winnerBoard = Boards.First();
                var numberOfTurns = Boards.First().GetFirstBingo(SelectedNumbers);
                foreach (var board in Boards)
                {
                    var number = board.GetFirstBingo(SelectedNumbers);
                    if (number < numberOfTurns)
                    {
                        numberOfTurns = number;
                        winnerBoard = board;
                    }
                }

                var unselectedSum = winnerBoard.GetUnselectedSum(SelectedNumbers.Take(numberOfTurns + 1));

                return SelectedNumbers[numberOfTurns] * unselectedSum;
            }


            private class Board
            {
                public int[,] Content { get; private set; } = new int[5, 5];

                public Board(List<string> rows)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        var row = rows[i].Split(' ').Where(x => !string.IsNullOrWhiteSpace(x)).Select(int.Parse).ToArray();
                        for (int j = 0; j < 5; j++)
                        {
                            Content[i, j] = row[j];
                        }
                    }
                }

                public int GetUnselectedSum(IEnumerable<int> selectedNumbers)
                {
                    var sum = 0;
                    for (var i = 0; i < 5; i++)
                    {
                        for (var j = 0; j < 5; j++)
                        {
                            if (selectedNumbers.All(x => x != Content[i, j])) sum += Content[i, j];
                        }
                    }
                    return sum;
                }

                public int GetFirstBingo(IEnumerable<int> numbers)
                {
                    for (var i = 5; i < numbers.Count(); i++)
                    {
                        var array = numbers.Take(i).ToArray();
                        var bingo =
                           checkBingoRow(0, array)
                        || checkBingoRow(1, array)
                        || checkBingoRow(2, array)
                        || checkBingoRow(3, array)
                        || checkBingoRow(4, array)
                        || checkBingoColumn(0, array)
                        || checkBingoColumn(1, array)
                        || checkBingoColumn(2, array)
                        || checkBingoColumn(3, array)
                        || checkBingoColumn(4, array);

                        if (bingo) return i -1;
                    }

                    return 0;
                }

                private bool checkBingoRow(int row, int[] numbers)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (numbers.All(x => x != Content[row, i])) return false;
                    }

                    return true;
                }

                private bool checkBingoColumn(int col, int[] numbers)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        if (numbers.All(x => x != Content[i, col])) return false;
                    }

                    return true;
                }
            }
        }


        public static int CalculatePart1(string inputFileName)
        {
            return new Bingo(inputFileName).GeResultPart1();
        }

        public static int CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            return 0;
        }
    }
}