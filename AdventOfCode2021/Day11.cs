namespace AdventOfCode2021
{
    public static class Day11
    {
        public class Octopus
        {
            public int Energy { get; private set; }
            public bool Flashed { get; private set; }

            public void SetFlashed()
            {
                Flashed = true;
            }

            public void IncreaseEnergy() => Energy++;

            public void ResetAfterFlash()
            {
                Energy = 0;
                Flashed = false;
            }

            public static Octopus Create(int energy) => new()
            {
                Energy = energy,
                Flashed = false
            };
        }

        private const int MaxX = 10;
        private const int MaxY = 10;

        public static int CalculatePart1(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var steps = 100;
            var flashes = 0;
            var octopuses = new Octopus[MaxY, MaxX];

            for (var y = 0; y < MaxY; y++)
            {
                var line = lines[y].ToCharArray();
                for (var x = 0; x < MaxX; x++)
                {
                    octopuses[y, x] = Octopus.Create(int.Parse(line[x].ToString()));
                }
            }

            for (var step = 0; step < steps; step++)
            {
                for (var y = 0; y < MaxY; y++)
                {
                    for (var x = 0; x < MaxX; x++)
                    {
                        octopuses[y, x].IncreaseEnergy();
                        Flashes(ref octopuses, x, y);
                    }
                }

                for (var y = 0; y < MaxY; y++)
                {
                    for (var x = 0; x < MaxX; x++)
                    {
                        if (octopuses[y, x].Energy > 9)
                        {
                            octopuses[y, x].ResetAfterFlash();
                            flashes++;
                        }
                    }
                }
            }

            return flashes;
        }

        private static void Flashes(ref Octopus[,] octopuses, int x, int y)
        {
            if (octopuses[y, x].Energy > 9 && !octopuses[y, x].Flashed)
            {
                octopuses[y, x].SetFlashed();
                IncreaseEnergyIfExist(ref octopuses, x - 1, y - 1);
                IncreaseEnergyIfExist(ref octopuses, x - 1, y + 0);
                IncreaseEnergyIfExist(ref octopuses, x - 1, y + 1);

                IncreaseEnergyIfExist(ref octopuses, x + 0, y - 1);
                IncreaseEnergyIfExist(ref octopuses, x + 0, y + 1);

                IncreaseEnergyIfExist(ref octopuses, x + 1, y - 1);
                IncreaseEnergyIfExist(ref octopuses, x + 1, y + 0);
                IncreaseEnergyIfExist(ref octopuses, x + 1, y + 1);
            }
        }

        private static void IncreaseEnergyIfExist(ref Octopus[,] octopuses, int x, int y)
        {
            if (!exists(x, y)) return;
            octopuses[y, x].IncreaseEnergy();
            Flashes(ref octopuses, x, y);
        }

        private static bool exists(int x, int y) => x >= 0 && y >= 0 && x < MaxX && y < MaxY;

        public static long CalculatePart2(string inputFileName)
        {
            var lines = File.ReadAllLines(inputFileName);
            var points = new int[MaxY, MaxX];

            //todo

            return 0;
        }
    }
}