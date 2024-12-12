internal class Day8_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        Dictionary<char, List<(int y, int x)>> map = [];
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            for (int k = 0; k < line.Length; k++)
            {
                char c = line[k];
                if (c != '.')
                {
                    if (map.ContainsKey(c))
                    {
                        map[c].Add((i, k));
                    }
                    else
                    {
                        map.Add(c, [(i, k)]);
                    }
                }
            }
        }

        HashSet<(int y, int x)> antinodes = [];
        foreach (KeyValuePair<char, List<(int y, int x)>> item in map)
        {
            List<(int y, int x)> listOfPositions = item.Value;
            for (int i = 0; i < listOfPositions.Count; i++)
            {
                (int y, int x) first = listOfPositions[i];
                for (int k = i + 1; k < listOfPositions.Count; k++)
                {
                    (int y, int x) second = listOfPositions[k];

                    (int y, int x) difference = Subtract(first, second);

                    (int y, int x) currAntinode = first;

                    if (part == 1)
                    {
                        TryAddAntinode(
                            antinodes,
                            Add(first, difference),
                            lines.Count,
                            lines[0].Length);

                        TryAddAntinode(
                            antinodes,
                            Subtract(second, difference),
                            lines.Count,
                            lines[0].Length);
                    }
                    else if (part == 2)
                    {
                        while (TryAddAntinode(
                                   antinodes,
                                   currAntinode,
                                   lines.Count,
                                   lines[0].Length))
                        {
                            currAntinode = Add(currAntinode, difference);
                        }

                        currAntinode = second;
                        while (TryAddAntinode(
                                   antinodes,
                                   currAntinode,
                                   lines.Count,
                                   lines[0].Length))
                        {
                            currAntinode = Subtract(currAntinode, difference);
                        }
                    }
                }
            }
        }

        sum = antinodes.Count;

        Console.WriteLine(sum);
    }

    private static bool TryAddAntinode(HashSet<(int y, int x)> antinodes, (int y, int x) antinode, int maxY, int maxX)
    {
        if (antinode.y < 0 || antinode.x < 0 || antinode.y >= maxY || antinode.x >= maxX)
        {
            return false;
        }

        antinodes.Add(antinode);
        return true;
    }

    private static (int y, int x) Subtract((int y, int x) first, (int y, int x) second)
    {
        return (first.y - second.y, first.x - second.x);
    }

    private static (int y, int x) Add((int y, int x) first, (int y, int x) second)
    {
        return (first.y + second.y, first.x + second.x);
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