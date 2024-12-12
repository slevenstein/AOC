internal class Day12_2024
{
    public static void Solution(int part)
    {
        List<string> lines = File.ReadLines(@"D:\SCL33\Downloads\input").ToList();

        int sum = 0;
        HashSet<(int y, int x)> visited = [];
        for (int i = 0; i < lines.Count; i++)
        {
            string line = lines[i];
            for (int k = 0; k < line.Length; k++)
            {
                char c = line[k];

                if (visited.Contains((i, k)))
                    continue;

                int area = 0;
                int adjacentPerimeters = 0;

                List<(int y, int x, Direction dir)> perimeters = [];
                GetRegionMetrics(i, k);

                for (int l = 0; l < perimeters.Count; l++)
                {
                    (int y, int x, Direction dir) perimeter1 = perimeters[l];
                    for (int r = l + 1; r < perimeters.Count; r++)
                    {
                        (int y, int x, Direction dir) perimeter2 = perimeters[r];
                        if (Math.Abs(perimeter1.x - perimeter2.x) +
                            Math.Abs(perimeter1.y - perimeter2.y)
                            == 1 &&
                            perimeter1.dir == perimeter2.dir)
                        {
                            adjacentPerimeters++;
                        }
                    }
                }

                if (part == 1)
                {
                    sum += area * perimeters.Count;
                }
                else
                {
                    int sides = perimeters.Count - adjacentPerimeters;
                    sum += area * sides;
                }

                void GetRegionMetrics(int y, int x)
                {
                    if (visited.Contains((y, x)))
                        return;

                    area++;
                    visited.Add((y, x));

                    int newY = y + 1;
                    if (newY < lines.Count && lines[newY][x] == c)
                        GetRegionMetrics(newY, x);
                    else
                        perimeters.Add((y, x, Direction.Up));

                    newY = y - 1;
                    if (newY >= 0 && lines[newY][x] == c)
                        GetRegionMetrics(newY, x);
                    else
                        perimeters.Add((y, x, Direction.Down));

                    int newX = x + 1;
                    if (newX < line.Length && lines[y][newX] == c)
                        GetRegionMetrics(y, newX);
                    else
                        perimeters.Add((y, x, Direction.Right));

                    newX = x - 1;
                    if (newX >= 0 && lines[y][newX] == c)
                        GetRegionMetrics(y, newX);
                    else
                        perimeters.Add((y, x, Direction.Left));
                }
            }
        }


        Console.WriteLine(sum);
    }

    private enum Direction
    {
        Right,
        Down,
        Left,
        Up
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