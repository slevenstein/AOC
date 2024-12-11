
class Day11_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        long sum = 0;

        string line = lines[0];
        Dictionary<long, long> stones = line.Split(' ').Select(long.Parse)
            .ToDictionary(n => n, n => (long) 1);

        for (int n = 0; n < (part == 1 ? 25 : 75); n++)
        {
            Dictionary<long, long> newStones = [];
            foreach (var stone in stones)
            {
                long num = stone.Key;
                long frequency = stone.Value;
                string numString = num.ToString();
                int size = numString.Length;
                if (size % 2 == 0)
                {
                    AddOrMultiplyValue(
                        newStones, 
                        int.Parse(numString[..(size / 2)]), 
                        frequency);

                    AddOrMultiplyValue(
                        newStones,
                        int.Parse(numString[(size / 2)..]), 
                        frequency);
                }
                else if (num == 0)
                {
                    AddOrMultiplyValue(newStones, 1, frequency);
                }
                else
                {
                    AddOrMultiplyValue(newStones, num * 2024, frequency);
                }

                
            }
            stones = newStones;
            Console.WriteLine(n);
        }

        foreach (var stone in stones)
            sum += stone.Value;
        Console.WriteLine(sum);
    }
    static void AddOrMultiplyValue(Dictionary<long, long> map, long num, long frequency)
    {
        if (!map.TryAdd(num, frequency))
        {
            map[num] += frequency;
        }
    }
}

/*
line.Split(' ');
line.Trim().Replace("  ", " ");


(2023)
Help:
3-2 - debugging help -> add *'s to border
5-2 - hint -> do in reverse
8-2 - hint -> LCM
10-2 - hint -> rasterization
12-2 - hint -> DP/cache, dictionary key to string
17-1 - Dijkstra’s refresher
	   hint -> visited data: posn, direction, sameMoves
18-2 - hint -> Shoelaace + Pick's formula
24-2 - include unknown in initial equation

Tips:
12-2, 11-2 - use long for big numbers (wrap in checked context?)
all - SLEEP/TAKE BREAK WHEN STUCK

(2024)
Tips:
10 - dont overthink
*/