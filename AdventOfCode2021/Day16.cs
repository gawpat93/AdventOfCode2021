using System.Text;

namespace AdventOfCode2021
{
    public static class Day16
    {
        private class Packet
        {
            public int Version { get; init; }
            public int Type { get; init; }
            public int BitCount { get; set; }
            public string BinaryStringValue { get; set; }
            public bool IsValueTpe => Type == 4;
            public int DecimalValue => IsValueTpe ? Convert.ToInt32(BinaryStringValue, 2) : -1;
            public List<Packet> SubPackets { get; init; } = new();
            public void AddSubPacket(Packet packet) => SubPackets.Add(packet);

            public int GetAllVersionsSum()
            {
                var sum = Version;

                foreach (var packet in SubPackets)
                {
                    sum+=packet.GetAllVersionsSum();
                }

                return sum;
            }

            public Packet(int version, int type)
            {
                Version = version;
                Type = type;
                BinaryStringValue = string.Empty;
            }
        }

        private static readonly Dictionary<char, string> hexCharacterToBinary = new Dictionary<char, string>
        {
            { '0', "0000" },
            { '1', "0001" },
            { '2', "0010" },
            { '3', "0011" },
            { '4', "0100" },
            { '5', "0101" },
            { '6', "0110" },
            { '7', "0111" },
            { '8', "1000" },
            { '9', "1001" },
            { 'a', "1010" },
            { 'b', "1011" },
            { 'c', "1100" },
            { 'd', "1101" },
            { 'e', "1110" },
            { 'f', "1111" }
        };
        public static string HexStringToBinary(string hex)
        {
            var result = new StringBuilder();
            foreach (char c in hex)
            {
                result.Append(hexCharacterToBinary[char.ToLower(c)]);
            }
            return result.ToString();
        }

        private static Packet ReadPacket(string binaryString)
        {
            var x = 0;
            var version = Convert.ToInt32(binaryString.Substring(x, 3), 2);
            x+=3;
            var type = Convert.ToInt32(binaryString.Substring(x, 3), 2);
            x+=3;
            var packet = new Packet(version, type);
            if (type == 4) //literal value
            {
                bool hasNext;
                do
                {
                    var data = binaryString.Substring(x, 5);
                    x+=5;
                    var tmp = data.Substring(1, 4);
                    var valuePart = data.Substring(1, 4);
                    packet.BinaryStringValue+=valuePart;
                    hasNext = data[0] == '1';
                }
                while (hasNext);
            }
            else //operator
            {
                var lengthTypeId = binaryString.Substring(x++, 1);
                if (lengthTypeId == "0")
                {
                    var subPacketsLength = Convert.ToInt32(binaryString.Substring(x, 15), 2);
                    x+=15;
                    var subpacketsString = binaryString.Substring(x, subPacketsLength);
                    do
                    {
                        var subPacket = ReadPacket(subpacketsString);
                        packet.AddSubPacket(subPacket);
                        subpacketsString = subpacketsString.Substring(subPacket.BitCount, subpacketsString.Length-subPacket.BitCount);
                    }
                    while (subpacketsString.Any(c => c == '1'));
                    x+=subPacketsLength;
                }
                else
                {
                    var subPacketsNumber = Convert.ToInt32(binaryString.Substring(x, 11), 2);
                    x+=11;
                    var subpacketsString = binaryString.Substring(x);
                    var subPacketsLength = 0;
                    for (var i = 0; i<subPacketsNumber; i++)
                    {
                        var subPacket = ReadPacket(subpacketsString);
                        packet.AddSubPacket(subPacket);
                        subpacketsString = subpacketsString.Substring(subPacket.BitCount, subpacketsString.Length-subPacket.BitCount);
                        subPacketsLength += subPacket.BitCount;
                    }
                    x+=subPacketsLength;
                }
            }

            packet.BitCount = x;

            return packet;
        }

        public static long CalculatePart1(string inputFileName)
        {
            var hexString = File.ReadAllText(inputFileName);

            var binaryString = HexStringToBinary(hexString);
            var packet = ReadPacket(binaryString);

            return packet.GetAllVersionsSum();
        }

        public static long CalculatePart2(string inputFileName)
        {
            //todo

            return 0;
        }
    }
}